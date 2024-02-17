using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Contracts;
using SoftUniBazar.Data;
using SoftUniBazar.Data.DataConstants;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;
using System.Security.Claims;

namespace SoftUniBazar.Services
{
    public class AdService : IAdService
    {
        private readonly BazarDbContext _context;

        public AdService(BazarDbContext context)
        {
            _context = context;
        }


        public async Task<ICollection<AdViewModel>> GetAllAdAsync()
        {
            return await _context.Ads
               .AsNoTracking()
               .Select(a => new AdViewModel()
               {
                   Id = a.Id, 
                   Category = a.Category.Name,
                   CreatedOn = a.CreatedOn.ToString(ValidationConstants.DataFormat),
                   Description = a.Description,
                   ImageUrl = a.ImageUrl,
                   Name = a.Name,
                   Owner = a.Owner.UserName,
                   Price = a.Price
               })
               .ToListAsync();
        }

        public async Task<ICollection<CategoryViewModel>> GetCategoryAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<Ad> AddNewAdAsync(AdNewViewModel model, string userId)
        {
            var categories = await GetCategoryAsync();
            var advertise = new Ad()
            {
                OwnerId = userId,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now
            };

            await _context.Ads.AddAsync(advertise);
            await _context.SaveChangesAsync();

            return (advertise);
        }

        public async Task<ICollection<AdViewModel>> GetAllAdsInCartAsync(string userId)
        {
            return await _context.AdsBuyers
                .Where(ep => ep.BuyerId == userId)
                .AsNoTracking()
                .Select(ep => new AdViewModel()
                {
                    Id = ep.AdId,
                    Category = ep.Ad.Category.Name,
                    CreatedOn = ep.Ad.CreatedOn.ToString(ValidationConstants.DataFormat),
                    Description = ep.Ad.Description,
                    ImageUrl = ep.Ad.ImageUrl,
                    Name = ep.Ad.Name,
                    Owner = ep.Ad.Owner.UserName,
                    Price = ep.Ad.Price
                })
                .ToListAsync();
        }

        public async Task AddToCartCollectionAsync(int id, string userId)
        {
            var advertise = await _context.Ads
                .Where(a => a.Id == id)
                .Select(a => new AdViewModel()
                {
                    Id = a.Id,
                    Category = a.Category.Name,
                    CreatedOn = a.CreatedOn.ToString(ValidationConstants.DataFormat),
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Name = a.Name,
                    Owner = a.Owner.UserName,
                    Price = a.Price
                })
                .FirstOrDefaultAsync();

            if (advertise == null)
            {
                throw new ArgumentException("Invalid ad ID");
            }
            
            var checkIfAlreadyAdded = await _context.AdsBuyers
                .Where(ab => ab.AdId == advertise.Id && ab.BuyerId == userId)
                .FirstOrDefaultAsync();

            if (checkIfAlreadyAdded == null)
            {
                var ad = new AdBuyer()
                {
                    BuyerId = userId,
                    AdId = id
                };

                await _context.AdsBuyers.AddAsync(ad);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCollectionAsync(int id, string userId)
        {
             var model = await _context.AdsBuyers
                .Where(ab => ab.AdId == id && ab.BuyerId == userId)
                .FirstOrDefaultAsync();
            
            if (model != null)
            {
                _context.AdsBuyers.Remove(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AdNewViewModel?> GetAdForEditAsync(int id)
        {
            var categories = await GetCategoryAsync();

            var model = await _context.Ads
                .Where(a => a.Id == id)
                .Select(a => new AdNewViewModel()
                {
                    CategoryId = a.CategoryId,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl,
                    Name = a.Name,
                    Price = a.Price,
                    Categories = categories
                })
                .FirstOrDefaultAsync();

            return model;
        }

        public async Task EditAdAsync(AdNewViewModel model, int id)
        {
            var ad = await _context.Ads
                .FindAsync(id);
            if (ad == null)
            {
                throw new ArgumentException("There is not ad for editing");
            }
            
            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

            await _context.SaveChangesAsync();
        }
    }
}
