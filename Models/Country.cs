namespace Geography.Models;

public class Country
{
  public int id { get; set; }
  public string name { get; set; } = string.Empty;
  public ICollection<Province>? provinces { get; set; }
}