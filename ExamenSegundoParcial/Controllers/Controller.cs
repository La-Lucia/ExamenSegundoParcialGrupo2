using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExamenSegundoParcial.Controllers
{
    public class Controller
    {
        public async static Task CreateUbicacion(Models.Ubicacion ubicacion)
        {

            String json = JsonConvert.SerializeObject(ubicacion);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage respuesta = null;

            using(HttpClient client = new HttpClient())
            {
                respuesta = await client.PostAsync("http://192.168.1.6/examen/Create.php", content);
            }

            if (respuesta.IsSuccessStatusCode) {

                var resultado = respuesta.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Consultado");
            
            }

        }

        

        public async static Task<List<Models.Ubicacion>> GetUbicaciones()
        {



            List<Models.Ubicacion> posts = new List<Models.Ubicacion>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var repsonse = await client.GetAsync("http://192.168.1.6/examen/Read.php");

                    if (repsonse.IsSuccessStatusCode)
                    {

                        posts = JsonConvert.DeserializeObject<List<Models.Ubicacion>>(repsonse.Content.ReadAsStringAsync().Result);

                    }
                }
            }
            catch (Exception ex) {

               Console.WriteLine("error" + ex);

            }

            return posts;

        }

    }
}
