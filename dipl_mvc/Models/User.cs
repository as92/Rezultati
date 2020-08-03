//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dipl_mvc.Models
{
          using System;
          using System.Collections.Generic;
          using System.ComponentModel.DataAnnotations;

          public partial class User
          {
                    public int Id { get; set; }
                    [Required]
                    [Display(Name = "Korisni�ko ime: ")]
                    public string Username { get; set; }
                    [Required]
                    [DataType(DataType.Password)]
                    [Display(Name = "Lozinka: ")]
                    public string Password { get; set; }
                    [Required]
                    [DataType(DataType.EmailAddress)]
                    [Display(Name = "Email: ")]

                    public string Email { get; set; }
                    [Display(Name = "Registriraj se kao (korisnik ili admin): ")]
                    public bool Role { get; set; }
                    public string VCode { get; set; }
                    public Nullable<double> stanjeRacuna { get; set; }
          }
}