using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechDemo.Core.Infrastructure.Services
{
    public class CreateUserVm
    {
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Выберите фотографию")]
        [Display(Name = "Фотография")]
        public IFormFile Photo { get; set; }
    }
}