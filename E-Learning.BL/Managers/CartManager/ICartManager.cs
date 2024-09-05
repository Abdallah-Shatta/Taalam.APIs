using E_Learning.BL.DTO.Cart;
using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.CartManager
{
    public interface ICartManager
    {
        IEnumerable<ReadCourseDTO> GetCartItems(int userId);
        CartTotalDTO GetCartTotal(int userId);
        void RemoveItemFromCart(int userId, int courseId);
    }
}
