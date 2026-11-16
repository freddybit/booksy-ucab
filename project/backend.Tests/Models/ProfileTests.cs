using backend.models;

namespace backend.Tests.Models;

public class ProfileTests
{
    [Fact]
    public void Constructor_AssignsValues()
    {
        var buyer = new Buyer("a@ucab.edu.ve", "Ana", "Perez", 21, "secreto");

        Assert.Equal("a@ucab.edu.ve", buyer.Email);
        Assert.Equal("Ana", buyer.FirstName);
        Assert.Equal("Perez", buyer.LastName);
        Assert.Equal(21, buyer.Age);
        Assert.Equal("secreto", buyer.Password);
    }

    [Fact]
    public void Constructor_NullEmail_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new Buyer(null!, "Ana", "Perez", 21, "secreto"));
    }

    [Fact]
    public void Constructor_NullFirstName_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new Buyer("a@ucab.edu.ve", null!, "Perez", 21, "secreto"));
    }

    [Fact]
    public void Constructor_NullLastName_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new Buyer("a@ucab.edu.ve", "Ana", null!, 21, "secreto"));
    }

    [Fact]
    public void Constructor_NullPassword_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => new Buyer("a@ucab.edu.ve", "Ana", "Perez", 21, null!));
    }

    [Fact]
    public void Setters_NullValue_Throw()
    {
        var buyer = Builders.MakeBuyer();

        Assert.Throws<ArgumentNullException>(() => buyer.Email = null!);
        Assert.Throws<ArgumentNullException>(() => buyer.FirstName = null!);
        Assert.Throws<ArgumentNullException>(() => buyer.LastName = null!);
        Assert.Throws<ArgumentNullException>(() => buyer.Password = null!);
    }

    [Fact]
    public void Setters_ValidValue_UpdateProperties()
    {
        var buyer = Builders.MakeBuyer();

        buyer.Email = "nuevo@ucab.edu.ve";
        buyer.FirstName = "Maria";
        buyer.LastName = "Lopez";
        buyer.Age = 40;

        Assert.Equal("nuevo@ucab.edu.ve", buyer.Email);
        Assert.Equal("Maria", buyer.FirstName);
        Assert.Equal("Lopez", buyer.LastName);
        Assert.Equal(40, buyer.Age);
    }

    [Fact]
    public void VerifyPassword_CorrectPassword_ReturnsTrue()
    {
        var buyer = Builders.MakeBuyer(password: "clave123");

        Assert.True(buyer.verifyPassword("clave123"));
    }

    [Fact]
    public void VerifyPassword_WrongPassword_ReturnsFalse()
    {
        var buyer = Builders.MakeBuyer(password: "clave123");

        Assert.False(buyer.verifyPassword("otra"));
    }

    [Fact]
    public void VerifyPassword_IsCaseSensitive()
    {
        var buyer = Builders.MakeBuyer(password: "Clave123");

        Assert.False(buyer.verifyPassword("clave123"));
    }

    [Fact]
    public void VerifyPassword_Null_Throws()
    {
        var buyer = Builders.MakeBuyer();

        Assert.Throws<ArgumentNullException>(() => buyer.verifyPassword(null!));
    }

    [Fact]
    public void ChangePassword_CorrectCurrentPassword_UpdatesPassword()
    {
        var buyer = Builders.MakeBuyer(password: "vieja");

        buyer.changePassword("vieja", "nueva");

        Assert.Equal("nueva", buyer.Password);
        Assert.True(buyer.verifyPassword("nueva"));
    }

    [Fact]
    public void ChangePassword_WrongCurrentPassword_ThrowsAndKeepsPassword()
    {
        var buyer = Builders.MakeBuyer(password: "vieja");

        Assert.Throws<UnauthorizedAccessException>(() => buyer.changePassword("incorrecta", "nueva"));
        Assert.Equal("vieja", buyer.Password);
    }

    [Fact]
    public void ChangePassword_NullArguments_Throw()
    {
        var buyer = Builders.MakeBuyer(password: "vieja");

        Assert.Throws<ArgumentNullException>(() => buyer.changePassword(null!, "nueva"));
        Assert.Throws<ArgumentNullException>(() => buyer.changePassword("vieja", null!));
    }
}
