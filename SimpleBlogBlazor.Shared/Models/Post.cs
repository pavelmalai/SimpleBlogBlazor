using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleBlogBlazor.Shared.Models
{
    public class Post
    {
        public Guid PostId { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Title is long (200 character limit).")]
        public string Title { get; set; }
        public string Content { get; set; }
        public int Category { get; set; }
        public PostStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
