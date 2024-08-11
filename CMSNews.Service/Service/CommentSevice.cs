using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNews.Models.Context;
using CMSNews.Models.Models;
using CMSNews.Repository.Repository;
namespace CMSNews.Service.Service
{
    public class CommentSevice : GenericService<Comment>, ICommentSevice
    {
        public CommentSevice(DbCMSNewsContext context) : base(context)
        {
        }


    }
}
