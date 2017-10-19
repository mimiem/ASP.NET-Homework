namespace MyTinyBlog.Data.Models
{
    using System;

    public class PostComment
    {
        public PostComment()
        {
            this.IsVisible = true;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsVisible { get; set; }
    }
}
