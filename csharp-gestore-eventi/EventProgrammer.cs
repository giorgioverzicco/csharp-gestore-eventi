using System.Collections.ObjectModel;

namespace csharp_gestore_eventi;

public class EventProgrammer
{
    private List<Event> _events;

    private string _title;
    public string Title
    {
        get => _title;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("You must provide a title.");
            }

            _title = value;
        }
    }
    
    public ReadOnlyCollection<Event> Events { get; }

    public int Count => _events.Count;

    public EventProgrammer(string title)
    {
        Title = title;
        _events = new List<Event>();
        Events = _events.AsReadOnly();
    }

    public void Add(Event theEvent)
    {
        if (_events.Any(x => x.Equals(theEvent)))
        {
            throw new InvalidOperationException("This event is already in the list.");
        }
        
        _events.Add(theEvent);
    }

    public void Empty()
    {
        _events.Clear();
    }

    public List<Event> GetByDate(DateOnly date)
    {
        return _events.FindAll(x => DateOnly.FromDateTime(x.Date) == date);
    }

    public string GetListText()
    {
        var text = $"{Title}:\n";

        foreach (var theEvent in _events)
        {
            text += $"\t{theEvent}\n";
        }

        return text;
    }

    public static string ListToString(List<Event> events)
    {
        var text = "";

        foreach (var theEvent in events)
        {
            text += theEvent + "\n";
        }

        return text;
    }
}