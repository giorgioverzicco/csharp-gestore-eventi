using FluentAssertions;

namespace csharp_gestore_eventi.UnitTests;

public class EventProgrammerTests
{
    private EventProgrammer _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new EventProgrammer("DummyTest");
    }

    [Test]
    public void Add_ShouldAddNewEventToList_WhenEventIsValid()
    {
        var newEvent = new Event("test", DateTime.Now, 150);

        _sut.Add(newEvent);

        _sut.Events.Should().Contain(newEvent);
    }
    
    [Test]
    public void Add_ShouldThrowException_WhenEventIsAlreadyDefined()
    {
        var newEvent = new Event("test", DateTime.Parse("2022/10/25"), 150);
        _sut.Add(newEvent);
        
        Action action = () => _sut.Add(newEvent); // duplicate event

        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("This event is already in the list.");
    }

    [Test]
    public void GetByDate_ShouldReturnAListWithOneEvent_WhenGivenTheExactDate()
    {
        var date = DateTime.Parse("2022/10/25");
        var newEvent = new Event("test", date, 150);
        _sut.Add(newEvent);
        
        List<Event> events = _sut.GetByDate(DateOnly.FromDateTime(date));

        events.Should().HaveCount(1);
    }
    
    [Test]
    public void GetByDate_ShouldReturnAListWithTwoEvent_WhenGivenTheExactDate()
    {
        var date = DateTime.Parse("2022/10/25");
        var newEvent = new Event("test", date, 150);
        var newEvent2 = new Event("test2", date, 130);
        _sut.Add(newEvent);
        _sut.Add(newEvent2);
        
        List<Event> events = _sut.GetByDate(DateOnly.FromDateTime(date));

        events.Should().HaveCount(2);
    }
    
    [Test]
    public void GetByDate_ShouldReturnAnEmptyList_WhenGivenTheExactDate()
    {
        var date = DateTime.Parse("2022/10/25");
        var newEvent = new Event("test", DateTime.Parse("2022/06/20"), 150);
        var newEvent2 = new Event("test2", DateTime.Parse("2022/06/20"), 130);
        _sut.Add(newEvent);
        _sut.Add(newEvent2);
        
        List<Event> events = _sut.GetByDate(DateOnly.FromDateTime(date));

        events.Should().HaveCount(0);
    }

    [Test]
    public void EventProgrammerListToString_ShouldReturnStringRepresentation_WhenEventListIsGiven()
    {
        var events = new List<Event>()
        {
            new("test", DateTime.Parse("2022/10/25"), 150),
            new("test1", DateTime.Parse("2022/10/25"), 150)
        };
        
        var result = EventProgrammer.ListToString(events);

        result.Should().Be("25/10/2022 - test\n25/10/2022 - test1\n");
    }

    [Test]
    public void Count_ShouldReturnEventsCount_WhenInvoked()
    {
        var events = new List<Event>()
        {
            new("test", DateTime.Parse("2022/10/25"), 150),
            new("test1", DateTime.Parse("2022/10/25"), 150)
        };
        _sut.Add(events[0]);
        _sut.Add(events[1]);
        
        var result = _sut.Count;

        result.Should().Be(2);
    }

    [Test]
    public void Empty_ShouldRemoveAnyEventInTheList_WhenInvoked()
    {
        var events = new List<Event>()
        {
            new("test", DateTime.Parse("2022/10/25"), 150),
            new("test1", DateTime.Parse("2022/10/25"), 150)
        };
        _sut.Add(events[0]);
        _sut.Add(events[1]);
        
        _sut.Empty();

        _sut.Count.Should().Be(0);
    }

    [Test]
    public void GetListText_ShouldReturnTheEventsAndTheNameOfEventProgrammer_WhenInvoked()
    {
        var events = new List<Event>()
        {
            new("test", DateTime.Parse("2022/10/25"), 150),
            new("test1", DateTime.Parse("2022/10/25"), 150)
        };
        _sut.Add(events[0]);
        _sut.Add(events[1]);
        
        var result = _sut.GetListText();

        result.Should().Be(
            $"{_sut.Title}:\n\t{events[0]}\n\t{events[1]}\n");
    }
}