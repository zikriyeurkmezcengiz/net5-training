using System;
using BookStoreWebApi.Application.BookOperations.CreateBook;
using FluentAssertions;
using TestSetup;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("Lord Of The", 0, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 0, 1)]
        [InlineData("", 100, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lord", 0, 1)]
        [InlineData("Lord", 100, 0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);

            command.Model = new CreateBookModel() { Title = title, PageCount = pageCount, PublishDate = DateTime.Now.AddDays(-10), GenreId = genreId };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var errors = validator.Validate(command);

            //act & assert
            errors.Errors.Count.Should().BeGreaterThan(0);
        }

         [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);

            command.Model = new CreateBookModel() { Title = "Lord Of The Rings", PageCount = 100, PublishDate = DateTime.Now, GenreId = 1 };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var errors = validator.Validate(command);

            //act & assert
            errors.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}