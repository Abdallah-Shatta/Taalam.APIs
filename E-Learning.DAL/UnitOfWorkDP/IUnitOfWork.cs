using E_Learning.DAL.Repositories.AnswerRepository;
using E_Learning.DAL.Repositories.CartRepository;
using E_Learning.DAL.Repositories.CategoryRepository;
using E_Learning.DAL.Repositories.CourseRepository;
using E_Learning.DAL.Repositories.EnrollmentRepository;
using E_Learning.DAL.Repositories.LessonRepository;
using E_Learning.DAL.Repositories.QuestionRepository;
using E_Learning.DAL.Repositories.QuizRepository;
using E_Learning.DAL.Repositories.SectionRepository;
using E_Learning.DAL.Repositories.UserRepository;
using E_Learning.DAL.Repositories.WishListRepository;

namespace E_Learning.DAL.UnitOfWorkDP
{
    public interface IUnitOfWork
    {
        /*------------------------------------------------------------------------*/
        IAnswerRepository AnswerRepository { get; }
        ICartRepository CartRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICourseRepository CourseRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        ILessonRepository LessonRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IQuizRepository QuizRepository { get; }
        ISectionRepository SectionRepository { get; }
        IUserRepository UserRepository { get; }
        IWishListRepository WishListRepository { get; }
        /*------------------------------------------------------------------------*/
        void SaveChanges();
        /*------------------------------------------------------------------------*/
    }
}
