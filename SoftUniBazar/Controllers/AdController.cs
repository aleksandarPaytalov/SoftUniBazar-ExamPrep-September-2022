using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Contracts;
using SoftUniBazar.Data;
using SoftUniBazar.Data.DataConstants;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly IAdService adService;

        public AdController(IAdService _adService)
        {
            adService = _adService;
        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await adService.GetAllAdAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await adService.GetCategoryAsync();
        
            var model = new AdNewViewModel()
            {
                Categories = categories
            }; 
        
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AdNewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
        
            var userId = GetUserId();
            var ad = await adService.AddNewAdAsync(model, userId);            
        
            return RedirectToAction(nameof(All));
        }
        
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            var model = await adService.GetAllAdsInCartAsync(userId);
        
            return View(model);
        
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();
            await adService.AddToCartCollectionAsync(id, userId);
        
            return RedirectToAction(nameof(Cart));
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = GetUserId();
            await adService.RemoveFromCollectionAsync(id, userId);
        
            return RedirectToAction(nameof(All));
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await adService.GetAdForEditAsync(id);
            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(AdNewViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await adService.EditAdAsync(model, id);
            
            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
