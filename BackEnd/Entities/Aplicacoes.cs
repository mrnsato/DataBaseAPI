using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AplicacaoTecnologiaAPI.Entities;
using AplicacaoTecnologiaAPI.Data;
using TecnologiaAPI;

namespace AplicacaoAPI
{
    public class Aplicacoes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Nome { get; set; } = string.Empty;

        public ICollection<AplicacaoTecnologia> AplicacaoTecnologia { get; set; } = new List<AplicacaoTecnologia>();
       
    }
}
