namespace MyTinyBlog.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<BlogPost> posts;

        public Category()
        {
            this.posts = new HashSet<BlogPost>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// The UrlSlug property is an alternate for the Title property to use in address.
        /// </summary>
        public string UrlSlug { get; set; }

        public string Description { get; set; }

        public virtual ICollection<BlogPost> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }
    }
}
