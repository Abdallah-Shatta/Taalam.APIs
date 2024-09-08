using E_Learning.BL.DTO.Cart;
using E_Learning.BL.DTO.Course;
using E_Learning.BL.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.BL.Managers.WishListManager
{
    public interface IWishListManager
    {
        IEnumerable<ReadCourseDTO> GetWishListItems(int userId);
        IEnumerable<ReadCourseDTO>  RemoveItemFromWishList(int userId, int courseId);
    }
}
