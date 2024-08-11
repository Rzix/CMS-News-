using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class UserViewModel
    {
        [Display(Name = "کد کاربر")]
        public int UserId { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "شماره تماس(نام کاربری)")]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "وضعیت کاربر")]
        public bool IsActive { get; set; }

        public virtual IEnumerable<News> Newses { get; set; }
    }
}