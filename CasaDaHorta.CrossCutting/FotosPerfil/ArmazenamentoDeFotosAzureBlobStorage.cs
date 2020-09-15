using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.CrossCutting.FotosPerfil
{
    public class ArmazenamentoDeFotosAzureBlobStorage : IArmazenamentoDeFotos
    {
        const string connectionString = @"#";

        public async Task<Uri> ArmazenarFotoDePerfil(IFormFile foto)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync("fotos-do-perfil");

            BlobClient blobClient = containerClient.GetBlobClient(foto.FileName);

            var fotoStream = foto.OpenReadStream();
            await blobClient.UploadAsync(fotoStream, true);
            fotoStream.Close();

            return blobClient.Uri;
        }

        public Uri ArmazenarFotoDoPost(IFormFile foto)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("fotos-posts");

            if (containerClient == null)
                blobServiceClient.CreateBlobContainer("fotos-posts");

            BlobClient blobClient = containerClient.GetBlobClient(foto.FileName);

            try
            {
                var fotoStream = foto.OpenReadStream();
                blobClient.UploadAsync(fotoStream, true).Wait();
                fotoStream.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return blobClient.Uri;
        }
    }
}
