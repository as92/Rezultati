using dipl_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dipl_mvc.Controllers
{
          public class UsersController : Controller
          {
                    // GET: Users
                    public ActionResult Registracija()
                    {
                              //User user = new User();
                              return PartialView();
                    }
                    [HttpPost]
                    public ActionResult Registracija(User user)
                    {
                              if (user.Role == false)
                              {
                                        
                                        user.stanjeRacuna = 500;
                                        using (UsersEntities ue = new UsersEntities())
                                        {
                                                  if (ue.Users.Any(x => x.Username == user.Username))
                                                  {
                                                            ViewBag.DuplicateMessage = "Korisničko ime već postoji!";
                                                            return PartialView();
                                                  }
                                                  else
                                                  {
                                                            var keyNew = Helper.GeneratePassword(10);
                                                            var password = Helper.EncodePassword(user.Password, keyNew);
                                                            user.Password = password;
                                                            user.VCode = keyNew;
                                                            ue.Users.Add(user);
                                                            ue.SaveChanges();
                                                            ModelState.Clear();
                                                            ViewBag.SuccessMessage = "Registracija uspješna";
                                                            return PartialView();
                                                  }
                                                  
                                        }
                              }
                              else
                              {
                                        using (UsersEntities ue = new UsersEntities())
                                        {
                                                  if (ue.Users.Any(x => x.Username == user.Username))
                                                  {
                                                            ViewBag.DuplicateMessage = "Korisničko ime već postoji!";
                                                            return PartialView();
                                                  }
                                                  else
                                                  {
                                                            var keyNew = Helper.GeneratePassword(10);
                                                            var password = Helper.EncodePassword(user.Password, keyNew);
                                                            user.Password = password;
                                                            user.VCode = keyNew;
                                                            ue.Users.Add(user);
                                                            ue.SaveChanges();
                                                            ModelState.Clear();
                                                            ViewBag.SuccessMessage = "Registracija uspješna";
                                                            return PartialView();
                                                  }
                                                  
                                        }

                              }
                    
                    }
          }
}