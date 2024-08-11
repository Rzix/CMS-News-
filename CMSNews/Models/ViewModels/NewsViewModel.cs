using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSNews.Models.ViewModels
{
    public class NewsViewModel
    {
        [Display(Name ="کد خبر")]
        public int NewsId { get; set; }
        [MaxLength(300)]
        [Display(Name = "عنوان خبر")]
        public string NewsTitle { get; set; }
        [Display(Name = "توضیحات خبر")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Description { get; set; }
        [MaxLength(100)]
        [Display(Name = "تصویر خبر")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ درج خبر")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }
        [Display(Name = "تعداد بازدید ")]
        public int See { get; set; }
        [Display(Name = "تعداد لایک")]
        public int Like { get; set; }
        [Display(Name = "گروه خبری")]
        public int NewsGroupId { get; set; }
        [Display(Name = "کاربر ثبت کننده خبر")]
        public int UserId { get; set; }

        public User User { get; set; }
        public NewsGroup NewsGroup { get; set; }
        public IEnumerable<Comment> Coments { get; set; }
    }
}