using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;
using NutritionalRecipeBook.Application.Services;
using NutritionalRecipeBook.Domain;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;
using NutritionalRecipeBook.Infrastructure.Repositories;

namespace NutritionalRecipeBook.Api.UnitTests.Services;

[TestFixture]
public class RecipeServiceTest
{
    private Mock<IUnitOfWork> _unitOfWorkMock = null!;
    private Mock<ILogger<RecipeService>> _loggerMock = null!;
    private Mock<IIngredientService> _ingredientServiceMock = null!;
    private Mock<ICommentsService> _commentsServiceMock = null!;
    private Mock<IRepository<Recipe, Guid>> _recipeRepoMock = null!; 
    private Mock<IRepository<UserRecipe, Guid>> _userRecipeRepoMock = null!;
    private RecipeService _service = null!;

    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<RecipeService>>();
        _ingredientServiceMock = new Mock<IIngredientService>();
        _commentsServiceMock = new Mock<ICommentsService>();

        _recipeRepoMock = new Mock<IRepository<Recipe, Guid>>();
        _userRecipeRepoMock = new Mock<IRepository<UserRecipe, Guid>>();

        _unitOfWorkMock.Setup(u => u.Repository<Recipe, Guid>()).Returns(_recipeRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.Repository<UserRecipe, Guid>()).Returns(_userRecipeRepoMock.Object);

        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(true);

        _service = new RecipeService(
            _unitOfWorkMock.Object,
            _loggerMock.Object,
            _ingredientServiceMock.Object,
            _commentsServiceMock.Object
        );
    }

    [Test]
    public async Task CreateRecipeAsync_ValidInput_ShouldCreateRecipe()
    {
        // Arrange
        Recipe? capturedRecipe = null;
        UserRecipe? capturedUserRecipe = null;

        _recipeRepoMock
            .Setup(r => r.InsertAsync(It.IsAny<Recipe>()))
            .Callback<Recipe>(r => capturedRecipe = r)
            .Returns(Task.FromResult(true));

        _userRecipeRepoMock
            .Setup(r => r.InsertAsync(It.IsAny<UserRecipe>()))
            .Callback<UserRecipe>(ur => capturedUserRecipe = ur)
            .Returns(Task.FromResult(true));

        var userId = Guid.NewGuid();

        var recipeDto = new RecipeIngredientNutrientDTO
        (
            new RecipeDTO
            (
                Guid.Empty,
                "Salad",
                "Fresh and healthy",
                "Some instructions",
                20,
                2,
                "http://example.com/salad.jpg",
                240
            ),
            new List<IngredientAmountDTO>(),
            new List<NutrientDTO>()
        );

        // Act
        await _service.CreateRecipeAsync(recipeDto, userId);

        // Assert
        _recipeRepoMock.Verify(r => r.InsertAsync(It.IsAny<Recipe>()), Times.Once);
        _userRecipeRepoMock.Verify(r => r.InsertAsync(It.IsAny<UserRecipe>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.AtLeastOnce);

        Assert.IsNotNull(capturedRecipe, "Recipe entity should be passed to repository");
        Assert.That(capturedRecipe!.Name, Is.EqualTo(recipeDto.RecipeDTO.Name), "Recipe name should match");
        Assert.That(capturedRecipe.Description, Is.EqualTo(recipeDto.RecipeDTO.Description), "Recipe description should match");

        Assert.IsNotNull(capturedUserRecipe, "UserRecipe entity should be passed to repository");
        Assert.That(capturedUserRecipe!.UserId, Is.EqualTo(userId), "UserRecipe UserId should match");
        Assert.That(capturedUserRecipe.RecipeId, Is.EqualTo(capturedRecipe.Id), "UserRecipe RecipeId should match created Recipe Id");
        Assert.IsTrue(capturedUserRecipe.IsOwner, "capturedUserRecipe.IsOwner");
    }

    [Test]
    public async Task UpdateRecipeAsync_ValidInput_ShouldUpdateRecipe()
    {
        // Arrange
        var recipeGuid = new Guid("3f7b4b2a-8c2e-4b8a-9c2d-9a2f4a1e6c31");
        var userId = Guid.NewGuid();

        var existingRecipe = new Recipe
        {
            Id = recipeGuid,
            Name = "Old salad",
            Description = "Old description",
            Instructions = "Old instructions",
            CookingTimeInMin = 10,
            Servings = 1,
            ImageUrl = "http://example.com/old.jpg",
            CaloriesPerServing = 100
        };

        Recipe? capturedRecipe = null;

        _recipeRepoMock
            .Setup(r => r.GetByIdAsync(recipeGuid))
            .ReturnsAsync(existingRecipe);

        _userRecipeRepoMock
            .Setup(r => r.GetSingleOrDefaultAsync(
                It.IsAny<Expression<Func<UserRecipe, bool>>>()))
            .ReturnsAsync(new UserRecipe
            {
                UserId = userId,
                RecipeId = recipeGuid,
                IsOwner = true
            });

        _recipeRepoMock
            .Setup(r => r.UpdateAsync(It.IsAny<Recipe>()))
            .Callback<Recipe>(r => capturedRecipe = r)
            .Returns(Task.FromResult(true));

        var recipeDtoToUpdate = new RecipeIngredientNutrientDTO
        (
            new RecipeDTO
            (
                recipeGuid,
                "Saladss",
                "Fresh and healthy",
                "Some instructions",
                20,
                2,
                "http://example.com/salads.jpg",
                240
            ),
            new List<IngredientAmountDTO>(),
            new List<NutrientDTO>()
        );

        // Act
        await _service.UpdateRecipeAsync(recipeGuid, recipeDtoToUpdate, userId);

        // Assert
        _recipeRepoMock.Verify(r => r.GetByIdAsync(recipeGuid), Times.Once);
        _recipeRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Recipe>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.AtLeastOnce);

        Assert.IsNotNull(capturedRecipe, "Recipe entity should be passed to repository");
        Assert.That(capturedRecipe!.Id, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.Id), "Recipe Id should match");
        Assert.That(capturedRecipe.Name, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.Name), "Recipe name should match");
        Assert.That(capturedRecipe.Description, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.Description), "Recipe description should match");
        Assert.That(capturedRecipe.Instructions, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.Instructions), "Recipe instructions should match");
        Assert.That(capturedRecipe.CaloriesPerServing, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.CaloriesPerServing), "Recipe calories should match");
        Assert.That(capturedRecipe.CookingTimeInMin, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.CookingTimeInMin), "Recipe cooking time should match");
        Assert.That(capturedRecipe.Servings, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.Servings), "Recipe servings should match");
        Assert.That(capturedRecipe.ImageUrl, Is.EqualTo(recipeDtoToUpdate.RecipeDTO.ImageUrl), "Recipe image URL should match");
    }

    [Test]
    public async Task DeleteRecipeAsync_ValidInput_RecipeDeleted()
    {
        // Arrange
        var recipeGuid = new Guid("3f7b4b2a-8c2e-4b8a-9c2d-9a2f4a1e6c31");
        var userId = Guid.NewGuid();

        var existingUserRecipe = new UserRecipe
        {
            UserId = userId,
            RecipeId = recipeGuid,
            IsOwner = true
        };

        _userRecipeRepoMock
            .Setup(r => r.GetSingleOrDefaultAsync(
                It.IsAny<Expression<Func<UserRecipe, bool>>>()))
            .ReturnsAsync(existingUserRecipe);

        _recipeRepoMock
            .Setup(r => r.DeleteAsync(recipeGuid))
            .Returns(Task.FromResult(true));

        // Act
        var result = await _service.DeleteRecipeAsync(recipeGuid, userId);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task GetRecipesAsync_WhenCalled_ReturnsPaginedResult()
    {
        // Arrange
        var db = CreateDbContext();
        var unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));

        var service = new RecipeService(
            unitOfWork,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        db.Recipes.AddRange(
            new Recipe { Id = Guid.NewGuid(), Name = "Apple Salad" },
            new Recipe { Id = Guid.NewGuid(), Name = "Chicken Soup" },
            new Recipe { Id = Guid.NewGuid(), Name = "Beef Steak" },
            new Recipe { Id = Guid.NewGuid(), Name = "Greek Salad" }
        );

        await db.SaveChangesAsync();

        // Act
        var result = await service.GetRecipesAsync(1, 2);

        // Assert
        result.Should().NotBeNull();
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(2);
        result.TotalCount.Should().Be(4);

        result.Items.Should().HaveCount(2);
        result.Items.Select(r => r.Name)
            .Should()
            .BeInAscendingOrder();
    }

    [Test]
    public async Task GetRecipesForUserAsync_WhenCalled_ReturnsPaginedResult()
    {
        // Arrange
        var db = CreateDbContext();
        var unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));

        var service = new RecipeService(
            unitOfWork,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        var userId = Guid.NewGuid();

        db.Recipes.AddRange(
            new Recipe
            {
                Id = Guid.NewGuid(),
                Name = "Apple Salad",
                UserRecipes = new List<UserRecipe>
                {
                    new UserRecipe { UserId = userId, IsOwner = true }
                }
            },
            new Recipe { Id = Guid.NewGuid(), Name = "Chicken Soup" },
            new Recipe { Id = Guid.NewGuid(), Name = "Beef Steak" },
            new Recipe { Id = Guid.NewGuid(), Name = "Greek Salad" }
        );

        await db.SaveChangesAsync();

        // Act
        var result = await service.GetRecipesForUserAsync(1, 2, userId);

        // Assert
        result.Should().NotBeNull();
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(2);
        result.TotalCount.Should().Be(1);

        result.Items.Should().HaveCount(1);
        result.Items.Select(r => r.Name).Should().Contain("Apple Salad");
    }

    [Test]
    public async Task MarkFavoriteRecipeAsync_ValidInput_ShouldMarkAsFavorite()
    {
        // Arrange
        var recipeGuid = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var existingUserRecipe = new UserRecipe
        {
            UserId = userId,
            RecipeId = recipeGuid,
            IsOwner = false,
            IsFavourite = false
        };

        IEnumerable<UserRecipe> recipeCollection = new[] { existingUserRecipe };

        _userRecipeRepoMock
            .Setup(r => r.GetWhereAsync(
                It.IsAny<Expression<Func<UserRecipe, bool>>>()))
            .ReturnsAsync(recipeCollection);

        _userRecipeRepoMock
            .Setup(r => r.UpdateAsync(It.IsAny<UserRecipe>()))
            .Returns(Task.FromResult(true));

        // Act
        await _service.MarkFavoriteRecipeAsync(recipeGuid, userId);

        // Assert
        _userRecipeRepoMock.Verify(r =>
            r.GetWhereAsync(It.IsAny<Expression<Func<UserRecipe, bool>>>()), Times.Once);
        _userRecipeRepoMock.Verify(
            r => r.UpdateAsync(It.Is<UserRecipe>(ur =>
                ur.UserId == existingUserRecipe.UserId &&
                ur.RecipeId == existingUserRecipe.RecipeId &&
                ur.IsFavourite == true)),
            Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.AtLeastOnce);

        Assert.IsTrue(existingUserRecipe.IsFavourite, "Recipe should be marked as favorite");
    }

    [Test]
    public async Task UnmarkFavoriteRecipeAsync_ValidInput_ShouldUnmarkAsFavorite()
    {
        // Arrange
        var recipeGuid = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var existingUserRecipe = new UserRecipe
        {
            UserId = userId,
            RecipeId = recipeGuid,
            IsOwner = false,
            IsFavourite = true
        };

        IEnumerable<UserRecipe> recipeCollection = new[] { existingUserRecipe };

        _userRecipeRepoMock
            .Setup(r => r.GetWhereAsync(
                It.IsAny<Expression<Func<UserRecipe, bool>>>()))
            .ReturnsAsync(recipeCollection);

        _userRecipeRepoMock
            .Setup(r => r.UpdateAsync(It.IsAny<UserRecipe>()))
            .Returns(Task.FromResult(true));

        // Act
        await _service.UnmarkFavoriteRecipeAsync(recipeGuid, userId);

        // Assert
        _userRecipeRepoMock.Verify(r =>
            r.GetWhereAsync(It.IsAny<Expression<Func<UserRecipe, bool>>>()), Times.Once);
        _userRecipeRepoMock.Verify(
            r => r.UpdateAsync(It.Is<UserRecipe>(ur =>
                ur.UserId == existingUserRecipe.UserId &&
                ur.RecipeId == existingUserRecipe.RecipeId &&
                ur.IsFavourite == false)),
            Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.AtLeastOnce);

        Assert.IsFalse(existingUserRecipe.IsFavourite, "Recipe should be unmarked as favorite");
    }

    [Test]
    public async Task UploadImageAsync_ValidJpeg_ShouldSaveFileAndReturnUrl()
    {
        // Arrange
        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        try
        {
            var bytes = new byte[] { 1, 2, 3, 4 };
            await using var ms = new MemoryStream(bytes);

            var originalFileName = "photo.jpg";

            // Act
            var result = await _service.UploadImageAsync(ms, originalFileName, tempRoot);

            // Assert
            result.Should().NotBeNull();
            result.Should().StartWith("/api/recipes/image/").And.EndWith(".jpg");

            var fileName = result.Split("/").Last();
            var savedPath = Path.Combine(tempRoot, "images", fileName);
            File.Exists(savedPath).Should().BeTrue();

            var savedBytes = await File.ReadAllBytesAsync(savedPath);
            savedBytes.Should().Equal(bytes);
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, recursive: true);
        }
    }

    [Test]
    public async Task GetRecipeByIdAsync_ValidId_ReturnsRecipeWithAggregatedNutrients()
    {
        // Arrange
        var db = CreateDbContext();
        var unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));

        var service = new RecipeService(
            unitOfWork,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        var recipeId = Guid.NewGuid();
        var ingredientId = Guid.NewGuid();
        var nutrientId = Guid.NewGuid();

        var recipe = new Recipe
        {
            Id = recipeId,
            Name = "Salad",
            Description = "Desc",
            Instructions = "Instr",
            CookingTimeInMin = 10,
            Servings = 2,
            ImageUrl = "url",
            CaloriesPerServing = 100,
            RecipeIngredients = new List<RecipeIngredient>()
        };

        var ingredient = new Ingredient
        {
            Id = ingredientId,
            Name = "Tomato",
            IsLiquid = false
        };

        var unit = new UnitOfMeasure
        {
            Id = Guid.NewGuid(),
            Name = "g",
            IsLiquidMeasure = false
        };

        var recipeIngredient = new RecipeIngredient
        {
            RecipeId = recipeId,
            Recipe = recipe,
            IngredientId = ingredientId,
            Ingredient = ingredient,
            Amount = 200m,
            UnitOfMeasureId = unit.Id,
            UnitOfMeasure = unit
        };

        recipe.RecipeIngredients.Add(recipeIngredient);

        var nutrient = new Nutrient
        {
            Id = nutrientId,
            Name = "Vitamin C",
            Unit = "mg"
        };

        var nutrientIngredient = new NutrientIngredient
        {
            IngredientId = ingredientId,
            NutrientId = nutrientId,
            IngredientAmountPer100G = 10m
        };

        db.Recipes.Add(recipe);
        db.Ingredients.Add(ingredient);
        db.UnitOfMeasures.Add(unit);
        db.Nutrients.Add(nutrient);
        db.NutrientIngredients.Add(nutrientIngredient);
        await db.SaveChangesAsync();

        // Act
        var result = await service.GetRecipeByIdAsync(recipeId);

        // Assert
        result.Should().NotBeNull();
        result!.RecipeDTO.Id.Should().Be(recipeId);
        result.Ingredients.Should().HaveCount(1);
        result.Nutrients.Should().HaveCount(1);

        var nutrientDto = result.Nutrients.Single();
        nutrientDto.Name.Should().Be("vitamin c");
        nutrientDto.UnitOfMeasureDTO.Name.Should().Be("mg");
        nutrientDto.Amount.Should().Be(20m);
    }

    [Test]
    public async Task ParseRecipeFromJsonAsync_ValidJsonWithOneRecipe_ShouldCreateRecipes()
    {
        // Arrange
        var recipeJsonObj = new[]
        {
            new
            {
                RecipeDTO = new
                {
                    Id = Guid.Empty,
                    Name = "Json Salad",
                    Description = "From JSON",
                    Instructions = "Mix",
                    CookingTimeInMin = 5,
                    Servings = 1,
                    ImageUrl = (string?)null,
                    CaloriesPerServing = 50
                },
                Ingredients = Array.Empty<object>(),
                Nutrients = Array.Empty<object>()
            }
        };

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(recipeJsonObj);
        var bytes = System.Text.Encoding.UTF8.GetBytes(json);
        var ms = new MemoryStream(bytes);

        var fileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
        fileMock.Setup(f => f.FileName).Returns("recipes.json");
        fileMock.Setup(f => f.Length).Returns(bytes.Length);
        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);

        var userId = Guid.NewGuid();

        // Act
        var result = await _service.ParseRecipeFromJsonAsync(fileMock.Object, userId);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(0);
    }

    [Test]
    public async Task ExportRecipesForUserJsonAsync_UserWithRecipes_ReturnsJson()
    {
        // Arrange
        var db = CreateDbContext();
        var unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));

        var service = new RecipeService(
            unitOfWork,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        var userId = Guid.NewGuid();
        var recipe = new Recipe
        {
            Id = Guid.NewGuid(),
            Name = "Exported Salad",
            Description = "Desc",
            Instructions = "Instr",
            CookingTimeInMin = 10,
            Servings = 2,
            ImageUrl = "url",
            CaloriesPerServing = 100,
            UserRecipes = new List<UserRecipe>
            {
                new UserRecipe { UserId = userId, IsOwner = true }
            },
            RecipeIngredients = new List<RecipeIngredient>(),
            Comments = new List<Comment>()
        };

        db.Recipes.Add(recipe);
        await db.SaveChangesAsync();

        // Act
        var result = await service.ExportRecipesForUserJsonAsync(userId);

        // Assert
        result.Should().NotBeNull();
        var (buffer, contentType) = result!.Value;
        contentType.Should().Be("application/json");

        var json = System.Text.Encoding.UTF8.GetString(buffer);
        json.Should().Contain("Exported Salad");
    }

    [Test]
    public async Task GetImageAsync_ExistingPng_ReturnsBufferAndContentType()
    {
        // Arrange
        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var imagesPath = Path.Combine(tempRoot, "images");
        Directory.CreateDirectory(imagesPath);

        try
        {
            var fileName = "test.png";
            var fullPath = Path.Combine(imagesPath, fileName);
            var bytes = new byte[] { 10, 20, 30 };
            await File.WriteAllBytesAsync(fullPath, bytes);

            // Act
            var result = await _service.GetImageAsync(fileName, tempRoot);

            // Assert
            result.Should().NotBeNull();
            var (buffer, contentType) = result!.Value;
            contentType.Should().Be("image/png");
            buffer.Should().Equal(bytes);
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, recursive: true);
        }
    }

    [Test]
    public async Task CreateRecipeAsync_NullDto_ReturnsNullAndDoesNotInsert()
    {
        // Act
        var result = await _service.CreateRecipeAsync(null, Guid.NewGuid());

        // Assert
        result.Should().BeNull();
        _recipeRepoMock.Verify(r => r.InsertAsync(It.IsAny<Recipe>()), Times.Never);
        _userRecipeRepoMock.Verify(r => r.InsertAsync(It.IsAny<UserRecipe>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task CreateRecipeAsync_EmptyName_ReturnsNullAndDoesNotInsert()
    {
        // Arrange
        var dto = new RecipeIngredientNutrientDTO(
            new RecipeDTO(Guid.Empty, "  ", string.Empty, string.Empty, 0, 0, string.Empty, 0),
            new List<IngredientAmountDTO>(),
            new List<NutrientDTO>());

        // Act
        var result = await _service.CreateRecipeAsync(dto, Guid.NewGuid());

        // Assert
        result.Should().BeNull();
        _recipeRepoMock.Verify(r => r.InsertAsync(It.IsAny<Recipe>()), Times.Never);
        _userRecipeRepoMock.Verify(r => r.InsertAsync(It.IsAny<UserRecipe>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task UpdateRecipeAsync_NullDto_ReturnsFalseAndDoesNotCallRepo()
    {
        // Act
        var result = await _service.UpdateRecipeAsync(Guid.NewGuid(), null, Guid.NewGuid());

        // Assert
        result.Should().BeFalse();
        _recipeRepoMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
        _recipeRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Recipe>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task UpdateRecipeAsync_RecipeNotFound_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Recipe?)null);

        var dto = new RecipeIngredientNutrientDTO(
            new RecipeDTO(id, "Name", string.Empty, string.Empty, 0, 0, string.Empty, 0),
            new List<IngredientAmountDTO>(),
            new List<NutrientDTO>());

        // Act
        var result = await _service.UpdateRecipeAsync(id, dto, Guid.NewGuid());

        // Assert
        result.Should().BeFalse();
        _recipeRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Recipe>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task UpdateRecipeAsync_UserIsNotOwner_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();
        _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Recipe { Id = id });

        _userRecipeRepoMock
            .Setup(r => r.GetSingleOrDefaultAsync(It.IsAny<Expression<Func<UserRecipe, bool>>>() ))
            .ReturnsAsync(new UserRecipe { UserId = userId, RecipeId = id, IsOwner = false });

        var dto = new RecipeIngredientNutrientDTO(
            new RecipeDTO(id, "Name", string.Empty, string.Empty, 0, 0, string.Empty, 0),
            new List<IngredientAmountDTO>(),
            new List<NutrientDTO>());

        // Act
        var result = await _service.UpdateRecipeAsync(id, dto, userId);

        // Assert
        result.Should().BeFalse();
        _recipeRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Recipe>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task DeleteRecipeAsync_UserIsNotOwner_ReturnsFalseAndDoesNotDelete()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _userRecipeRepoMock
            .Setup(r => r.GetSingleOrDefaultAsync(It.IsAny<Expression<Func<UserRecipe, bool>>>() ))
            .ReturnsAsync(new UserRecipe { UserId = userId, RecipeId = id, IsOwner = false });

        // Act
        var result = await _service.DeleteRecipeAsync(id, userId);

        // Assert
        result.Should().BeFalse();
        _recipeRepoMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task UploadImageAsync_InvalidExtension_ReturnsNullAndDoesNotCreateFile()
    {
        // Arrange
        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);
        await using var ms = new MemoryStream(new byte[] { 1, 2, 3 });
        var originalFileName = "file.txt";

        try
        {
            // Act
            var result = await _service.UploadImageAsync(ms, originalFileName, tempRoot);

            // Assert
            result.Should().BeNull();
            var imagesPath = Path.Combine(tempRoot, "images");
            Directory.Exists(imagesPath).Should().BeFalse();
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, true);
        }
    }

    [Test]
    public async Task UploadImageAsync_NullStreamOrFileName_ReturnsNull()
    {
        // Arrange
        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        try
        {
            // Act
            var result1 = await _service.UploadImageAsync(null, "photo.jpg", tempRoot);
            var result2 = await _service.UploadImageAsync(new MemoryStream(), string.Empty, tempRoot);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, true);
        }
    }

    [Test]
    public async Task GetRecipeByIdAsync_WhenException_ReturnsNull()
    {
        // Arrange
        var db = CreateDbContext();
        var unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));
        var service = new RecipeService(
            unitOfWork,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        db.Dispose();

        // Act
        var result = await service.GetRecipeByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task ParseRecipeFromJsonAsync_NullOrEmptyFile_ReturnsEmptyArray()
    {
        // Act
        var result1 = await _service.ParseRecipeFromJsonAsync(null, Guid.NewGuid());

        var emptyFileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
        emptyFileMock.Setup(f => f.Length).Returns(0);
        emptyFileMock.Setup(f => f.FileName).Returns("recipes.json");

        var result2 = await _service.ParseRecipeFromJsonAsync(emptyFileMock.Object, Guid.NewGuid());

        // Assert
        result1.Should().BeEmpty();
        result2.Should().BeEmpty();
    }

    [Test]
    public async Task ParseRecipeFromJsonAsync_InvalidExtension_ReturnsEmptyArray()
    {
        // Arrange
        var fileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
        fileMock.Setup(f => f.FileName).Returns("recipes.txt");
        fileMock.Setup(f => f.Length).Returns(10);

        // Act
        var result = await _service.ParseRecipeFromJsonAsync(fileMock.Object, Guid.NewGuid());

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task ParseRecipeFromJsonAsync_MalformedJson_ReturnsEmptyArray()
    {
        // Arrange
        var bytes = System.Text.Encoding.UTF8.GetBytes("{ not json }");
        var ms = new MemoryStream(bytes);

        var fileMock = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
        fileMock.Setup(f => f.FileName).Returns("recipes.json");
        fileMock.Setup(f => f.Length).Returns(bytes.Length);
        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);

        // Act
        var result = await _service.ParseRecipeFromJsonAsync(fileMock.Object, Guid.NewGuid());

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public async Task ExportRecipesForUserJsonAsync_NoRecipes_ReturnsNull()
    {
        // Arrange
        var db = CreateDbContext();
        var unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));
        var service = new RecipeService(
            unitOfWork,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        // Act
        var result = await service.ExportRecipesForUserJsonAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task GetImageAsync_EmptyOrMissingFile_ReturnsNull()
    {
        // Arrange
        var tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        try
        {
            // Act
            var result1 = await _service.GetImageAsync(string.Empty, tempRoot);
            var result2 = await _service.GetImageAsync("no_such_file.png", tempRoot);

            // Assert
            result1.Should().BeNull();
            result2.Should().BeNull();
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, true);
        }
    }

    [Test]
    public async Task MarkFavoriteRecipeAsync_NullRecipeId_ReturnsFalse()
    {
        // Act
        var result = await _service.MarkFavoriteRecipeAsync(null, Guid.NewGuid());

        // Assert
        result.Should().BeFalse();
        _userRecipeRepoMock.Verify(r => r.InsertAsync(It.IsAny<UserRecipe>()), Times.Never);
        _userRecipeRepoMock.Verify(r => r.UpdateAsync(It.IsAny<UserRecipe>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task UnmarkFavoriteRecipeAsync_NoFavoriteConnections_ReturnsFalse()
    {
        // Arrange
        _userRecipeRepoMock
            .Setup(r => r.GetWhereAsync(It.IsAny<Expression<Func<UserRecipe, bool>>>() ))
            .ReturnsAsync(Enumerable.Empty<UserRecipe>());

        // Act
        var result = await _service.UnmarkFavoriteRecipeAsync(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        result.Should().BeFalse();
        _userRecipeRepoMock.Verify(r => r.UpdateAsync(It.IsAny<UserRecipe>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
    }

    [Test]
    public async Task GetRecipesAsync_WhenException_ReturnsEmptyPagedResult()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock
            .Setup(u => u.Repository<Recipe, Guid>().GetQueryable())
            .Throws(new Exception("DB failure"));

        var service = new RecipeService(
            unitOfWorkMock.Object,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        // Act
        var result = await service.GetRecipesAsync(1, 10, null);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
    }

    [Test]
    public async Task GetRecipesForUserAsync_WhenUserHasNoRecipes_ReturnsEmptyPagedResult()
    {
        // Arrange
        var db = CreateDbContext();
        var unitOfWork = new UnitOfWork(db, new RepositoryFactory(db));
        var service = new RecipeService(
            unitOfWork,
            Mock.Of<ILogger<RecipeService>>(),
            Mock.Of<IIngredientService>(),
            Mock.Of<ICommentsService>()
        );

        // Act
        var result = await service.GetRecipesForUserAsync(1, 10, Guid.NewGuid());

        // Assert
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }

    private ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}