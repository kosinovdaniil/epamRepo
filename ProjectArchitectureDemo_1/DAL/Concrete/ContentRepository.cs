using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class ContentRepository : IContentRepository
    {
        private readonly DbContext context;

        public ContentRepository(DbContext uow)
        {
            this.context = uow;
        }

        #region content
        public DalContent Create(DalContent e)
        {
            var content = e.ToOrmContent();
            content.Date = DateTime.Now;
            content.User = context.Set<User>().Find(e.UserId);
            return context.Set<Content>().Add(content).ToDalContent(); //CONTENT VALIDATION TODO
        }

        public void Delete(DalContent e)
        {
            var content = e.ToOrmContent();
            content = context.Set<Content>().Single(c => c.Id == content.Id);
            context.Set<Content>().Remove(content);
        }

        public IEnumerable<DalContent> GetAll()
        {
            return context.Set<Content>().ToList().Select(content => content.ToDalContent());
        }

        public DalContent GetById(int key)
        {
            return context.Set<Content>().FirstOrDefault(content => content.Id == key).ToDalContent();
        }

        public IEnumerable<DalContent> GetByPredicate(Expression<Func<DalContent, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalContent entity)
        {
            /*
            var updatedContent = entity.ToOrmContent();
            context.Set<Content>().Attach(updatedContent);

            var entry = context.Entry(updatedContent);
            if (updatedContent.Name != null && original.Name != updatedContent.Name)
                entry.Property(e => e.Name).IsModified = true;

            if (updatedContent.Description != null && original.Description != updatedContent.Description)
                entry.Property(e => e.Description).IsModified = true;

            if (entity.Rating != null && original.Rating != updatedContent.Rating)
                entry.Property(e => e.Rating).IsModified = true;

            if (updatedContent.VotedUsers.Any() && original.VotedUsers.SequenceEqual(updatedContent.VotedUsers) )
                entry.Property(e => e.VotedUsers).IsModified = true;*/
            var original = context.Set<Content>().First(u => u.Id == entity.Id);

            var updatedContent = entity.ToOrmContent();

            if (updatedContent.Name != null)
                original.Name = updatedContent.Name;
            if (updatedContent.Description != null)
                original.Description = updatedContent.Description;
        }

        public int UpVote(DalContent content, DalUser user)
        {
            var originalUser = context.Set<User>().First(u => u.Id == user.Id);
            var originalContent = context.Set<Content>().First(u => u.Id == content.Id);

            if (!originalUser.VotedPublications.Contains(originalContent))
            {
                originalContent.Rating++;
                originalUser.VotedPublications.Add(originalContent);
            }
            return (int)originalContent.Rating;
        }
        public int DownVote(DalContent content, DalUser user)
        {
            var originalUser = context.Set<User>().First(u => u.Id == user.Id);
            var originalContent = context.Set<Content>().First(u => u.Id == content.Id);

            if (!originalUser.VotedPublications.Contains(originalContent))
            {
                originalContent.Rating--;
                originalUser.VotedPublications.Add(originalContent);//TODO refactor
            }
            return (int)originalContent.Rating;
        }

        #endregion

        #region comment
        public void AddComment(DalComment e)
        {
            var comment = e.ToOrmComment();
            comment.Date = DateTime.Now;
            comment.User = context.Set<User>().Find(e.User.Id);
            comment.Content = context.Set<Content>().Find(e.ContentId);
            context.Set<Comment>().Add(comment);
        }

        public void DeleteComment(DalComment e)
        {
            var comment = e.ToOrmComment();
            comment = context.Set<Comment>().Single(c => c.Id == comment.Id);
            context.Set<Comment>().Remove(comment);
        }

        public void UpdateComment(DalComment comment)
        {
            var original = context.Set<Comment>().First(u => u.Id == comment.Id);

            var updatedComment = comment.ToOrmComment();
            if (updatedComment.Text != null)
                original.Text = updatedComment.Text;
        }

        public DalComment GetComment(int id)
        {
            return context.Set<Comment>().FirstOrDefault(comment => comment.Id == id).ToDalComment();
        }
        #endregion









    }
}