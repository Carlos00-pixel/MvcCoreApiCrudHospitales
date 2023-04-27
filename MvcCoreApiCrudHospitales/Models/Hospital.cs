using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCoreApiCrudHospitales.Models
{
    public class Hospital
    {
        public int IdHospital { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int NumeroCamas { get; set; }
    }
}
