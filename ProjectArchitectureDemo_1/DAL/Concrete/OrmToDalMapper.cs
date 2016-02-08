using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public static class OrmToDalMapper
    {
        public static User ToOrmUser(this DalUser e)
        {
            return new User()
            {
                Id = e.Id,
                Name = e.Name,
                Password = e.Password,
                Publications = e.Publications?.Select(x => ToOrmContent(x)).ToList(),
                Roles = e.Roles?.Select(x => ToOrmRole(x)).ToList()
            };
        }
        public static DalUser ToDalUser(this User e)
        {
            HashSet<int> VotedPubs = null;
            if (e.VotedPublications != null && e.VotedPublications.Any())
            {
                VotedPubs = new HashSet<int>(e.VotedPublications.Select(x => x.Id));
            }
            
            return new DalUser()
            {
                Id = e.Id,
                Name = e.Name,
                Password = e.Password,
                Publications = e.Publications?.Select(x => ToDalContent(x)).ToList(),
                Roles = e.Roles?.Select(x => ToDalRole(x)).ToList(),
                VotedPublicationsId = VotedPubs
            };
        }
        public static Role ToOrmRole(this DalRole e)
        {
            return new Role()
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name
            };
        }
        public static DalRole ToDalRole(this Role e)
        {
            return new DalRole()
            {
                Id = e.Id,
                Description = e.Description,
                Name = e.Name
            };
        }
        public static Content ToOrmContent(this DalContent e)
        {
            return new Content()
            {
                Date = e.Date,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Rating = e.Rating.Value,
                UrlToContent = e.UrlToContent,
                User = new User() { Id = e.UserId, Name = e.UserName },
                Comments = e.Comments?.Select(x => x.ToOrmComment()).ToList()
            };
        }
        public static DalContent ToDalContent(this Content e)
        {
            return new DalContent()
            {
                Date = e.Date,
                Description = e.Description,
                Id = e.Id,
                Name = e.Name,
                Rating = e.Rating,
                UrlToContent = e.UrlToContent,
                UserId = e.User.Id,
                UserName = e.User.Name,
                Comments = e.Comments?.Select(x => x.ToDalComment())?.ToList()
            };
        }
        public static Comment ToOrmComment(this DalComment e)
        {
            return new Comment()
            {
                Date = e.Date,
                Id = e.Id,
                Text = e.Text,
                User = e.User?.ToOrmUser(),
                Content = new Content() { Id = e.ContentId }
            };
        }
        public static DalComment ToDalComment(this Comment e)
        {
            return new DalComment()
            {
                Date = e.Date,
                Id = e.Id,
                Text = e.Text,
                User = new DalUser() {Id = e.User.Id, Name = e.User.Name },
                ContentId = e.Content.Id
            };
        }
    }
}
