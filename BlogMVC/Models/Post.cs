namespace BlogMVC.Models
{
    public class Post
    {     
    public int Id { get; set; }
    public string? Title { get; set; } 
    public DateTime PublishDate { get; set; } 
    public string? Description { get; set; } = string.Empty;
    public bool IsDraft { get; set; } 
    public string? Content { get; set; }
    public int? CategoryId  { get; set; }
    public virtual Category? Category { get; set; }
    }
}