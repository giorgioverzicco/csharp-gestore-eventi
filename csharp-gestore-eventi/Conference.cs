namespace csharp_gestore_eventi;

public class Conference : Event
{
    private string _speaker;
    public string Speaker
    {
        get => _speaker;

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("You must provide a speaker.");
            }

            _speaker = value;
        }
    }
    
    private double _price;
    public double Price
    {
        get => _price;

        private init
        {
            if (value < 0.0)
            {
                throw new ArgumentException("Price must not be negative.");
            }

            _price = value;
        }
    }

    public string FormattedPrice => $"{Price:0.00} euro";

    public Conference(string title, DateTime date, int maxSeats, string speaker, double price)
        : base(title, date, maxSeats)
    {
        Speaker = speaker;
        Price = price;
    }

    public override string ToString()
    {
        return base.ToString() + $" - {Speaker} - {FormattedPrice}";
    }
}