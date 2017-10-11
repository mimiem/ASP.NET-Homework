using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTinyBlog.Data.Models
{
    public class Tag
    {
        private ICollection<BlogPost> blogPosts;

        public Tag()
        {
            this.blogPosts = new HashSet<BlogPost>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// The UrlSlug property is an alternate for the Title property to use in address.
        /// </summary>
        public string UrlSlug { get; set; }

        public string Description { get; set; }

        public virtual ICollection<BlogPost> BlogPosts
        {
            get { return this.blogPosts; }
            set { this.blogPosts = value; }
        }

    }
}
