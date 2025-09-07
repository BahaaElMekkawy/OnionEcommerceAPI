using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using OnionEcommerceAPI.Core.Application.Abstractions.Contracts;
using OnionEcommerceAPI.Core.Application.Abstractions.Models.Product;
using OnionEcommerceAPI.Core.Application.Services.Products;
using OnionEcommerceAPI.Core.Domain.Contracts.Presistence;
using OnionEcommerceAPI.Core.Domain.Entities.Products;

namespace OnionEcommerceAPI.Tests.UnitTests.Application
{
    public class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<Product,int>> _productRepoMock;
        private readonly ProductService _productService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IFixture _fixture;
        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepoMock = new Mock<IGenericRepository<Product, int>>();
            _mapperMock = new Mock<IMapper>();
            _productService = new ProductService(_unitOfWorkMock.Object,_mapperMock.Object);
            _fixture = new Fixture();
            _fixture.Customize(new AutoFixtureCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnProductsDtoList() 
        {
            //Arrange
            var products = _fixture.CreateMany<Product>(5).ToList();

             _productRepoMock.Setup(R => R.GetAllAsync(false)).ReturnsAsync(products);
            _unitOfWorkMock.Setup(U => U.GetRepository<Product, int>()).Returns(_productRepoMock.Object);

            _mapperMock.Setup(m => m.Map<IEnumerable<ProductDetailsDto>>(products))
               .Returns(products.Select(p => new ProductDetailsDto
               {
                   Id = p.Id,
                   Name = p.Name,
                   Price = p.Price,
                   Description = p.Description,
                   PictureUrl = p.PictureUrl,
                   Brand = p.Brand?.Name,
                   Category = p.Category?.Name
               }).ToList());


            //Act
            var res = await _productService.GetAllProductsAsync();
            //Assert
            res.Should().NotBeNull().And.HaveCount(products.Count);
        }

        [Fact]
        public async Task GetProductAsync_ProductExists_ReturnsProductDetailsDto()
        {
            // Arrange
            var productId = 1;//Exist productID

            var productEntity = _fixture.Build<Product>().With(p => p.Id, productId) .Create();

            var expectedDto = _fixture.Build<ProductDetailsDto>().With(d => d.Id, productId) .Create();

            _unitOfWorkMock.Setup(u => u.GetRepository<Product, int>())
                           .Returns(_productRepoMock.Object);

            _productRepoMock.Setup(r => r.GetAsync(productId))
                           .ReturnsAsync(productEntity); 

            _mapperMock.Setup(m => m.Map<ProductDetailsDto>(productEntity)) 
                       .Returns(expectedDto); 

            // Act
            var result = await _productService.GetProductAsync(productId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDto); 

            // Verify the correct methods were called with the correct parameters
            _unitOfWorkMock.Verify(u => u.GetRepository<Product, int>(), Times.Once);
            _productRepoMock.Verify(r => r.GetAsync(productId), Times.Once);
            _mapperMock.Verify(m => m.Map<ProductDetailsDto>(productEntity), Times.Once);
        }

        [Fact]
        public async Task GetProductAsync_ProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var productId = 999; // An ID that doesn't exist

            _unitOfWorkMock.Setup(u => u.GetRepository<Product, int>())
                           .Returns(_productRepoMock.Object);

            _productRepoMock.Setup(r => r.GetAsync(productId))
                           .ReturnsAsync((Product?)null); 

            // Note: The mapper should NEVER be called if the repository returns null.
            // Therefore, we do NOT set up the mapper mock.

            // Act
            var result = await _productService.GetProductAsync(productId);

            // Assert
            result.Should().BeNull(); // The service should return null

            // Verify the repository was called, but the mapper was NOT called
            _unitOfWorkMock.Verify(u => u.GetRepository<Product, int>(), Times.Once);
            _productRepoMock.Verify(r => r.GetAsync(productId), Times.Once);
            _mapperMock.Verify(m => m.Map<ProductDetailsDto>(It.IsAny<Product>()), Times.Never); 
        }

        [Fact]
        public async Task GetAllBrandsAsync_ShouldReturnBrandsDtoList()
        {
            // Arrange
            var brands = _fixture.CreateMany<ProductBrand>(3).ToList();
            var brandsDtos = _fixture.CreateMany<BrandDto>(3).ToList();

            // Setup the repository and UOW mocks for Brand
            var brandRepoMock = new Mock<IGenericRepository<ProductBrand, int>>();
            brandRepoMock.Setup(r => r.GetAllAsync(It.IsAny<bool>()))
                         .ReturnsAsync(brands);

            _unitOfWorkMock.Setup(u => u.GetRepository<ProductBrand, int>())
                           .Returns(brandRepoMock.Object);

            // Setup the mapper to return the list of DTOs
            _mapperMock.Setup(m => m.Map<IEnumerable<BrandDto>>(brands))
                       .Returns(brandsDtos);

            // Act
            var result = await _productService.GetAllBrandsAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(brandsDtos.Count);
            result.Should().BeEquivalentTo(brandsDtos); // Ensure the returned DTOs are correct

            // Verify interactions
            _unitOfWorkMock.Verify(u => u.GetRepository<ProductBrand, int>(), Times.Once);
            brandRepoMock.Verify(r => r.GetAllAsync(It.IsAny<bool>()), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<BrandDto>>(brands), Times.Once);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnCategoriesDtoList()
        {
            // Arrange
            var categories = _fixture.CreateMany<ProductCategory>(4).ToList();
            var categoriesDtos = _fixture.CreateMany<CategoryDto>(4).ToList();

            // Setup the repository and UOW mocks for Category
            var categoryRepoMock = new Mock<IGenericRepository<ProductCategory, int>>();
            categoryRepoMock.Setup(r => r.GetAllAsync(It.IsAny<bool>()))
                            .ReturnsAsync(categories);

            _unitOfWorkMock.Setup(u => u.GetRepository<ProductCategory, int>())
                           .Returns(categoryRepoMock.Object);

            // Setup the mapper to return the list of DTOs
            _mapperMock.Setup(m => m.Map<IEnumerable<CategoryDto>>(categories))
                       .Returns(categoriesDtos);

            // Act
            var result = await _productService.GetAllCategoriesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(categoriesDtos.Count);
            result.Should().BeEquivalentTo(categoriesDtos);

            // Verify interactions
            _unitOfWorkMock.Verify(u => u.GetRepository<ProductCategory, int>(), Times.Once);
            categoryRepoMock.Verify(r => r.GetAllAsync(It.IsAny<bool>()), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<CategoryDto>>(categories), Times.Once);
        }


    }
}
