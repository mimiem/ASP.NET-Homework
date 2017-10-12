namespace MyTinyBlog.Web.Extensions
{
    using MyTinyBlog.Web.ViewModels.Blog;
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class ActionLinkExtensions
    {
        public static MvcHtmlString PostLink(this HtmlHelper helper, BlogPostViewModel post)
        {
            return helper.ActionLink(post.Title, "Post", "Blog",
                new
                {
                    year = post.CreatedOn.Year,
                    month = post.CreatedOn.Month,
                    title = post.UrlSlug
                },
                new
                {
                    title = post.Title
                });
        }

        public static MvcHtmlString CategoryLink(this HtmlHelper helper,
            CategoryViewModel category)
        {
            return helper.ActionLink(category.Name, "Category", "Blog",
                new
                {
                    category = category.UrlSlug
                },
                new
                {
                    title = String.Format("See all posts in {0}", category.Name)
                });
        }

        public static MvcHtmlString TagLink(this HtmlHelper helper, TagViewModel tag)
        {
            return helper.ActionLink(tag.Name, "Tag", "Blog", new { tag = tag.UrlSlug },
                new
                {
                    title = String.Format("See all posts in {0}", tag.Name)
                });
        }
    }
}