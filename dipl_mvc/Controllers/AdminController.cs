using dipl_mvc.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dipl_mvc.Controllers
{
          public class AdminController : Controller
          {
                    private UsersEntities1 utakmice = new UsersEntities1();
                    private UsersEntities users = new UsersEntities();
                    public ActionResult DodajUtakmicu()
                    {
                              ViewBag.Users = users.Users.ToList();
                              return PartialView();
                    }
                    [HttpPost]
                    [ValidateAntiForgeryToken]
                    public ActionResult DodajUtakmicu(Utakmice game)
                    {
                              if (ModelState.IsValid)
                              {
                                        using (UsersEntities1 ue = new UsersEntities1())

                                        {
                                                  game.golDomacin = 0;
                                                  game.golGost = 0;
                                                  game.rezultat = " - ";

                                                  int result = DateTime.Compare(game.PocetakUtakmice, DateTime.Now);
                                                  if (result < 0)
                                                  {
                                                            ViewBag.UsporediMessage = game.PocetakUtakmice.ToString() + " je prije " + DateTime.Now.ToString();
                                                            ViewBag.UnsuccessfulMessage = "Utakmica nije dodana";
                                                            return PartialView();
                                                  }
                                                  else if (result == 0)
                                                  {
                                                            ViewBag.UsporediMessage=game.PocetakUtakmice.ToString() + " je u isto vrijeme kao " + DateTime.Now.ToString();
                                                            ViewBag.UnsuccessfulMessage = "Utakmica nije dodana";
                                                            return PartialView();
                                                  }
                                                  else
                                                  {
                                                            ViewBag.UsporediMessage = game.PocetakUtakmice.ToString() + " je poslije " + DateTime.Now.ToString();
                                                            ue.Utakmice.Add(game);
                                                            ue.SaveChanges();
                                                            ModelState.Clear();
                                                            ViewBag.SuccessMessage = "Utakmica dodana";
                                                            ViewBag.Users = users.Users.ToList();
                                                            return RedirectToAction("DodajUtakmicu","Admin");
                                                            //return PartialView();
                                                  }
                                                  
                                        }
                              }
                              else
                              {
                                        return PartialView();
                              }
                    }                                    
                    public ActionResult Utakmice()
                    {                              
                              Dictionary<int, string> rjecnik = new Dictionary<int, string>();                             
                              List<int> lista = new List<int>();
                              Dictionary<int, string> rjecnik2 = new Dictionary<int, string>();
                              List<int> lista2 = new List<int>();
                              List<int> listapauza = new List<int>();
                              foreach (var u in utakmice.Utakmice)
                              {
                                        if (u.status == "u tijeku")
                                        {
                                                  var uptime = DateTime.Now - u.PocetakUtakmice;
                                                  string output = string.Format("{0}:{1}", uptime.Minutes, uptime.Seconds);
                                                  rjecnik.Add(u.Id, output);
                                                  lista.Add(u.Id);
                                        }
                                        else if (u.status == "u tijeku2")
                                        {
                                                  var uptime = DateTime.Now - u.PocetakUtakmice;
                                                  string output = string.Format("{0}:{1}", uptime.Minutes, uptime.Seconds);
                                                  rjecnik2.Add(u.Id, output);
                                                  lista2.Add(u.Id);
                                        }
                                        else if (u.status == "Pauza")
                                        {
                                                  listapauza.Add(u.Id);
                                        }
                              }
                              ViewBag.Rjecnik = rjecnik;
                              ViewBag.Id = lista;
                              ViewBag.Rjecnik2 = rjecnik2;
                              ViewBag.Id2 = lista2;
                              ViewBag.Pauza = listapauza;
                              ViewBag.Users = users.Users.ToList();
                              return PartialView(utakmice.Utakmice.ToList());
                    }
                    [HttpPost]
                    public ActionResult Utakmice(Utakmice ut)
                    {                            
                        Utakmice u = utakmice.Utakmice.Where(p => p.Id == ut.Id).FirstOrDefault();
                        u.rezultat = ut.rezultat;
                        u.status = ut.status;
                        u.TipKraj = ut.TipKraj;
                        utakmice.SaveChanges();
                        ViewBag.Users = users.Users.ToList();
                        return PartialView(utakmice.Utakmice.ToList());
                    }
                    public ActionResult Gotovo()
                    {
                              ViewBag.Users = users.Users.ToList();
                              return PartialView(utakmice.Utakmice.ToList());                              
                    }                   
          }
}