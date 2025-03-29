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
            if (NewLibro.A񯐵blicacion.ToString().Length == 0)
            {
                ModelState.AddModelError("NewLibro.A񯐵blicacion", "El a񯠤e publicaci󮠥s obligatorio");
            }

            // verificar si no hay mensajes de error
            if (ModelState.IsValid)
            {
                // pasar a los atributos del objeto lo digitado y seleccionado por el usuario
                NewLibro.ISBN = Request.Form["NewLibro.ISBN"];
                NewLibro.Titulo = Request.Form["NewLibro.Titulo"];
                NewLibro.Autor = Request.Form["NewLibro.Autor"];
                NewLibro.A񯐵blicacion = int.Parse(Request.Form["NewLibro.A񯐵blicacion"]);
                NewLibro.Precio = decimal.Parse(Request.Form["NewLibro.Precio"]);
                NewLibro.FechaLectura = DateTime.Parse(Request.Form["NewLibro.FechaLectura"]);
                NewLibro.NumPaginas = int.Parse(Request.Form["NewLibro.NumPaginas"]);

                try
                {
                    // Definimos una variable y le asignamos la cadena de conexi󮠧enerada con el
                    // "Explorador de servidores"
                    string cadena = "Data Source=Victor\\MSSQLSERVER2022;Initial Catalog=Base2;Integrated Security=True;Trust Server Certificate=True";

                    // Creamos un objeto de la clase SqlConnection indicando como par᭥tro la
                    // cadena de conexi󮠣reada anteriormente
                    SqlConnection conexion = new SqlConnection(cadena);

                    conexion.Open(); // Abrimos la conexi󮍊
                    // Crear el query
                    string query = "insert into Libros (ISBN, Titulo, Autor, A񯐵blicacion, Precio, FechaUltimaLectura, NumPaginas) " +
                                   "values(@ISBN, @Titulo, @Autor, @A񯐵blicacion, @Precio, @FechaUltimaLectura, @NumPaginas);";
                    // Creamos un objeto de la clase SqlCommand
                    SqlCommand comando = new SqlCommand(query, conexion);

                    // Pasar datos ingresados a los par᭥tros
                    comando.Parameters.AddWithValue("@ISBN", NewLibro.ISBN);
                    comando.Parameters.AddWithValue("@Titulo", NewLibro.Titulo);
                    comando.Parameters.AddWithValue("@Autor", NewLibro.Autor);
                    comando.Parameters.AddWithValue("@A񯐵blicacion", NewLibro.A񯐵blicacion);
                    comando.Parameters.AddWithValue("@Precio", NewLibro.Precio);
                    comando.Parameters.AddWithValue("@FechaUltimaLectura", NewLibro.FechaLectura);
                    comando.Parameters.AddWithValue("@NumPaginas", NewLibro.NumPaginas);

                    // Le indicamos a SQL Server que ejecute el comando especificado anteriormente
                    comando.ExecuteNonQuery();
                    conexion.Close();  // Cerramos conexi󮍊
                    TempData["mensajeExito"] = "El libro se ha creado exitosamente";

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.ToString());
                }

                // Redirigir a la p᧩na Ӊndexԍ
                Response.Redirect("/Libros/Index");

            }
        }
    }

}
