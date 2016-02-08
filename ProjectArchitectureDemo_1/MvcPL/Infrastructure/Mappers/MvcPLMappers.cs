using BLL.Interface.Entities;
using MvcPL.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserEntity ToBllUser(this UserViewModel e)
        {
            return new UserEntity()
            {
                Id = e.Id,
                Name = e.Name,
                Roles = e.Roles.Select(x=>x.ToBllRole()).ToList()                               
            };
        }
        public static UserViewModel ToUserViewModel(this UserEntity e)
        {
            if (e.Publications == null)
                e.Publications = new List<ContentEntity>();
            return new UserViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                AmountOfPublications = e.Publications.Count,
                AverageRating = e.Publications.Average(x => x.Rating),
                Publications = e.Publications?.Select(x => x.ToContentViewModel()).ToList(),
                Roles = e.Roles?.Select(x=>x.ToRoleViewModel()).ToList()               
            };
        }
        public static RoleViewModel ToRoleViewModel(this RoleEntity e)
        {
            return new RoleViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                IsAssigned = true
            };
        }
        public static RoleEntity ToBllRole(this RoleViewModel e)
        {
            return new RoleEntity()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Name
            };
        }
        public static ContentEntity ToBllContent(this ContentViewModel e)
        {
            if (e.User == null)
                e.User = new UserViewModel() { Id = 0 };
            return new ContentEntity()
            {
                Date = e.Date,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Rating = e.Rating,
                UrlToContent = e.UrlToContent,
                UserId = e.User.Id,
                UserName = e.User.Name,
                Comments = e.Comments?.Select(x => x.ToBllComment()).ToList()
            };
        }
        public static ContentViewModel ToContentViewModel(this ContentEntity e)
        {
            return new ContentViewModel()
            {
                Date = e.Date,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Rating = e.Rating.Value,
                UrlToContent = e.UrlToContent,
                User = new UserViewModel() { Id = e.UserId, Name = e.UserName},
                Comments = e.Comments.Select(x=>x.ToCommentViewModel()).ToList()

            };
        }
        public static CommentEntity ToBllComment(this CommentViewModel e)
        {
            return new CommentEntity()
            {
                Date = e.Date,
                Id = e.Id,
                Text = e.Text,
                User = e.User.ToBllUser(),
                ContentId = e.ContentId
            };
        }
        public static CommentViewModel ToCommentViewModel(this CommentEntity e)
        {
            return new CommentViewModel()
            {
                Date = e.Date,
                Id = e.Id,
                Text = e.Text,
                User = e.User.ToUserViewModel(),
                ContentId = e.ContentId
            };
        }
    }
}