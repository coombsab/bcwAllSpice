namespace bcwAllSpice.Models;

public class Recipe : IRepoItem<int> {
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  [Required]
  public string Title { get; set; }
  public string Subtitle { get; set; }
  public string Instructions { get; set; }
  public string Img { get; set; }
  [Required]
  public string Category { get; set; }
  [Required]
  public string CreatorId { get; set; }

  // populated info
  public Profile Creator { get; set; }
  public List<string> FavoriteeIds { get; set; }
  public List<Profile> Favoritees { get; set; }
}