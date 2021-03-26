using AutoMapper;
using TechDemo.Core.Infrastructure.Persisence;

namespace TechDemo.Tests.Common
{
    class TestBase
    {
        private ApplicationContext _context;
        private IMapper _mapper;

        protected ApplicationContext Context { get => _context; }
        protected IMapper Mapper { get => _mapper;  }

        public TestBase()
        {
            _context = DbContextFactory.Create();
            _mapper = MapperFactory.Create();

        }
        public TestBase(bool asNoTracking)
        {
            _context = DbContextFactory.Create(asNoTracking);
        }

        public void CreateNewContext(bool asNoTracking = false)
        {
            _context = DbContextFactory.Create(asNoTracking);
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(_context);
        }
    }
}
