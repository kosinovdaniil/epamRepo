using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Linq;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static UserEntity ToBllUser(this DalUser e)
        {
            return new UserEntity()
            {
                Id = e.Id,
                Name = e.Name,
                Password = e.Password,
                Publications = e.Publications?.Select(x => ToBllContent(x)).ToList(),
                Roles = e.Roles?.Select(x => ToBllRole(x)).ToList(),
                VotedPublicationsId = e.VotedPublicationsId
            };
        }
        public static DalUser ToDalUser(this UserEntity e)
        {
            return new DalUser()
            {
                Id = e.Id,
                Name = e.Name,
                Password = e.Password,
                Publications = e.Publications?.Select(x => ToDalContent(x)).ToList(),
                Roles = e.Roles?.Select(x => ToDalRole(x)).ToList(),
                VotedPublicationsId = e.VotedPublicationsId
            };
        }
        public static RoleEntity ToBllRole(this DalRole e)
        {
            return new RoleEntity()
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name
            };
        }
        public static DalRole ToDalRole(this RoleEntity e)
        {
            return new DalRole()
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name
            };
        }
        public static ContentEntity ToBllContent(this DalContent e)
        {
            return new ContentEntity()
            {
                Date = e.Date,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Rating = e.Rating,
                UrlToContent = e.UrlToContent,
                UserId = e.UserId,
                UserName = e.UserName,
                Comments = e.Comments?.Select(x=>x.ToBllComment()).ToList()
            };
        }
        public static DalContent ToDalContent(this ContentEntity e)
        {
            return new DalContent()
            {
                Date = e.Date,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Rating = e.Rating,
                UrlToContent = e.UrlToContent,
                UserId = e.UserId,
                UserName = e.UserName,
                Comments = e.Comments?.Select(x=>x.ToDalComment()).ToList()
                
            };
        }
        public static CommentEntity ToBllComment(this DalComment e)
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
        public static DalComment ToDalComment(this CommentEntity e)
        {
            return new DalComment()
            {
                Date = e.Date,
                Id = e.Id,
                Text = e.Text,
                User = e.User?.ToDalUser(),
                ContentId = e.ContentId
            };
        }
    }
}
