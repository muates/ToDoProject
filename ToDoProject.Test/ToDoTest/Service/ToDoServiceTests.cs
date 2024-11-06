using Moq;
using ToDoProject.Application.Converter;
using ToDoProject.Application.Service.Concrete;
using ToDoProject.Core.Service.Abstract;
using ToDoProject.CrossCutting.Ex;
using ToDoProject.DataAccess.Repository.Abstract;
using ToDoProject.Model.Dto.ToDo.Request;
using ToDoProject.Model.Entity;

namespace ToDoProject.Test.ToDoTest.Service;

[TestFixture]
public class ToDoServiceTests
{
        private Mock<IToDoRepository> _mockToDoRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private ToDoService _toDoService;

        [SetUp]
        public void SetUp()
        {
            _mockToDoRepository = new Mock<IToDoRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _toDoService = new ToDoService(_mockToDoRepository.Object, _mockUnitOfWork.Object);
        }

        [Test]
        public async Task GetAllToDoAsync_ShouldReturnToDoList_WhenToDosExist()
        {
            var toDos = new List<ToDo> { new ToDo { Title = "Test ToDo" } };
            _mockToDoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(toDos);

            // Act
            var result = await _toDoService.GetAllToDoAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.IsNotEmpty(result.Data!);
            Assert.That(result.Message, Is.EqualTo("ToDo retrieved successfully"));
        }

        [Test]
        public void GetAllToDoAsync_ShouldThrowNotFoundException_WhenNoToDosExist()
        {
            // Arrange
            _mockToDoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<ToDo>());

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _toDoService.GetAllToDoAsync());
        }

        [Test]
        public async Task GetToDoByIdAsync_ShouldReturnToDo_WhenToDoExists()
        {
            // Arrange
            var toDo = new ToDo { Title = "Test ToDo" };
            _mockToDoRepository.Setup(repo => repo.GetByIdAsync(toDo.Id)).ReturnsAsync(toDo);

            // Act
            var result = await _toDoService.GetToDoByIdAsync(toDo.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Data!.Id, Is.EqualTo(toDo.Id));
                Assert.That(result.Message, Is.EqualTo("ToDo retrieved successfully"));
            });
        }


        [Test]
        public void GetToDoByIdAsync_ShouldThrowNotFoundException_WhenToDoDoesNotExist()
        {
            // Arrange
            var toDoId = Guid.NewGuid();
            _mockToDoRepository.Setup(repo => repo.GetByIdAsync(toDoId)).ReturnsAsync((ToDo)null!);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _toDoService.GetToDoByIdAsync(toDoId));
        }

        [Test]
        public async Task AddToDoAsync_ShouldAddToDo_WhenRequestIsValid()
        {
            // Arrange
            var request = new AddToDoRequest { Title = "New ToDo", Description = "New Description" };
            var toDo = ToDoConverter.ToEntity(request);

            // Act
            await _toDoService.AddToDoAsync(request);

            // Assert
            _mockToDoRepository.Verify(repo => repo.AddAsync(It.Is<ToDo>(t => t.Title == request.Title)), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteToDoAsync_ShouldDeleteToDo_WhenToDoExists()
        {
            // Arrange
            var toDo = new ToDo { Title = "To Be Deleted" };
            _mockToDoRepository.Setup(repo => repo.GetByIdAsync(toDo.Id)).ReturnsAsync(toDo);

            // Act
            await _toDoService.DeleteToDoAsync(toDo.Id);

            // Assert
            _mockToDoRepository.Verify(repo => repo.DeleteAsync(toDo), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void DeleteToDoAsync_ShouldThrowNotFoundException_WhenToDoDoesNotExist()
        {
            // Arrange
            var toDoId = Guid.NewGuid();
            _mockToDoRepository.Setup(repo => repo.GetByIdAsync(toDoId)).ReturnsAsync((ToDo)null!);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _toDoService.DeleteToDoAsync(toDoId));
        }
}