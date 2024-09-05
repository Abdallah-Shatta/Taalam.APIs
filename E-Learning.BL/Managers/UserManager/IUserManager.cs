using E_Learning.BL.DTO.User;
namespace E_Learning.BL.Managers.CategoryManager
{
    public interface IUserManager
    {
        InstructorDTO GetInstructorInfo(int id);
        bool EditUserProfile(EditUserProfileDTO editUserProfileDTO);
    }
}
