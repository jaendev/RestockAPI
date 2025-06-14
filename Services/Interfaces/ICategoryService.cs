using RestockAPI.Models;

namespace RestockAPI.Services.Interfaces;

public class ICategoryService
{
    public Task<List<Category>> GetAllCategories() { throw new NotImplementedException(); }
    public Task<Category> GetCategoryById(int id) { throw new NotImplementedException(); }
    public Task<Category> CreateCategory(Category category) { throw new NotImplementedException(); }
    public Task<Category> UpdateCategory(Category category) { throw new NotImplementedException(); }
    public Task<bool> DeleteCategory(int id) { throw new NotImplementedException(); }
}