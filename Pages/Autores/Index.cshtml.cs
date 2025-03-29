using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Guia6.Modelos;

namespace Guia6.Pages.Autores
{
    public class IndexModel : PageModel
    {
        public List<Autor> listaAutores { get; set; } = new List<Autor>();

        public void OnGet()
        {
            try
            {
                string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";
                using (SqlConnection conexion = new SqlConnection(cadena))
                {
                    conexion.Open();
                    string query = "SELECT * FROM Autores";
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                            {
                                Autor autor = new Autor
                                {
                                    IdAutor = lector.GetInt32(0),
                                    NombreAutor = lector.GetString(1),
                                    Nacionalidad = lector.IsDBNull(2) ? string.Empty : lector.GetString(2)
                                };
                                listaAutores.Add(autor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
