using CasaDaHora.Domain.Amigo;
using CasaDaHora.Domain.Amigo.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasaDaHorta.Services.AmigoServices.AjudaPadrão
{
    public class Ajuda
    {
        public static AmigoDomainResponse ConvertendoAmigoDomainEmAmigoDomainResponse(AmigoDomain amigoDomain)
        {
            AmigoDomainResponse amigoDomainResponse = new AmigoDomainResponse
            {
                Id = amigoDomain.Id,
                Nome = amigoDomain.Nome,
                Sobrenome = amigoDomain.Sobrenome,
                Email = amigoDomain.Email,
                Datanascimento = amigoDomain.Datanascimento,
            };
            return amigoDomainResponse;
        }
        public static AmigoDomain ConvertendoAmigoDomainResponseEmAmigoDomain(AmigoDomainResponse amigoDomainResponse)
        {
            AmigoDomain amigoDomain = new AmigoDomain
            {
                Id = amigoDomainResponse.Id,
                Nome = amigoDomainResponse.Nome,
                Sobrenome = amigoDomainResponse.Sobrenome,
                Email = amigoDomainResponse.Email,
                Datanascimento = amigoDomainResponse.Datanascimento,
            };
            return amigoDomain;
        }
    }
}
