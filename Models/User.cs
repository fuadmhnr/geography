using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Geography.Models;

public class User
{
  public int id { get; set; }
  public string username { get; set; } = string.Empty;
  public byte[] password_hash { get; set; }
  public byte[] password_salt { get; set; }
}