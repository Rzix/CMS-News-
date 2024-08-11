using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class CommentViewModel
    {
        [Display(Name="کد نظر")]
        public int CommentId { get; set; }
        [Required]
        [MaxLength(200)]
        [Display(Name="متن نظر")]
        [DataType(DataType.MultilineText)]
        public string CommentText { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "نام کاربر")]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name="ایمیل")]
        public string Email { get; set; }
        [Display(Name="تارخ ثبت نظر")]
        public DateTime Registerdate { get; set; }
        [Required]
        [Display(Name = "وضعیت نظر")]
        public bool IsActive { get; set; }
        [Display(Name = "کد خبر")]
        [Required]
        public int NewsId { get; set; }

        public virtual News News { get; set; }
    }
}