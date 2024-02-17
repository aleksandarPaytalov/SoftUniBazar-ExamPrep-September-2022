using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;

namespace SoftUniBazar.Contracts
{
    public interface IAdService
    {
        Task<ICollection<AdViewModel>> GetAllAdAsync();
        Task<ICollection<CategoryViewModel>> GetCategoryAsync();
        Task<Ad> AddNewAdAsync(AdNewViewModel model, string userId);
        Task<ICollection<AdViewModel>> GetAllAdsInCartAsync(string userId);
        Task AddToCartCollectionAsync(int id, string userId);
        Task RemoveFromCollectionAsync(int id, string userId);
        Task<AdNewViewModel?> GetAdForEditAsync(int id);
        Task EditAdAsync(AdNewViewModel model, int id);
    }
}
