﻿using E_Learning.BL.DTO.User;
using E_Learning.BL.Managers.CartManager;
using E_Learning.BL.Managers.WishListManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WishListController : APIBaseController
    {
        private readonly IWishListManager _wishListManager;

        public WishListController(IWishListManager wishListManager)
        {
            _wishListManager = wishListManager;
        }

        [HttpGet("GetWishListItems")]

        public ActionResult<IEnumerable<CourseDTO>> GetWishListItems(int userId)
        {
            var wishListItems = _wishListManager.GetWishListItems(userId);
            return Ok(wishListItems);
        }

        [HttpDelete("RemoveWishListItem")]

        public ActionResult DeleteWishListItems(int userId,int courseId)
        {
            _wishListManager.RemoveItemFromWishList(userId, courseId);
            return NoContent();
        }
    }
}
