﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly CSGOContext context;

        public PostRepository(CSGOContext context)
        {
            this.context = context;
        }

        public IQueryable<Post> GetAll()
        {
            return context.Posts.AsQueryable();
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await context.Posts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetAllCountAsync()
        {
            return await context.Posts.CountAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await context.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> AddAsync(Post post)
        {
            var createdPost = await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            return createdPost.Entity;
        }

        public async Task UpdatedAsync(Post post)
        {
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Post post)
        {
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}