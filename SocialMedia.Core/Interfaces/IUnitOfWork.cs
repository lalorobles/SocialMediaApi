﻿using SocialMedia.Core.Entities;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository postRepository { get; }
        IRepository<User> userRepository { get; }
        IRepository<Comment> commentRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
