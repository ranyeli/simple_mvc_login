using System;
using Machine.Specifications;
using SimpleLogInSystem.Web.Models;
using System.Web.Mvc;
using SimpleLogInSystem.Core.Services;

namespace SimpleLogInSystem.Tests
{

    public class When_validating_login
    {
        static UserModel user;
        static Moq.Mock<UserValidationService> moqService;

        Establish context = () =>
        {
            user = new UserModel()
            {
                Email = String.Empty,
                Password = String.Empty
            };

            moqService = new Moq.Mock<UserValidationService>();
        };

        It should_allow_access_to_an_existing_user = () =>
        {
            moqService.Setup(m => m.UserExistence(user)).Returns(true);

            var exist = moqService.Object.IsUserValid(new ModelStateDictionary(), user);
            exist.ShouldBeTrue();
        };

        It should_deny_access_to_a_nonexisting_user = () =>
        {

            moqService.Setup(m => m.UserExistence(user)).Returns(false);

            var exist = moqService.Object.IsUserValid(new ModelStateDictionary(), user);
            exist.ShouldBeFalse();
        };
        It should_validate_the_email_format;
        It should_limit_the_password_length_to_minumum_of_6;
    }
}
