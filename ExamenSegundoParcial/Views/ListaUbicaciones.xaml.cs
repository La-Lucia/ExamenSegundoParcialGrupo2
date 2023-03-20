using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenSegundoParcial.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaUbicaciones : ContentPage
    {
        public ListaUbicaciones()
        {
            InitializeComponent();
        }
        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnActualizar_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnMapa_Clicked(object sender, EventArgs e)
        {
           
            /*if (!double.TryParse(Latitud.Text, out double lat))
                return;

            if (!double.TryParse(Longitud.Text, out double lng))
                return;

            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                NavigationMode = NavigationMode.Driving
            });*/

        }

        private void BtnPlay_Clicked(object sender, EventArgs e)
        {

        }
    }
}