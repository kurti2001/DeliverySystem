using DAL.Entities;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Testing.DAL
{
    public class AreaRepository_Test : BaseTest
    {
        private readonly IAreaRepository _repository;

        public AreaRepository_Test()
        {
            _repository = _serviceProvider.GetRequiredService<IAreaRepository>();
        }

        [Fact]
        public async Task AddArea_ShouldSucceed()
        {
            var newArea = new Area { Name = "Area Oneeee" };
            await _repository.AddAsync(newArea);
            await _unitOfWork.CommitAsync();
            Assert.True(newArea.Id > 0, $"Area {newArea.Name} was expected to be added");
        }

        [Fact]
        public async Task AddArea_ShouldFail_WithoutCommit()
        {
            var newArea = new Area { Name = "Transient Area" };
            await _repository.AddAsync(newArea);
            Assert.True(newArea.Id == 0, "Area ID should not be set without commit");
        }

        [Fact]
        public async Task GetAllAreas_WhenNotEmpty_ShouldReturnAreas()
        {
            await _repository.AddAsync(new Area { Name = "Existing Area" });
            await _unitOfWork.CommitAsync();

            var areas = await _repository.GetAllAsync();
            Assert.NotEmpty(areas);
        }

        [Fact]
        public async Task GetAllAreas_WhenEmpty_ShouldReturnEmpty()
        {
            var allAreas = await _repository.GetAllAsync();
            foreach (var area in allAreas)
            {
                _repository.DeleteAsync(area);
            }
            await _unitOfWork.CommitAsync();

            var areas = await _repository.GetAllAsync();
            Assert.Empty(areas);
        }

        [Fact]
        public async Task DeleteArea_ShouldSucceed()
        {
            var areaToDelete = new Area { Name = "Deleted" };
            await _repository.AddAsync(areaToDelete);
            await _unitOfWork.CommitAsync();

            _repository.DeleteAsync(areaToDelete);
            await _unitOfWork.CommitAsync();

            var deletedArea = await _repository.GetByIdAsync(areaToDelete.Id);
            Assert.Null(deletedArea);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectArea()
        {
            var newArea = new Area { Name = "Area Two" };
            await _repository.AddAsync(newArea);
            await _unitOfWork.CommitAsync();

            var fetchedArea = await _repository.GetByIdAsync(newArea.Id);
            Assert.NotNull(fetchedArea);
            Assert.Equal("Unique Area", fetchedArea.Name);
        }
    }
}
