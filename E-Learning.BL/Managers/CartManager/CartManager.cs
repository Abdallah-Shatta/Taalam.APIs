﻿using E_Learning.BL.DTO.Cart;
using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.User;
using E_Learning.DAL.UnitOfWorkDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.CartManager
{
    public class CartManager:ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ReadCourseDTO> GetCartItems(int userId)
        {
            return _unitOfWork.CartRepository.GetCartItemsByUserId(userId)
                .Select(c => new ReadCourseDTO
                {
                    Id = c.Id,
                    Title = c.Course.Title,
                    Price = c.Course.Price,
                    CoverPicture =c.Course.CoverPicture,
                });
        }



        public void RemoveItemFromCart(int userId, int courseId)
        {
            var cartItem = _unitOfWork.CartRepository.GetCartItem(userId, courseId);
            if (cartItem != null)
            {
                _unitOfWork.CartRepository.Delete(cartItem);
                _unitOfWork.SaveChanges();
            }
        }
        public CartTotalDTO GetCartTotal(int userId)
        {
            return new CartTotalDTO
            {
                TotalPrice = _unitOfWork.CartRepository.GetCartTotalByUserId(userId)
            };
        }



    }
}

