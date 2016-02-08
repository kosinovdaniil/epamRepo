using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BLL.Services
{
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork uow;
        private readonly IContentRepository contentRepository;

        public ContentService(IUnitOfWork uow, IContentRepository repository)
        {
            this.uow = uow;
            this.contentRepository = repository;
        }

        #region content
        public IEnumerable<ContentEntity> GetTop(int num)
        {
            Lucene.LuceneSearch.ClearLuceneIndex();
            Lucene.LuceneSearch.AddUpdateLuceneIndex(contentRepository.GetAll().Select(x=>x.ToBllContent()));
            return contentRepository.GetAll().OrderByDescending(e => e.Rating).Take(num).Select(e => e.ToBllContent());
        }

        public ContentEntity GetContentById(int id)
        {
            return contentRepository.GetById(id).ToBllContent();
        }

        public IEnumerable<ContentEntity> GetAllContents()
        {
            
            return contentRepository.GetAll().Select(x => x.ToBllContent());
        }

        public ContentEntity Create(ContentEntity content)
        {
            if (content.UrlToContent.Contains("youtube.com"))
            {
                content.UrlToContent = GetYouTubeID(content.UrlToContent);
            }
            var temp = contentRepository.Create(content.ToDalContent());
            uow.Commit();
            Lucene.LuceneSearch.AddUpdateLuceneIndex(content);
            return temp.ToBllContent();
        }
        private string GetYouTubeID(string youTubeUrl)
        {
            Match regexMatch = Regex.Match(youTubeUrl, "^[^v]+v=(.{11}).*",
                               RegexOptions.IgnoreCase);
            if (regexMatch.Success)
            {
                return "http://www.youtube.com/v/" + regexMatch.Groups[1].Value +
                       "&hl=en&fs=1";
            }
            return youTubeUrl;
        }

        public void Delete(ContentEntity content)
        {
            contentRepository.Delete(content.ToDalContent());
            uow.Commit();
            Lucene.LuceneSearch.ClearLuceneIndexRecord(content.Id);
        }

        public void Update(ContentEntity content)
        {
            contentRepository.Update(content.ToDalContent());
            uow.Commit();
            Lucene.LuceneSearch.AddUpdateLuceneIndex(content);
        }

        public IEnumerable<ContentEntity> GetContentByPredicate(Expression<Func<ContentEntity, bool>> f)
        {
            throw new NotImplementedException();
        }

        public int UpVoteContent(ContentEntity content, UserEntity user)
        {
            var temp = contentRepository.UpVote(content.ToDalContent(), user.ToDalUser());
            uow.Commit();
            return temp;
        }

        public int DownVoteContent(ContentEntity content, UserEntity user)
        {
            var temp = contentRepository.DownVote(content.ToDalContent(), user.ToDalUser());
            uow.Commit();
            return temp;
        }
        #endregion 

        #region comments
        public void Create(CommentEntity comment)
        {
            contentRepository.AddComment(comment.ToDalComment());
            uow.Commit();
        }

        public void Delete(CommentEntity comment)
        {
            contentRepository.DeleteComment(comment.ToDalComment());
            uow.Commit();
        }

        public void Update(CommentEntity comment)
        {
            contentRepository.UpdateComment(comment.ToDalComment());
            uow.Commit();
        }

        public CommentEntity GetComment(int id)
        {
            return contentRepository.GetComment(id).ToBllComment();
        }


        #endregion


    }
}
