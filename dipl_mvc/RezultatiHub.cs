using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using dipl_mvc.Models;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Web.Hosting;
using System.Timers;
using System.ComponentModel;

namespace dipl_mvc
{
          public class RezultatiHub : Hub
          {
                    private UsersEntities1 utakmice = new UsersEntities1();                                                                                         
                    public void Share(string tekst)
                    {                              
                              Listici l = new Listici();                             
                              l.Tekst = tekst;                              
                              using (ListiciEntities listici = new ListiciEntities())
                              {
                                                  listici.Listici.Add(l);
                                                  listici.SaveChanges();
                              }
                             Clients.All.getTiket(tekst);
                    }
                    public void Ukloni(int i,string s)
                    {
                       Utakmice u = utakmice.Utakmice.Where(p => p.Id == i).FirstOrDefault();
                       u.status = s;
                       utakmice.SaveChanges();                       
                       Clients.All.ukloni(i);
                    }                  
                    public void Pocetak(int i)
                    {
                              Utakmice game = utakmice.Utakmice.Where(p => p.Id == i).FirstOrDefault();
                              var uptime = DateTime.Now - game.PocetakUtakmice;
                              string output = string.Format("{0}:{1}", uptime.Minutes, uptime.Seconds);
                              var a = uptime.Minutes;
                              var b = uptime.Seconds;
                              Clients.All.pocetak(i, a, b);
                    }
                    public void Pauza(int id)
                    {
                              Utakmice game = utakmice.Utakmice.Where(p => p.Id == id).FirstOrDefault();
                              game.status = "Pauza";                              
                              utakmice.SaveChanges();
                              Clients.All.pauza(id);
                    }
                    public void Nastavi(int id)
                    {
                              Utakmice game = utakmice.Utakmice.Where(p => p.Id == id).FirstOrDefault();
                              game.status = "u tijeku2";                              
                              var uptime = DateTime.Now - game.PocetakUtakmice;
                              string output = string.Format("{0}:{1}", uptime.Minutes, uptime.Seconds);
                              var a = uptime.Minutes;
                              var b = uptime.Seconds;
                              utakmice.SaveChanges();
                              Clients.All.nastavi(id,a,b);
                    }
                    public void Kraj(int id,string tekst)
                    {
                              Clients.All.kraj(id, tekst);
                    }                   
                    public void Score(int id,int gol, string strana)
                    {
                          Utakmice game = utakmice.Utakmice.Where(p=>p.Id == id).FirstOrDefault();
                          if (strana=="domacin")
                          utakmice.Utakmice.Where(p => p.Id == id).FirstOrDefault().golDomacin = gol;
                          else
                          utakmice.Utakmice.Where(p => p.Id == id).FirstOrDefault().golGost = gol;
                          utakmice.SaveChanges();
                          Clients.All.getScore(id, gol, strana);
                    }                   
          }
}