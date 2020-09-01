using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.CrossCutting.Storage
{
    public class AzureStorage
    {
        private AzureStorageOptions Options { get; set; }

        private CloudBlobClient CloudBlobClient { get; set; }
        private CloudBlobDirectory ImagesDirectory { get; set; }

        public AzureStorage(IOptions<AzureStorageOptions> options)
        {
            this.Options = options.Value;

            this.Setup();

        }

        //este aqui é responsávelpor se conectar ao nosso storage
        private void Setup()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(this.Options.ConnectionString);

            this.CloudBlobClient = account.CreateCloudBlobClient();

            CloudBlobContainer container = this.CloudBlobClient.GetContainerReference("imagens");

            this.ImagesDirectory = container.GetDirectoryReference(".");

        }

        public async Task<String> SaveToStorage(byte[] buffer, string fileName)
        {
            CloudBlockBlob cloudBlockBlob = null;

            cloudBlockBlob = this.ImagesDirectory.GetBlockBlobReference(fileName);

            using (Stream stream = new MemoryStream(buffer))
            {
                await cloudBlockBlob.UploadFromStreamAsync(stream);
            }

            return $"https://projetocasadahorta.blob.core.windows.net/imagens/{fileName}";
        }
    }
}
