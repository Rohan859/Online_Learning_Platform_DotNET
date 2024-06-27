using Online_Learning_Platform.AllDbContext;

namespace Online_Learning_Platform.Service
{
    public class EnrollmentService
    {
        private readonly AllTheDbContext _context;

        public EnrollmentService(AllTheDbContext context)
        {
            _context = context;
        }


        public string EnrollInACourse(Guid userId,Guid courseId)
        {

        }
    }
}
