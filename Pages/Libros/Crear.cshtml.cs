using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Guia6.Libros;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Guia6.Pages.Libros
{
    public class CrearModel : PageModel
    {
        [BindProperty]
        public Libro NewLibro { get; set; }

        // Lista de autores para el dropdown
        public List<SelectListItem> Autores { get; set; }

        public void OnGet()
        {
            CargarAutores();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";

                    // Obtener el nombre del autor a partir del Id seleccionado
                    string autorNombre = "";
                    using (SqlConnection conexion = new SqlConnection(cadena))
                    {
                        conexion.Open();
                        string queryAutor = "SELECT NombreAutor FROM Autores WHERE IdAutor = @IdAutor";
                        using (SqlCommand comandoAutor = new SqlCommand(queryAutor, conexion))
                        {
                            comandoAutor.Parameters.AddWithValue("@IdAutor", NewLibro.IdAutor);
                            object resultado = comandoAutor.ExecuteScalar();
                            if (resultado != null)
                            {
                                autorNombre = resultado.ToString();
                            }
                        }
                    }

                    // Asignar el nombre del autor al campo Autor del objeto libro
                    NewLibro.Autor = autorNombre;

                    using (SqlConnection conexion = new SqlConnection(cadena))
                    {
                        conexion.Open();
                        string query = "INSERT INTO Libros (ISBN, Titulo, Autor, IdAutor, AnioPublicacion, Precio, FechaUltimaLectura, NumPaginas) " +
                                       "VALUES(@ISBN, @Titulo, @Autor, @IdAutor, @AnioPublicacion, @Precio, @FechaUltimaLectura, @NumPaginas)";
                        using (SqlCommand comando = new SqlCommand(query, conexion))
                        {
                            comando.Parameters.AddWithValue("@ISBN", NewLibro.ISBN);
                            comando.Parameters.AddWithValue("@Titulo", NewLibro.Titulo);
                            comando.Parameters.AddWithValue("@Autor", NewLibro.Autor);
                            comando.Parameters.AddWithValue("@IdAutor", NewLibro.IdAutor);
                            comando.Parameters.AddWithValue("@AnioPublicacion", NewLibro.AnioPublicacion);
                            comando.Parameters.AddWithValue("@Precio", NewLibro.Precio);
                            comando.Parameters.AddWithValue("@FechaUltimaLectura", NewLibro.FechaLectura);
                            comando.Parameters.AddWithValue("@NumPaginas", NewLibro.NumPaginas);
                            comando.ExecuteNonQuery();
                        }
                    }
                    TempData["mensajeExito"] = "El libro se ha creado exitosamente";
                    return RedirectToPage("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            // Si el ModelState no es válido, se recarga la lista de autores para volver a mostrarlos
            CargarAutores();
            return Page();
        }

        private void CargarAutores()
        {
            Autores = new List<SelectListItem>();
            try
            {
                string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();
                    string query = "SELECT IdAutor, NombreAutor FROM Autores";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                Autores.Add(new SelectListItem
                                {
                                    Value = lector.GetInt32(0).ToString(),
                                    Text = lector.GetString(1)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar autores: " + ex.Message);
            }
        }
    }
}
