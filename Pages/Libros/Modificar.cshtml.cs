using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Guia6.Libros;
using Microsoft.Data.SqlClient;

namespace Guia6.Pages.Libros
{
    public class ModificarModel : PageModel
    {
        [BindProperty]
        public Libro Libro { get; set; }

        public void OnGet(string id)
        {
            try
            {
                string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();
                    string query = "SELECT * FROM Libros WHERE ISBN = @ISBN";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@ISBN", id);
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                Libro = new Libro
                                {
                                    ISBN = lector.GetString(0),
                                    Titulo = lector.GetString(1),
                                    Autor = lector.GetString(2),
                                    AnioPublicacion = lector.GetInt32(3),
                                    Precio = lector.GetDecimal(4),
                                    FechaLectura = lector.GetDateTime(5),
                                    NumPaginas = lector.GetInt32(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Puede implementar el registro del error según convenga
                Console.WriteLine("Error: " + ex.Message);
            }
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
                    string query = "UPDATE Libros SET Titulo=@Titulo, Autor=@Autor, AnioPublicacion=@AnioPublicacion, " +
                                   "Precio=@Precio, FechaUltimaLectura=@FechaUltimaLectura, NumPaginas=@NumPaginas " +
                                   "WHERE ISBN=@ISBN";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@ISBN", Libro.ISBN);
                        comando.Parameters.AddWithValue("@Titulo", Libro.Titulo);
                        comando.Parameters.AddWithValue("@Autor", Libro.Autor);
                        comando.Parameters.AddWithValue("@AnioPublicacion", Libro.AnioPublicacion);
                        comando.Parameters.AddWithValue("@Precio", Libro.Precio);
                        comando.Parameters.AddWithValue("@FechaUltimaLectura", Libro.FechaLectura);
                        comando.Parameters.AddWithValue("@NumPaginas", Libro.NumPaginas);
                        comando.ExecuteNonQuery();
                    }
                }
                TempData["mensajeExito"] = "El libro se ha modificado exitosamente";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return RedirectToPage("Index");
        }
    }
}
