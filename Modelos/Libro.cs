using System.ComponentModel.DataAnnotations;

namespace Guia6.Libros
{
    public class Libro
    {
        // Propiedades para acceder a los campos de la tabla

        [Required(ErrorMessage = "El código ISBN es obligatorio")]
        [MaxLength(13, ErrorMessage = "El código ISBN debe tener máximo 13 caracteres")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "El Título es obligatorio")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "El título debe tener como mínimo 2 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El(los) autor(es) es (son) obligatorio(s)")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "El nombre del autor debe tener como mínimo 10 caracteres")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "El año de publicación es obligatorio")]
        [Display(Name = "Año de Publicación")]
        public int AnioPublicacion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un valor positivo")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El campo debe ser un número válido con hasta dos decimales.")]
        public decimal Precio { get; set; }
        [Display(Name = "Fecha de última lectura")]
        [DataType(DataType.Date)]
        public DateTime FechaLectura { get; set; }
        [Required(ErrorMessage = "El número de paginas es obligatorio")]
        [Display(Name = "Número de páginas")]
        public int NumPaginas { get; set; }

    }
}
