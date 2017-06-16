using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage.Auth;

namespace SmartStore.Servicios
{
    public static class ServiceStorage
    {
        public static async Task<Stream> Descargar(string url)
        {
            try
            {
                CloudBlockBlob blob = new CloudBlockBlob(new Uri(url));
                MemoryStream stream = new MemoryStream();
                await blob.DownloadToStreamAsync(stream);
                return stream;
            }
            catch (Exception exc)
            {
                string msgError = exc.Message;
                return null;
            }

        }

        public static async Task<string> performBlobOperation(Stream stream)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=smartstoreluisbeltran;AccountKey=frr83m1L/GfqnFFTijq2ayV0+qWWmDoiaV3XRdkxG/H/3YJDAzNfwUY9843ZK+Lzanr/efK+AhCV3VAkb5WnEg==");
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("fotos");
                await container.CreateIfNotExistsAsync();


                var id = Guid.NewGuid();
                CloudBlockBlob blob = container.GetBlockBlobReference(id + ".jpg");

                using (stream)
                {
                    await blob.UploadFromStreamAsync(stream);
                    return blob.StorageUri.PrimaryUri.AbsoluteUri;
                }
            }
            catch (Exception exc)
            {
                string msgError = exc.Message;
                return "";
            }

        }

    }
}