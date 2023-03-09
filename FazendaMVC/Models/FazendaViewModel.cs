using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FazendaMVC.Models
{
    public class FazendaViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<AnimalViewModel> Animais { get; set; }
    }
}
