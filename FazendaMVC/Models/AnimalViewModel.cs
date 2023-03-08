using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace FazendaMVC.Models
{
    public class AnimalViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage ="O tamanho máximo da tag é 15 caracteres")]
        public string Tag { get; set; }
        public int? FazendaId { get; set; }
    }
}
