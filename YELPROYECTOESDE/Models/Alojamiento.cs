using System.ComponentModel.DataAnnotations;

namespace YELPROYECTOESDE.Models
{
    public class Alojamiento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La capacidad es requerida")]
        [Range(1, 7, ErrorMessage = "La capacidad debe estar entre 1 y 7")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo de alojamiento es requerido")]
        public int TipoId { get; set; }

        public string? ImagenUrl { get; set; }

        // Propiedades de navegación
        public virtual Tipo? Tipo { get; set; }
        public virtual ICollection<DetalleAlojamientoComodidad>? DetallesAlojamientoComodidad { get; set; }
    }
}