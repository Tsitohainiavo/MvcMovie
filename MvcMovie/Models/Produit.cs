using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

[Table ("Produit")]
public class Produit
{
    [Column("ID")]
    public int Id { get; set; }
    [Column("NOM")]
    public required string Nom { get; set; }

    [Column("PRIX")]  // Utilisez PRIX en majuscules
    public decimal Prix { get; set; }
}
