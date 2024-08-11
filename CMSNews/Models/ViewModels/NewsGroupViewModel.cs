using CMSNews.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMSNews.Models.ViewModels
{
    public class NewsGroupViewModel
    {
        [Display(Name ="کد گروه خبری ")]
        public int NewGroupId { get; set; }
        [Required]
        [MaxLength(200)]
        [Display(Name = "عنوان گروه خبری")]
        public string NewGroupTitle { get; set; }
        [MaxLength(100)]
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        public IEnumerable<News> Newses { get; set; }
    }
}