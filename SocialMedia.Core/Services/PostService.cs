using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }
        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.postRepository.GetById(id);
        }
        public IEnumerable<Post> GetPosts()
        {
            return _unitOfWork.postRepository.GetAll();
        }
        public async Task<Post> InsertPost(Post post)
        {
            var user = await _unitOfWork.userRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }
            if (post.Description.Contains("sexo"))
            {
                throw new BusinessException("Content not allowed");
            }
            var userPosts = await _unitOfWork.postRepository.GetPostsByUser(post.UserId);
            if(userPosts.Count()<10)
            {
                var lastPost = userPosts.OrderByDescending(x=>x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                    throw new BusinessException("You are not able to publish the post");
            }
            await _unitOfWork.postRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
            return post;
        }
        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.postRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.postRepository.Delete(id);
            return true;
        }
    }
}
