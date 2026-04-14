using System.ComponentModel.DataAnnotations;

namespace TestAPI.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 char")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 200 characters")]
        public string Title { get; set; } = String.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 char")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 200 characters")]
        public string Content { get; set; } = String.Empty;

    }
}
