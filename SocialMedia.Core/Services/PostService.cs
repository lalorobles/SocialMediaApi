using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
      
        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }
        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var posts = _unitOfWork.PostRepository.GetAll();
            if (filters.UserId!=null)
                posts = posts.Where(x => x.UserId == filters.UserId);
            if (filters.Date != null)
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            if (filters.Description != null)
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));

            var pagedPosts = PagedList<Post>.Create(posts,filters.PageNumber,filters.PageSize);
            return pagedPosts;
        }
        public async Task<Post> InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }
            if (post.Description.Contains("sexo"))
            {
                throw new BusinessException("Content not allowed");
            }
            var userPosts = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if(userPosts.Count()<10)
            {
                var lastPost = userPosts.OrderByDescending(x=>x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                    throw new BusinessException("You are not able to publish the post");
            }
            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
            return post;
        }
        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
