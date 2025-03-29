using Guia6.Libros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Linq.Expressions;
using Guia6.Pages.Libros;

namespace Guia6.Pages.Libros
{
    
    public class IndexModel : PageModel
    {
        public List<Libro> listaLibros = new List<Libro>();
        public void OnGet()
        {
            try
            {
                // Definimos una variable y le asignamos la cadena de conexión generada con el
                // "Explorador de servidores"
                string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";

                // Creamos un objeto de la clase SQLConnection indicando como parámetro la cadena de conexión
                SqlConnection conexion = new SqlConnection(cadena);

                conexion.Open(); // Abrimos la conexión

                // Creamos un objeto de la clase SqlCommand para consultar todos los registros de la tabla
                SqlCommand comando = new SqlCommand("select * from Libros", conexion);

                /* Creamos un objeto de la clase SqlDataReader e inicializándolo mediante la llamada del método ExecuteReader
                   de la clase SQLCommand */
                SqlDataReader lector = comando.ExecuteReader();

                // Recorremos el SqlDataReader
                while (lector.Read())
                {
                    Libro libro = new Libro(); // Creamos un objeto
                    libro.ISBN = lector.GetString(0);
                    libro.Titulo = lector.GetString(1);
                    libro.Autor = lector.GetString(2);
                    libro.AñoPublicacion = lector.GetInt32(3);
                    libro.Precio = lector.GetDecimal(4);
                    libro.FechaLectura = lector.GetDateTime(5);
                    libro.NumPaginas = lector.GetInt32(6);

                    listaLibros.Add(libro); // Agregamos el objeto a la lista
                }

                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

