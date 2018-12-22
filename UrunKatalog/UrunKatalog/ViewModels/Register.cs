using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrunKatalog.ViewModels
{
    public class Register
    {
        [Required]
        [DisplayName("Ad")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Soyad")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Eposta adresinizi doğru giriniz")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Parola")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Parola Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}