using System.ComponentModel.DataAnnotations;

namespace Guia6.Modelos
{
    public class Autor
    {
        public int IdAutor { get; set; }

        [Required(ErrorMessage = "El nombre del autor es obligatorio")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "El nombre del autor debe tener entre 2 y 150 caracteres")]
        [Display(Name = "Nombre del Autor")]
        public string NombreAutor { get; set; }

        [Display(Name = "Nacionalidad")]
        [StringLength(100, ErrorMessage = "La nacionalidad debe tener máximo 100 caracteres")]
        public string Nacionalidad { get; set; }
    }
}
