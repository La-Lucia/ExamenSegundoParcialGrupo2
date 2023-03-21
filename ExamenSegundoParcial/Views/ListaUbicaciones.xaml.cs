using ExamenSegundoParcial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenSegundoParcial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaUbicaciones : ContentPage
    {
        private Ubicacion ubi;

        public ListaUbicaciones()
        {
            InitializeComponent();
        }
        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ubi = lista.SelectedItem as Models.Ubicacion;
        }

        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnMapa_Clicked(object sender, EventArgs e)
        {
           
            if (!double.TryParse(ubi.latitud, out double lat))
                return;

            if (!double.TryParse(ubi.longitud, out double lng))
                return;

            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                NavigationMode = NavigationMode.Driving
            });

        }

        private void BtnPlay_Clicked(object sender, EventArgs e)
        {

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            lista.ItemsSource = await Controllers.Controller.GetUbicaciones();
        }
    }
}