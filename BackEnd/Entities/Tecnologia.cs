using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AplicacaoAPI;
using TecnologiaAPI;
using AplicacaoTecnologiaAPI.Entities;


namespace TecnologiaAPI
{

    public class Tecnologia
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Nome { get; set; } = string.Empty;

        public ICollection<AplicacaoTecnologia> AplicacaoTecnologia { get; set; } = new List<AplicacaoTecnologia>();
    }
}
