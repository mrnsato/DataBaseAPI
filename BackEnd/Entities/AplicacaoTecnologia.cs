using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AplicacaoAPI;
using TecnologiaAPI;
using AplicacaoTecnologiaAPI.Data;

namespace AplicacaoTecnologiaAPI.Entities
{
    public class AplicacaoTecnologia
    {
        public int AplicacaoId { get; set; }
        public required Aplicacoes Aplicacao { get; set; }

        public int TecnologiaId { get; set; }
        public required Tecnologia Tecnologia { get; set; }
    }
}
