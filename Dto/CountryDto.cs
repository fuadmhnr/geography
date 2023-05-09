namespace Geography.Dto;

public class CountryDto
{
  public int id { get; set; }
  public string? name { get; set; }
}

public class CountryRequestDto
{
  public string name { get; set; } = string.Empty;
}
