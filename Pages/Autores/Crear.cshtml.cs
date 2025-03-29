using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Guia6.Modelos;

namespace Guia6.Pages.Autores
{
    public class CrearModel : PageModel
    {
        [BindProperty]
        public Autor NewAutor { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();
                    string query = "INSERT INTO Autores (NombreAutor, Nacionalidad) VALUES (@NombreAutor, @Nacionalidad)";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@NombreAutor", NewAutor.NombreAutor);
                        comando.Parameters.AddWithValue("@Nacionalidad", NewAutor.Nacionalidad);
                        comando.ExecuteNonQuery();
                    }
                }
                TempData["mensajeExito"] = "El autor se ha creado exitosamente";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return RedirectToPage("Index");
        }
    }
}
