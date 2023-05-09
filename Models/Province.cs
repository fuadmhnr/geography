using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Geography.Models;

public class Province
{
  public int id { get; set; }
  [Required]
  public string name { get; set; } = string.Empty;
  [ForeignKey("country")]
  public int country_id { get; set; }
  public Country country { get; set; }
}