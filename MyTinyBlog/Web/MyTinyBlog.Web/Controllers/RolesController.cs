namespace MyTinyBlog.Web.Controllers
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Data.Models;
    using Services.Data;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Data;

    public class RolesController : Controller
    {

        private readonly AdminService service;

        // GET: /Roles/Index
        public RolesController()
        {
            this.service = new AdminService();
        }

        public ActionResult Index()
        {
            IEnumerable<IdentityRole> roles = this.service.GetAllRoles();

            return View(roles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                this.service.CreateRole(collection["RoleName"]);


                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string roleName)
        {
            this.service.DeleteRole(roleName);

            return RedirectToAction("Index");
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            IdentityRole thisRole = this.service.GetRole(roleName);

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                this.service.EditRole(role);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            IEnumerable<SelectListItem> list = this.service.GetRolesForDropDown();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            ApplicationUser user = this.service.AddRoleToUser(userName);
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var idResult = um.AddToRole(user.Id, roleName);

            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = this.service.GetRolesForDropDown();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                ApplicationUser user = this.service.AddRoleToUser(userName);
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                ViewBag.RolesForThisUser = um.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = this.service.GetRolesForDropDown();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = this.service.AddRoleToUser(userName);

            if (um.IsInRole(user.Id, roleName))
            {
                um.RemoveFromRole(user.Id, roleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = this.service.GetRolesForDropDown();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }
    }
}