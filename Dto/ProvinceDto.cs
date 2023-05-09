namespace Geography.Dto;

public class ProvinceDto
{
  public int id { get; set; }
  public string? name { get; set; }
}

public class ProvinceRequestDto
{
  public string name { get; set; }
  public int country_id { get; set; }

}