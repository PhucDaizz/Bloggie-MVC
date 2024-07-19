﻿using Bloggie.web.Models.Domain;

namespace Bloggie.web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery = null,
            string? sortBy = null,
            string? sortDerection = null,
            int pageNumber = 1, 
            int pageSize = 100);

        Task<Tag> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid id);

        Task<int> CountAsync();
    }
}
