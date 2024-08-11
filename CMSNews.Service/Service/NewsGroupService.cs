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
    public class NewsGroupService : GenericService<NewsGroup>, INewsGroupService
    {
        public INewsGroupRepository _newsGroupRepository;
        public NewsGroupService(DbCMSNewsContext context) : base(context)
        {
            _newsGroupRepository = new NewsGroupRepository(context);
        }

        public int NextNewsGroupId()
        {
            int max = 1;
            var newGroups = _newsGroupRepository.GetAll().ToList();
            if (newGroups.Count() > 0)
            {
                max = newGroups.Max(t => t.NewGroupId) + 1;
            }
            return max;
        }
    }
}
