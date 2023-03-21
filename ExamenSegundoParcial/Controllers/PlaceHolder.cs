using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExamenSegundoParcial.Controllers
{
    public static class PlaceHolder
    {

        public async static Task<List<Models.Ubicacion>> GetUbicaciones() {

            List<Models.Ubicacion> posts = new List<Models.Ubicacion>();
        
            using(HttpClient client = new HttpClient())
            {
                var repsonse = await client.GetAsync("http://192.168.1.6/examen/Read.php");

                if (repsonse.IsSuccessStatusCode) { 
                
                    posts = JsonConvert.DeserializeObject<List<Models.Ubicacion>>(repsonse.Content.ReadAsStringAsync().Result);

                }
            }

            return posts;
                
        }

    }
}
