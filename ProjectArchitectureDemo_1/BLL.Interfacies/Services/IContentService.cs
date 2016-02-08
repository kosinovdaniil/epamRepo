using System.Collections.Generic;
using BLL.Interface.Entities;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;

namespace BLL.Interface.Services
{
    public interface IContentService
    {
        ContentEntity GetContentById(int id);
        IEnumerable<ContentEntity> GetAllContents();
        ContentEntity Create(ContentEntity content);
        void Delete(ContentEntity content);
        void Update(ContentEntity content);
        void Create(CommentEntity comment);
        void Delete(CommentEntity comment);
        void Update(CommentEntity comment);
        CommentEntity GetComment(int id);
        int UpVoteContent(ContentEntity content, UserEntity user);
        int DownVoteContent(ContentEntity content, UserEntity user);
        IEnumerable<ContentEntity> GetContentByPredicate(Expression<Func<ContentEntity, bool>> f);
        IEnumerable<ContentEntity> GetTop(int num);
        //etc.
    }
}