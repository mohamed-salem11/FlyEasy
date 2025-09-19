using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Interfaces;

namespace FlyEasy.Repositories.Implementations
{
    public class AirPlaneRepository: GenericRepository<AirPlane>
    {
        private readonly FlyEasyContext _context;

        public AirPlaneRepository(FlyEasyContext context) : base(context)
        {
            _context = context;
        }


    }
}
