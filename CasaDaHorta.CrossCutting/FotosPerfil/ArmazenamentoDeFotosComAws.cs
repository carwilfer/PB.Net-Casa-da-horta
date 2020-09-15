using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaHorta.CrossCutting.FotosPerfil
{
    public class ArmazenamentoDeFotosComAws : IArmazenamentoDeFotos
    {
        public Task<Uri> ArmazenarFotoDePerfil(IFormFile foto)
        {
            throw new NotImplementedException();
        }

        public Uri ArmazenarFotoDoPost(IFormFile foto)
        {
            throw new NotImplementedException();
        }
    }
}
