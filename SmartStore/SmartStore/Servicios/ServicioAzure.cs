using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using SmartStore.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartStore.Servicios
{
    public class ServicioAzure
    {
        public MobileServiceClient Client;
        private IMobileServiceTable<Libros> LibroTable;
        private IMobileServiceTable<Emociones> EmocionesTable;

        public ServicioAzure()
        {
            Client = new MobileServiceClient("https://smartstore-luisbeltran.azurewebsites.net/");
            //LibroTable = Client.GetTable<Libros>();
            EmocionesTable = Client.GetTable<Emociones>();
        }

        public async Task InsertEmocionAsync(Emociones emocion)
        {
            try
            {
                await EmocionesTable.InsertAsync(emocion);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task InsertLibrosAsync(List<Libros> Libros)
        {
            foreach (var libro in Libros)
            {
                try
                {
                    await LibroTable.InsertAsync(libro);
                }
                catch(Exception ex)
                {

                }
            }
        }

        public async Task<List<Emociones>> GetEmocionesAsync(string id)
        {
            return await EmocionesTable.Where(x => x.Libro == id).ToListAsync();
        }

        public async Task<ObservableCollection<Libros>> GetLibrosAsync()
        {
            try
            {
                var query = LibroTable.OrderBy(c => c.Nombre);
                var libros = await query.ToListAsync();

                return new ObservableCollection<Libros>(libros);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
