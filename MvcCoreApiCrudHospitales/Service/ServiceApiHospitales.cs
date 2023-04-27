using MvcCoreApiCrudHospitales.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;

namespace MvcCoreApiCrudHospitales.Service
{
    public class ServiceApiHospitales
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceApiHospitales(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiCrudHospitales");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            string request = "/api/hospitales";
            List<Hospital> hospitales =
                await this.CallApiAsync<List<Hospital>>(request);
            return hospitales;
        }

        public async Task<Hospital> FindHospitalAsync(int id)
        {
            string request = "/api/hospitales/" + id;
            Hospital hospital =
                await this.CallApiAsync<Hospital>(request);
            return hospital;
        }

        public async Task DeleteHospitalAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/hospitales/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task InsertHospitalAsync
            (string nombre, string direccion, string telefono, int numCamas)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/hospitales";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Hospital hos = new Hospital();
                hos.Nombre = nombre;
                hos.Direccion = direccion;
                hos.Telefono = telefono;
                hos.NumeroCamas = numCamas;
                string json = JsonConvert.SerializeObject(hos);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdateHospitalAsync
            (int idHospital, string nombre, string direccion, string telefono, int numCamas)

        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/hospitales";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Hospital doct =
                    new Hospital
                    {
                        IdHospital = idHospital,
                        Nombre = nombre,
                        Direccion = direccion,
                        Telefono = telefono,
                        NumeroCamas = numCamas
                    };
                string json = JsonConvert.SerializeObject(doct);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(request, content);
            }
        }

    }
}
