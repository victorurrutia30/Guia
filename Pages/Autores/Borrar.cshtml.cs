using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Guia6.Modelos;

namespace Guia6.Pages.Autores
{
    public class BorrarModel : PageModel
    {
        [BindProperty]
        public Autor Autor { get; set; }

        public void OnGet(int id)
        {
            try
            {
                string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();
                    string query = "SELECT * FROM Autores WHERE IdAutor = @IdAutor";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdAutor", id);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                Autor = new Autor
                                {
                                    IdAutor = lector.GetInt32(0),
                                    NombreAutor = lector.GetString(1),
                                    Nacionalidad = lector.IsDBNull(2) ? string.Empty : lector.GetString(2)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Puede registrar el error según sea necesario
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public IActionResult OnPost()
        {
            if (Autor == null || Autor.IdAutor == 0)
            {
                return Page();
            }
            try
            {
                string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();
                    string query = "DELETE FROM Autores WHERE IdAutor = @IdAutor";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@IdAutor", Autor.IdAutor);
                        comando.ExecuteNonQuery();
                    }
                }
                TempData["mensajeExito"] = "El autor se ha eliminado exitosamente";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return RedirectToPage("Index");
        }
    }
}
