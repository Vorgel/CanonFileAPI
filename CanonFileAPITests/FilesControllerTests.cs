using CanonFileAPI.Controllers;
using CanonFileAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CanonFileAPI.Tests.Controllers
{
    [TestFixture]
    public class FilesControllerTests
    {
        private FilesController filesController;
        private Mock<IFileService> filesServiceMock;

        [SetUp]
        public void Setup()
        {
            filesServiceMock = new Mock<IFileService>();
            filesController = new FilesController(filesServiceMock.Object);
        }

        [Test]
        public void Get_ReturnsListOfFilesFromService()
        {
            var mockFiles = new List<IFile> { new Models.File { Name = "file1.txt" }, new Models.File { Name = "file2.jpg" } };
            filesServiceMock.Setup(service => service.GetFiles()).Returns(mockFiles);

            var result = filesController.Get();

            Assert.That(result, Is.InstanceOf<IEnumerable<IFile>>());
            Assert.That(result, Is.EqualTo(mockFiles));
        }

        [Test]
        public void Post_WithValidFile_ReturnsOkResult()
        {
            var mockFile = new Models.File { Name = "file1.txt" };

            var result = filesController.Post(mockFile);

            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void Post_WithNullFile_ReturnsBadRequestResult()
        {
            var result = filesController.Post(null);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.EqualTo("File or its properties are missing"));
        }

        [Test]
        public void Post_WithEmptyFileName_ReturnsBadRequestResult()
        {
            var mockFile = new Models.File { Name = "" };

            var result = filesController.Post(mockFile);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.EqualTo("File or its properties are missing"));
        }

        [Test]
        public void Post_WithExceptionInService_ReturnsBadRequestResult()
        {
            var mockFile = new Models.File { Name = "file1.txt" };
            filesServiceMock.Setup(service => service.AddFile(mockFile)).Throws(new Exception("Some error occurred"));

            var result = filesController.Post(mockFile);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.That(badRequestResult.Value, Is.EqualTo("Some error occurred"));
        }
    }
}