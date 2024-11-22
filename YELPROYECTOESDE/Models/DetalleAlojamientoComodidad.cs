namespace YELPROYECTOESDE.Models
{
    public class DetalleAlojamientoComodidad
    {
        public int Id { get; set; }
        public int IdAlojamiento { get; set; }
        public int ComodidadId { get; set; }

        // Propiedades de navegación
        public virtual Alojamiento Alojamiento { get; set; }
        public virtual Comodidad Comodidad { get; set; }
    }
}
