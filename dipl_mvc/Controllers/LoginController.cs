using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dipl_mvc.Models;

namespace dipl_mvc.Controllers
{
          public class LoginController : Controller
          {
                    // GET: Login
                    public ActionResult Index()
                    {
                              if (Session["UserID"] != null)
                              {
                                        return RedirectToAction("Index", "Home");
                              }
                              else
                              {
                                        return PartialView();
                              }
                    }
                    [ValidateAntiForgeryToken]
                    [HttpPost]
                    public ActionResult Index(User user)
                    {
                              if (Session["UserID"] == null)
                              {
                                        try
                                        {
                                                  using (UsersEntities ue = new UsersEntities())
                                                  {
                                                            var usr = ue.Users.Where(x => x.Username == user.Username).FirstOrDefault();
                                                            if (usr != null)
                                                            {
                                                                      var hashCode = usr.VCode;
                                                                      var encodingPasswordString = Helper.EncodePassword(user.Password, hashCode);
                                                                      var usr2 = ue.Users.Where(x => x.Username == user.Username && usr.Password.Equals(encodingPasswordString)).FirstOrDefault();
                                                                      if (usr2 != null)
                                                                      {
                                                                                Session["UserID"] = usr2.Id.ToString();
                                                                                Session["Username"] = usr2.Username.ToString();

                                                                                if (usr2.Role == true)
                                                                                {
                                                                                          return RedirectToAction("DodajUtakmicu", "Admin");
                                                                                }
                                                                                else
                                                                                {

                                                                                          return RedirectToAction("Index", "Home");

                                                                                }
                                                                      }
                                                                      else
                                                                      {
                                                                                ViewBag.UnsuccessfulMessage = "Unijeli ste krivu lozinku";
                                                                                return PartialView();
                                                                      }
                                                            }
                                                            else
                                                            {
                                                                      ViewBag.UnsuccessfulMessage = "Unijeli ste krivo korisničko ime";
                                                                      return PartialView();
                                                            }
                                                  }
                                        }
                                        catch (Exception e)
                                        {
                                                  ViewBag.ErrorMessage = " Error!!!";
                                                  return PartialView();
                                        }
                              }
                              else
                              {
                                        ViewBag.UnsuccessfulMessage = "Drugi korisnik je ulogiran, zatvorite trenutnu sesiju ili koristite drugi preglednik";
                                        return PartialView();
                              }

                    }
          }
}