namespace MyTinyBlog.Data.Models
{
    using Contracts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BlogPost : DeletableEntity
    {
        private ICollection<PostComment> comments;
        private ICollection<Tag> tags;

        public BlogPost()
        {
            this.comments = new HashSet<PostComment>();
            this.tags = new HashSet<Tag>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent { get; set; }

        /// <summary>
        /// The Meta property is used to store the metadata description for the post
        /// </summary>
        public string Meta { get; set; }

        public string ImageOrVideoUrl { get; set; }

        /// <summary>
        /// The UrlSlug property is an alternate for the Title property to use in address.
        /// </summary>
        public string UrlSlug { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<PostComment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
