using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.postRepository.GetAll();
        }
        public async Task<Post> InsertPost(Post post)
        {
            var user = await _unitOfWork.userRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }
            if (post.Description.Contains("sexo"))
            {
                throw new Exception("Content not allowed");
            }
            await _unitOfWork.postRepository.Add(post);
            return post;
        }
        public async Task<bool> UpdatePost(Post post)
        {
            await _unitOfWork.postRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.postRepository.Delete(id);
            return true;
        }
    }
}
