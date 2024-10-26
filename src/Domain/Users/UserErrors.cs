using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");
    public static Error AlreadtExist(string email) => Error.Problem(
        "Users.AlreadyExist",
        $"The user with the Id = '{email}' allready exist");
    public static Error InvalidEmailOrPassword(string email, string password) => Error.Problem(
        "Users.Password.Or.Email",
        $"The email = '{email}' or password = '{password}' invalid");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found");

    public static readonly Error ApplicationNotFound = Error.NotFound(
        "Users.ApplicationNotFound",
        "The volunteer with the specified application was not found");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");
    public static readonly Error NotFoundAnyAvaliableLocations = Error.Conflict(
        "Users.NotFoundAnyAvaliableLOcations",
        "User dosent have any avaliable location");
    public static readonly Error NoSalesData = Error.Conflict(
    "Users.NoSalesData",
    "User dosent have any avaliable sales");
}