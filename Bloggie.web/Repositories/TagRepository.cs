﻿using Bloggie.web.Data;
using Bloggie.web.Models.Domain;
using Bloggie.web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<int> CountAsync()
        {
            return await bloggieDbContext.Tags.CountAsync();
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                bloggieDbContext.Tags.Remove(existingTag);
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(string? searchQuery, string? sortBy, string? sortDerection, int pageNumber = 1, int pageSize = 100)
        {
            var query = bloggieDbContext.Tags.AsQueryable();

            // Filtering
            if(string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) ||
                                         x.DisplayName.Contains(searchQuery));
            }

            // Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDerection, "Desc",StringComparison.OrdinalIgnoreCase);

                if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                }

                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDesc ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
                }
            }

            // Pagination
            // Skip 0 take 5 -> Page 1 of 5 result
            // Skip 5 take next 5 -> Page 2 of 5 result
            var skipResult = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResult).Take(pageSize);

            return await query.ToListAsync();
            //return await bloggieDbContext.Tags.ToListAsync();
        }

        public  Task<Tag> GetAsync(Guid id)
        {
            return bloggieDbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
