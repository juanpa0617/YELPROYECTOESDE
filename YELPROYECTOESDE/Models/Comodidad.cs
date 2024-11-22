using YELPROYECTOESDE.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace YELPROYECTOESDE.Models
{
    public class Comodidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        // Propiedad de navegación
        public virtual ICollection<DetalleAlojamientoComodidad> DetallesAlojamientoComodidad { get; set; }
    }
}
