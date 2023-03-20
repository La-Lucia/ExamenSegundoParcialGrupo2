using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenSegundoParcial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Modificar : ContentPage
    {
        byte[] firma;

        public Modificar()
        {
            InitializeComponent();

        }

        private void BtnUbicacion_Clicked(object sender, EventArgs e)
        {
            GetLocation();
        }

        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {

            ObtenerFirma();

        }

        public async void GetLocation()
        {

            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                Latitud.Text = location.Latitude.ToString();
                Longitud.Text = location.Longitude.ToString();
            }

        }

        public async void ObtenerFirma()
        {

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
            catch (Exception ex)
            {

                /*await DisplayAlert("no se pudo guardar la firma", "error " + ex, "ok");*/

            }


        }
    }
}