using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartStore.Clases
{
    public class Catalogo
    {
        List<Libros> Libros;
        public int Paginas;
        int LibrosPorPagina = 50;

        public Catalogo()
        {
            var assembly = typeof(Catalogo).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream("SmartStore.Datos.catalog.txt");
            var datos = "";

            using (var reader = new StreamReader(stream))
            {
                datos = reader.ReadToEnd();
            }

            Libros = new List<Libros>();
            var libros = datos.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Paginas = (int)Math.Ceiling((decimal)libros.Length / LibrosPorPagina);

            foreach (var libro in libros)
            {
                var registro = libro.Split(new string[] { "," }, StringSplitOptions.None);

                if (registro.Length > 1)
                {
                    Libros.Add(new Libros()
                    {
                        Id = registro[0],
                        Nombre = registro[1],
                        Categoria = registro[2],
                        Autor = registro[4].Replace("Author=", ""),
                        Editorial = registro[5].Replace("Publisher=", ""),
                        Ano = registro[6].Replace("Year=", "")
                    });
                }
            }
        }

        public async Task InsertarLibros()
        {
            await App.ServicioAzure.InsertLibrosAsync(Libros);
        }

        public Libros ObtenerLibro(string ID)
        {
            return Libros.Where(x => x.Id == ID).FirstOrDefault();
        }

        public List<Libros> ObtenerLibros(int pagina)
        {
            return Libros.Skip((pagina - 1) * LibrosPorPagina).Take(LibrosPorPagina).ToList();
        }
    }
}
