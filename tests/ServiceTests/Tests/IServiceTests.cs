using System.Threading.Tasks;

namespace ServiceTests.Tests
{
    public interface IServiceTests
    {
        public Task CreateAsync_Success();
        public Task ExistsAsync_Success();
        public Task GetByIdAsync_Success();
        public Task GetFilteredAsync_Success();
        public Task GetCountAsync_Success();
        public Task UpdateAsync_Success();
        public Task DeleteAsync_Success();
    }
}