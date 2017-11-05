namespace MyTinyBlog.Web.MyTinyBlog.Web.Controllers
{
    using Services.Data;
    using MyTinyBlog.Web.ViewModels.Blog;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Net;

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminService service;

        public AdminController()
        {
            this.service = new AdminService();
        }

        [HttpGet]
        public ViewResult Manage()
        {
            ViewBag.Title = "My Posts";
            IEnumerable<BlogPostViewModel> posts = this.service.GetAllPosts();
            IEnumerable<CategoryViewModel> categories = this.service.GetAllCategories();
            IEnumerable<TagViewModel> tags = this.service.GetAllTags();

            ManageViewModel model = new ManageViewModel
            {
                Posts = posts,
                Categories = categories,
                Tags = tags
            };

            return View(model);
        }

        [HttpGet]
        public ViewResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPost(CreateNewPostViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            this.service.AddNewPost(post);

            return RedirectToAction("Posts", "Blog");
        }

        [HttpGet]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditPostViewModel post = this.service.GetPostForEdit(id);
            
            return View(post);
        }

        [HttpPost]
        public ActionResult EditPost(EditPostViewModel post)
        {
            if (ModelState.IsValid)
            {
                this.service.EditPost(post);
                return RedirectToAction("Posts", "Blog");
            }
            return View(post);
        }

        [HttpGet]
        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeletePostViewModel post = this.service.GetPostForDelete(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePostConfirmed(int id)
        {
            try
            {
                this.service.RemovePost(id);
            }
            catch (Exception)
            {
                return RedirectToAction("DeletePost");
            }

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ViewResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            this.service.AddNewCategory(category);

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryViewModel post = this.service.GetCategory(id);

            return View(post);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                this.service.EditCategory(category);
                return RedirectToAction("Manage");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryViewModel category = this.service.GetCategory(id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        } 

        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            try
            {
                if (this.service.PostWithSuchCategory(id))
                {
                    CategoryViewModel category = this.service.GetCategory(id);

                    return View("Decline", category);
                }
                else
                {
                    this.service.RemoveCategory(id);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("DeleteCategory");
            }

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ViewResult AddTag()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTag(TagViewModel tag)
        {
            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            this.service.AddNewTag(tag);

            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult EditTag(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TagViewModel tag = this.service.GetTag(id);

            return View(tag);
        }

        [HttpPost]
        public ActionResult EditTag(TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                this.service.EditTag(tag);

                return RedirectToAction("Manage");
            }
            return View(tag);
        }

        [HttpGet]
        public ActionResult DeleteTag(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TagViewModel tag = this.service.GetTag(id);

            if (tag == null)
            {
                return HttpNotFound();
            }

            return View(tag);
        }

        [HttpPost, ActionName("DeleteTag")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTagConfirmed(int id)
        {
            try
            {
                if (this.service.PostWithSuchTag(id))
                {
                    TagViewModel tag = this.service.GetTag(id);

                    return View("DeclineDeleteTag", tag);
                }
                else
                {
                    this.service.RemoveTag(id);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("DeleteTag");
            }

            return RedirectToAction("Manage");
        }
    }
}