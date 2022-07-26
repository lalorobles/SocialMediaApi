﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Comment> _commentRepository;

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }
        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);

        public IRepository<Comment> CommentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

        public void Dispose()
        {
            if(_context != null)
                _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await  _context.SaveChangesAsync();
        }
    }
}
