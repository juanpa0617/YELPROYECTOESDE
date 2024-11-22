using System.ComponentModel.DataAnnotations;

namespace YELPROYECTOESDE.Models
{
    public class Tipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Propiedad de navegación
        public virtual ICollection<Alojamiento> Alojamientos { get; set; }
    }
}
