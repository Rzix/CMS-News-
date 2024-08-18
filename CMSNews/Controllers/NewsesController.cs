﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSNews.Service.Service;
using CMSNews.Models.Models;
using CMSNews.Models.Context;
using CMSNews.Models.ViewModels;
using CMSNews.App_Start;
using Auto_Mapper.App_Start;

namespace CMSNews.Controllers
{
    public class NewsesController : Controller
    {
        DbCMSNewsContext db = new DbCMSNewsContext();
        NewsGroupService _newsGroupService;
        NewsService _newsService;
        public NewsesController()
        {
            _newsGroupService = new NewsGroupService(db);
            _newsService = new NewsService(db);
        }
        public ActionResult ShowNewsGroup(int count)
        {
            var newsGroups = _newsGroupService.GetAll().Take(count);
            List<NewsGroupViewModel> newsGroupViewModels = AutoMapperConfig.mapper.Map<IEnumerable<NewsGroup>, List<NewsGroupViewModel>>(newsGroups);
            return PartialView(newsGroupViewModels);
        }

        public ActionResult LastNews(int co)
        {
            var lastNews = _newsService.GetAll().Where(t => t.IsActive).OrderByDescending(u => u.NewsId).Take(co);
            List<NewsViewModel> lastNewsViewModels = AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(lastNews);
            return PartialView(lastNewsViewModels);
        }

        public ActionResult BestNews(int co)
        {
            var lastNews = _newsService.GetAll().Where(t => t.IsActive).OrderByDescending(u => u.See).Take(co);
            List<NewsViewModel> lastNewsViewModels = AutoMapperConfig.mapper.Map<IEnumerable<News>, List<NewsViewModel>>(lastNews);
            return PartialView(lastNewsViewModels);
        }

        public ActionResult LastNews1()
        {
            var lastNews = _newsService.GetAll().Where(t => t.IsActive).LastOrDefault();
            NewsViewModel lastNewsViewModel = AutoMapperConfig.mapper.Map<News, NewsViewModel>(lastNews);
            return PartialView(lastNewsViewModel);
        }

        public ActionResult NewsDetailes(int id)
        {
            var news = _newsService.GetEntity(id);
            if (news == null || !news.IsActive)
            {
                return HttpNotFound();
            }
            news.See++;
            _newsService.Update(news);
            _newsService.Save();

            NewsViewModel newsViewModel= AutoMapperConfig.mapper.Map<News, NewsViewModel>(news);
            return View(newsViewModel);
        }


    }
}