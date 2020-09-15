using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.CrossCutting.FotosPerfil
{
    public interface IArmazenamentoDeFotos
    {
        Task<Uri> ArmazenarFotoDePerfil(IFormFile foto);
        Uri ArmazenarFotoDoPost(IFormFile foto);
    }
}
