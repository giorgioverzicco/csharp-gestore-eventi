using FluentAssertions;

namespace csharp_gestore_eventi.UnitTests;

public class EventTests
{
    private Event _sut;
    
    [SetUp]
    public void SetUp()
    {
        _sut = new Event("DummyTitle", DateTime.Now.AddDays(14), 30);
    }
    
    [Test]
    [TestCase(5, 5)]
    [TestCase(15, 15)]
    [TestCase(30, 30)]
    public void ReserveSeats_ShouldAddReservedSeats_WhenThereIsEnoughSpace(int seatsToReserve, int expected)
    {
        _sut.ReserveSeats(seatsToReserve);
        
        _sut.ReservedSeats.Should().Be(expected);
    }
    
    [Test]
    [TestCase(31)]
    [TestCase(35)]
    [TestCase(40)]
    public void ReserveSeats_ShouldThrowException_WhenThereIsNoSpace(int seatsToReserve)
    {
        Action action = () => _sut.ReserveSeats(seatsToReserve);

        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("The maximum seats capacity is been reached.");
    }
    
    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-12)]
    public void ReserveSeats_ShouldThrowException_WhenNegativesOrZeroArePassed(int seatsToReserve)
    {
        Action action = () => _sut.ReserveSeats(seatsToReserve);

        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("You must provide a positive number of seats.");
    }
    
    [Test]
    public void ReserveSeats_ShouldThrowException_WhenEventIsAlreadyOver()
    {
        var sut = new Event("DummyTitle", DateTime.Parse("2019/12/10 00:00:00"), 30);
        var seatsToReserve = 5;
        
        Action action = () => sut.ReserveSeats(seatsToReserve);

        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("You cannot reserve seats in a past event.");
    }
}