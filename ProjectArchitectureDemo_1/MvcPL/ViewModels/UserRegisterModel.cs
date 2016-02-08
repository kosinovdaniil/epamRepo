using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Вы не ввели логин")]
        [StringLength(10, ErrorMessage = "Логин не может привышать 10 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле пароль обязательно для заполнения")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Следует указать пароль от 5 до 20 символов")]
        [Compare("PasswordConfirm", ErrorMessage = "Пароли не совпадают")]
        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

    }
}