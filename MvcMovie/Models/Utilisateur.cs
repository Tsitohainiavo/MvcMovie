using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Utilisateur
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string EMAIL { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PASSWORD { get; set; }
    }
}
