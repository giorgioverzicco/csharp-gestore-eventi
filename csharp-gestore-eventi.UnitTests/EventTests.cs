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

    [Test]
    [TestCase(15, 5, 10)]
    [TestCase(10, 2, 8)]
    [TestCase(30, 30, 0)]
    public void CancelSeats_ShouldDecreaseReservedSeats_WhenInputIsValid(
        int reservedSeats, 
        int seatsToCancel, 
        int expected)
    {
        _sut.ReserveSeats(reservedSeats);
        
        _sut.CancelSeats(seatsToCancel);

        _sut.ReservedSeats.Should().Be(expected);
    }
    
    [Test]
    [TestCase(15, 16)]
    [TestCase(10, 15)]
    [TestCase(30, 50)]
    public void CancelSeats_ShouldThrowException_WhenSeatsToCancelAreOverMaximumSeats(
        int reservedSeats, 
        int seatsToCancel)
    {
        _sut.ReserveSeats(reservedSeats);
        
        Action action = () => _sut.CancelSeats(seatsToCancel);

        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Negative numbers are not allowed.");
    }
    
    [Test]
    public void CancelSeats_ShouldThrowException_WhenEventIsAlreadyOver()
    {
        var sut = new Event("DummyTitle", DateTime.Parse("2019/12/10 00:00:00"), 30);
        var seatsToCancel = 5;
        
        Action action = () => sut.CancelSeats(seatsToCancel);

        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("You cannot cancel seats in a past event.");
    }
    
    [Test]
    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-12)]
    public void CancelSeats_ShouldThrowException_WhenNegativesOrZeroArePassed(int seatsToCancel)
    {
        Action action = () => _sut.CancelSeats(seatsToCancel);

        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("You must provide a positive number of seats.");
    }
}