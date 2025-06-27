using System.Linq;
using System.Web.Mvc;
using HotelManagement.Context;
using HotelManagement.Models;
using System.Web.Security;

public class AccountController : Controller
{
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: /Account/Register
    public ActionResult Register() => View();

    [HttpPost]
    public ActionResult Register(User user)
    {
        if (ModelState.IsValid)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(user);
    }

    // GET: /Account/Login
    public ActionResult Login() => View();

    [HttpPost]
    public ActionResult Login(User user)
    {
        var existing = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        if (existing != null)
        {
            Session["Username"] = existing.Username;
            return RedirectToAction("Index", "Home");
        }
        ViewBag.Message = "Invalid Username or Password";
        return View();
    }

    public ActionResult Logout()
    {
        Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
