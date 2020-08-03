using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dipl_mvc.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace dipl_mvc.Controllers
{
          public class HomeController : Controller
          {
                    private UsersEntities1 utakmice = new UsersEntities1();
                    private UsersEntities users = new UsersEntities();
                    private ListiciEntities l = new ListiciEntities();
                    public ActionResult Index()
                    {
                              if (Session["UserID"] != null)
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
                              else
                                        return RedirectToRoute("Login");
                    }
                    public ActionResult Kladionica()
                    {
                              if (Session["UserID"] != null)
                              {
                                        ViewBag.Users = users.Users.ToList();
                                        return PartialView(utakmice.Utakmice.ToList());
                              }
                              else
                                        return RedirectToRoute("Login");
                    }
                    [HttpPost]
                    public ActionResult Kladionica(User user)
                    {
                              if (Session["UserID"] != null)
                              {
                                        User u = users.Users.Where(p => p.Id == user.Id).FirstOrDefault();
                                        u.stanjeRacuna = user.stanjeRacuna;
                                        users.SaveChanges();
                                        ViewBag.Users = users.Users.ToList();
                                        return PartialView(utakmice.Utakmice.ToList());
                              }
                              else
                                        return RedirectToRoute("Login");
                    }
                    public ActionResult Logout()
                    {
                              Session.Abandon();
                              return RedirectToAction("Index", "Login");
                    }
                    public ActionResult Listic()
                    {
                              
                              if (Session["UserID"] != null)
                              {
                                        ViewBag.Users = users.Users.ToList();
                                        return PartialView(l.Listici.ToList());
                              }
                              else
                                        return RedirectToRoute("Login");
                    }
                    [HttpPost]
                    public ActionResult Listic(Utakmice ut)
                    {
                              
                              if (Session["UserID"] != null)
                              {
                                        foreach (var item in l.Listici)
                                        {
                                                  if (item.Provjera == true)
                                                  {
                                                            continue;
                                                  }
                                                  else if (item.StatusListica == true)
                                                  {
                                                            continue;
                                                  }
                                                  else if (item.StatusListica == null || item.StatusListica == false)
                                                  {
                                                            var objekt = JObject.Parse(item.Tekst);
                                                            var d = objekt["utakmice"].Count();
                                                            for (int i = 0; i < d - 1; i++)
                                                            {
                                                                      var b = Convert.ToInt32(objekt["utakmice"][i]["id"]);
                                                                      var c = objekt["utakmice"][i]["naziv"];
                                                                      var t = objekt["utakmice"][i]["koeficijent"];
                                                                      var r = objekt["utakmice"][i]["tip"];
                                                                      var e = objekt["utakmice"][i]["status"];
                                                                      if (ut.Id == b)
                                                                      {
                                                                                var tip = ut.TipKraj.Split(';');
                                                                                var tip1 = tip[0];
                                                                                var tip2 = tip[1];
                                                                                var tip3 = tip[2];
                                                                                if (tip1 == (r.ToString()) || tip2 == (r.ToString()) || tip3 == (r.ToString()))
                                                                                {
                                                                                          objekt["utakmice"][i]["status"] = "true";
                                                                                }
                                                                                else
                                                                                {
                                                                                          objekt["utakmice"][i]["status"] = "false";
                                                                                          item.StatusListica = false;
                                                                                }
                                                                      }
                                                            }
                                                            bool statuslistica = true;
                                                            bool pregledan = true;
                                                            for (int i = 0; i < d - 1; i++)
                                                            {
                                                                      if (objekt["utakmice"][i]["status"].ToString() == "false" || objekt["utakmice"][i]["status"].ToString() == "null")
                                                                      {
                                                                                statuslistica = false;
                                                                      }
                                                            }
                                                            for (int i = 0; i < d - 1; i++)
                                                            {
                                                                      if (objekt["utakmice"][i]["status"].ToString() == "null")
                                                                      {
                                                                                pregledan = false;
                                                                      }
                                                            }
                                                            if (pregledan == true)
                                                            {
                                                                      item.Provjera = true;
                                                            }
                                                            var json = JsonConvert.SerializeObject(objekt);
                                                            item.Tekst = json;
                                                            if (statuslistica == true)
                                                            {
                                                                      item.StatusListica = true;
                                                                      string korisnik = Convert.ToString(objekt["utakmice"][d - 1]["korisnik"]);
                                                                      var isplata = Convert.ToDouble(objekt["utakmice"][d - 1]["isplati"]);
                                                                      User user = users.Users.Where(p => p.Username == korisnik).FirstOrDefault();
                                                                      user.stanjeRacuna += isplata;
                                                                      users.SaveChanges();
                                                            }
                                                  }
                                        }
                                        l.SaveChanges();
                                        ViewBag.Users = users.Users.ToList();
                                        return PartialView(l.Listici.ToList());
                              }
                              else
                              {
                                        return RedirectToRoute("Login");
                                        }
                    }
                    public ActionResult Zavrseno()
                    {
                              if (Session["UserID"] != null)
                              {
                                        ViewBag.Users = users.Users.ToList();
                                        return PartialView(utakmice.Utakmice.ToList());
                              }
                              else
                              {
                                        
                                        return RedirectToRoute("Login");
                              }
                    }
          }
}
