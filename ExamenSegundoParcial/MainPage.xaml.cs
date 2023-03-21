using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SignaturePad.Forms;
using System.IO;
using ExamenSegundoParcial.Views;

namespace ExamenSegundoParcial
{
    public partial class MainPage : ContentPage
    {
        byte[] audioBytes;
        byte[] firma;
        AudioRecorderService recorder;
        AudioPlayer player;

        public MainPage()
        {
            InitializeComponent();

            recorder = new AudioRecorderService
            {
                StopRecordingAfterTimeout = true,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };

            player = new AudioPlayer();
            player.FinishedPlaying += Player_FinishedPlaying;

            verificarGPS();

        }

        private async void BtnGrabar_Clicked(object sender, EventArgs e)
        {
            await RecordAudio();
        }


        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            ObtenerFirma();
            ObtenerAudio();
            
            if (Latitud.Text == null || Longitud == null)
            {

                await DisplayAlert("Atencion", "Obten tu ubicacion antes", "Aceptar");

            }
            else if (firma == null)
            {

                await DisplayAlert("Atencion", "Pon tu firma antes", "Aceptar");

            }
            else if (audioBytes == null)
            {

                await DisplayAlert("Atencion", "Graba un audio antes", "Aceptar");

            }
            else {

                try
                {

                    var ubi = new Models.Ubicacion
                    {
                        id = 0,
                        firma = firma,
                        latitud = Latitud.Text,
                        longitud = Longitud.Text,
                        audio = audioBytes
                    };

                    await Controllers.Controller.CreateUbicacion(ubi);
                } catch (Exception ex) {

                    await DisplayAlert("error", "error" + ex, "aceptar");

                }
            
            }

        }

        private void BtnUbicacion_Clicked(object sender, EventArgs e)
        {
            GetLocation();
        }

        private void BtnPlay_Clicked(object sender, EventArgs e)
        {
            PlayAudio();

        }

        // Obtener latitud y longitud
        public async void GetLocation() {

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                Latitud.Text = location.Latitude.ToString();
                Longitud.Text = location.Longitude.ToString();
            }

        }


        // Grabar Audio
        private async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording) //Record button clicked
                {
                    recorder.StopRecordingOnSilence = TimeoutSwitch.IsToggled;

                    BtnGrabar.IsEnabled = false;
                    BtnPlay.IsEnabled = false;

                    //start recording audio
                    var audioRecordTask = await recorder.StartRecording();

                    BtnGrabar.Text = "Stop Recording";
                    BtnGrabar.IsEnabled = true;

                    await audioRecordTask;

                    BtnGrabar.Text = "Record";
                    BtnPlay.IsEnabled = true;
                }
                else //Stop button clicked
                {
                    BtnGrabar.IsEnabled = false;

                    //stop the recording...
                    await recorder.StopRecording();

                    BtnGrabar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // Reproducir el Audio Grabado
        private async void PlayAudio()
        {
            try
            {
                var filePath = recorder.GetAudioFilePath();
                audioBytes = File.ReadAllBytes(filePath);

                // Convertir el arreglo de bytes a una cadena base64
                string base64String = Convert.ToBase64String(audioBytes);
                await DisplayAlert("Audio guardado en byte", "Audio " + audioBytes, "ok");
                Console.WriteLine(BitConverter.ToString(audioBytes));

                if (filePath != null)
                {
                    BtnPlay.IsEnabled = false;
                    BtnGrabar.IsEnabled = false;

                    player.Play(filePath);
                }
            }
            catch (Exception ex)
            {
                //blow up the app!
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // Terminar de reproducir el audio
        void Player_FinishedPlaying(object sender, EventArgs e)
        {
            BtnPlay.IsEnabled = true;
            BtnGrabar.IsEnabled = true;
        }

        private async void BtnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaUbicaciones());

        }

        // Verificar si el GPS esta encendido, si no se cierra la app
        public async void verificarGPS() {

            var locator = CrossGeolocator.Current;

            if (!locator.IsGeolocationEnabled)
            {
                // Mostrar un mensaj sigue
                await DisplayAlert("Error", "La ubicación no está habilitada.", "Aceptar");
                Process.GetCurrentProcess().Kill();
            }

        }

        //Obtener la firma y convertirla en base 64
        public async void ObtenerFirma() {

            try
            {
                Stream bitmap = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
                using (MemoryStream ms = new MemoryStream())
                {
                    await bitmap.CopyToAsync(ms);
                    firma = ms.ToArray();

                    /*await DisplayAlert("Firma guardada en byte", "Firma " + firma, "ok");
                    Console.WriteLine(BitConverter.ToString(firma));*/

                }
            }
            catch (Exception ex) {

                /*await DisplayAlert("no se pudo guardar la firma", "error " + ex, "ok");*/
            
            }


        }



        //Obtener el audio y convertirlo a base 64
        public async void ObtenerAudio()
        {

            try
            {
                audioBytes = File.ReadAllBytes(recorder.GetAudioFilePath());

                // Convertir el arreglo de bytes a una cadena base64
                string base64String = Convert.ToBase64String(audioBytes);
                /*await DisplayAlert("Audio guardado en byte", "Audio " + audioBytes, "ok");
                Console.WriteLine(BitConverter.ToString(audioBytes));*/
            }
            catch (Exception ex)
            {

                /*await DisplayAlert("no se pudo guardar el audio", "error " + ex, "ok");*/

            }


        }

    }
}
