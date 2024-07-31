using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using RadiosS.ProjectRadioS.WebAPI.Controllers;
using MediatR;
using RadioS.Application.Models;
using System.Reflection;
using RadioS.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace RadioS.Tests
{
    [TestFixture]
    public class FinancialDocumentsControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private DocumentsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new DocumentsController(_mockMediator.Object);
        }

        [Test]
        public async Task GetFinancialDocument_ProductNotSupported_ReturnsForbid()
        {
            // Arrange
            var request = new GetDocumentRequestModel
            {
                ProductCode = "UnsupportedProduct",
                TenantId = Guid.NewGuid(),
                DocumentId = Guid.NewGuid()
            };

            _mockMediator.Setup(m => m.Send(It.IsAny<IsProductSupported>(), default))
                         .ReturnsAsync(new IsProductSupported.Result { isSupported = false } );

            // Act
            var result = await _controller.GetFinancialDocument(request) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.Forbidden, result.StatusCode);
            Assert.AreEqual("Product is not supported", result.Value);
        }

    }

    public class IsProductSupported : IRequest<IsProductSupported.Result>
    {
        public string ProductCode { get; set; }

        public class Result
        {
            public bool isSupported { get; set; }
        }
    }
}
