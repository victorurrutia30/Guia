using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Guia6.Libros;
using Microsoft.Data.SqlClient;


namespace Guia6.Pages.Libros
{
    public class CrearModel : PageModel
    {
        [BindProperty]
        public Libro NewLibro { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (NewLibro.AñoPublicacion.ToString().Length == 0)
            {
                ModelState.AddModelError("NewLibro.AñoPublicacion", "El año de publicación es obligatorio");
            }

            // verificar si no hay mensajes de error
            if (ModelState.IsValid)
            {
                // pasar a los atributos del objeto lo digitado y seleccionado por el usuario
                NewLibro.ISBN = Request.Form["NewLibro.ISBN"];
                NewLibro.Titulo = Request.Form["NewLibro.Titulo"];
                NewLibro.Autor = Request.Form["NewLibro.Autor"];
                NewLibro.AñoPublicacion = int.Parse(Request.Form["NewLibro.AñoPublicacion"]);
                NewLibro.Precio = decimal.Parse(Request.Form["NewLibro.Precio"]);
                NewLibro.FechaLectura = DateTime.Parse(Request.Form["NewLibro.FechaLectura"]);
                NewLibro.NumPaginas = int.Parse(Request.Form["NewLibro.NumPaginas"]);

                try
                {
                    // Definimos una variable y le asignamos la cadena de conexión generada con el
                    // "Explorador de servidores"
                    string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";

                    // Creamos un objeto de la clase SqlConnection indicando como parámetro la
                    // cadena de conexión creada anteriormente
                    SqlConnection conexion = new SqlConnection(cadena);

                    conexion.Open(); // Abrimos la conexión

                    // Crear el query
                    string query = "insert into Libros (ISBN, Titulo, Autor, AñoPublicacion, Precio, FechaUltimaLectura, NumPaginas) " +
                                   "values(@ISBN, @Titulo, @Autor, @AñoPublicacion, @Precio, @FechaUltimaLectura, @NumPaginas);";
                    // Creamos un objeto de la clase SqlCommand
                    SqlCommand comando = new SqlCommand(query, conexion);

                    // Pasar datos ingresados a los parámetros
                    comando.Parameters.AddWithValue("@ISBN", NewLibro.ISBN);
                    comando.Parameters.AddWithValue("@Titulo", NewLibro.Titulo);
                    comando.Parameters.AddWithValue("@Autor", NewLibro.Autor);
                    comando.Parameters.AddWithValue("@AñoPublicacion", NewLibro.AñoPublicacion);
                    comando.Parameters.AddWithValue("@Precio", NewLibro.Precio);
                    comando.Parameters.AddWithValue("@FechaUltimaLectura", NewLibro.FechaLectura);
                    comando.Parameters.AddWithValue("@NumPaginas", NewLibro.NumPaginas);

                    // Le indicamos a SQL Server que ejecute el comando especificado anteriormente
                    comando.ExecuteNonQuery();
                    conexion.Close();  // Cerramos conexión

                    TempData["mensajeExito"] = "El libro se ha creado exitosamente";

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.ToString());
                }

                // Redirigir a la página “Index”
                Response.Redirect("/Libros/Index");

            }
        }
    }

}
