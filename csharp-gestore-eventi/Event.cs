using csharp_gestore_eventi.Exceptions;

namespace csharp_gestore_eventi;

public class Event
{
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

    private DateTime _date;
    public DateTime Date
    {
        get => _date;

        set
        {
            if (value < _date)
            {
                throw new InvalidOperationException("The date cannot be in the past.");
            }

            _date = value;
        }
    }

    private int _maxSeats;
    public int MaxSeats
    {
        get => _maxSeats;

        private init
        {
            if (value <= 0)
            {
                throw new InvalidSeatsException("You must provide at least one seat.");
            }

            _maxSeats = value;
        }
    }

    private int _reservedSeats;
    public int ReservedSeats
    {
        get => _reservedSeats;

        private set
        {
            if (value < 0)
            {
                throw new InvalidSeatsException("Seats cannot be negatives.");
            }
            
            if (value > _maxSeats)
            {
                throw new InvalidSeatsException("The maximum seats capacity is been reached.");
            }

            _reservedSeats = value;
        }
    }

    public int RemainingSeats => _maxSeats - _reservedSeats;

    public Event(string title, DateTime date, int maxSeats)
    {
        Title = title;
        Date = date;
        MaxSeats = maxSeats;
        ReservedSeats = 0;
    }

    public void ReserveSeats(int seatsToReserve)
    {
        if (IsEventOver())
        {
            throw new EventOverException("You cannot reserve seats in a past event.");
        }
        
        if (seatsToReserve <= 0)
        {
            throw new InvalidSeatsException("You must provide a positive number of seats.");
        }
        
        ReservedSeats += seatsToReserve;
    }

    public void CancelSeats(int seatsToCancel)
    {
        if (IsEventOver())
        {
            throw new EventOverException("You cannot cancel seats in a past event.");
        }
        
        if (seatsToCancel <= 0)
        {
            throw new InvalidSeatsException("You must provide a positive number of seats.");
        }
        
        ReservedSeats -= seatsToCancel;
    }

    public override string ToString()
    {
        return $"{Date:dd/MM/yyyy} - {Title}";
    }

    public override int GetHashCode()
    {
        return Title.GetHashCode() + Date.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Event other && Title == other.Title && Date == other.Date;
    }

    private bool IsEventOver()
    {
        return Date < DateTime.Now;
    }
}