using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHora.Domain.Post.IPostRepository
{
    public interface IImagemServices
    {
        Task<string> UploadFile(string type, Guid Id, string fileExtension, byte[] image);
    }
}
