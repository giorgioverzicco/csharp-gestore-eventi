using csharp_gestore_eventi;
using csharp_gestore_eventi.Exceptions;

var eventProgrammer = CreateEventProgrammer();
CreateEvents(eventProgrammer);
PrintTotalEvents(eventProgrammer);
PrintListOfEvents(eventProgrammer);
PrintEventsListFromDate(eventProgrammer);

/*
var newEvent = CreateEvent();
var canReserveSeats = ReserveSeats(newEvent);
if (!canReserveSeats)
{
    return;
}

var canCancelSeats = CancelSeats(newEvent);
if (!canCancelSeats)
{
    return;
}

PrintReservedAndRemainingSeats(newEvent);
*/

void PrintEventsListFromDate(EventProgrammer? programmer)
{
    var date = GetDate();
    var events = programmer?.GetByDate(date);
    var listText = EventProgrammer.ListToString(events!);

    Console.WriteLine(listText);
}

DateOnly GetDate()
{
    var retry = false;
    DateOnly date;

    do
    {
        Console.WriteLine("Please, insert a date to know how many events will be there:");
        Console.Write("> ");
        
        string dateText = Console.ReadLine()!;

        try
        {
            date = DateOnly.FromDateTime(DateTime.Parse(dateText));
            retry = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            retry = false;
        }
    } while (!retry);
    
    return date;
}

void PrintListOfEvents(EventProgrammer? programmer)
{
    Console.WriteLine($"{programmer?.GetListText()}");
}

void PrintTotalEvents(EventProgrammer? programmer)
{
    Console.WriteLine($"The total events are: {programmer?.Count}");
}

bool CancelSeats(Event? theEvent)
{
    do
    {
        try
        {
            var cancelSeats = AskUserIfWantCancelSeats();
            if (!cancelSeats)
            {
                return true;
            }
            
            Console.WriteLine("Please, enter how many seats you want to cancel:");

            Console.Write("> ");
            var seatsToCancel = Convert.ToInt32(Console.ReadLine());
            
            theEvent?.CancelSeats(seatsToCancel);
            
            PrintReservedAndRemainingSeats(theEvent);
        }
        catch (EventOverException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Try with another event.");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Try again.");
            Console.WriteLine();
        }
    } while (true);
}

bool AskUserIfWantCancelSeats()
{
    Console.WriteLine("Do you want to cancel some seats? [Y/N]");
    
    Console.Write("> ");
    var cancelSeats = Console.ReadKey().KeyChar == 'y';
    
    Console.WriteLine();
    
    return cancelSeats;
}

bool ReserveSeats(Event? theEvent)
{
    var retry = false;
    
    do
    {
        try
        {
            Console.WriteLine("Please, enter how many seats you want to reserve:");

            Console.Write("> ");
            var seatsToReserve = Convert.ToInt32(Console.ReadLine());

            theEvent?.ReserveSeats(seatsToReserve);
            
            PrintReservedAndRemainingSeats(theEvent);

            retry = false;
        }
        catch (EventOverException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Try with another event.");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Try again.");
            Console.WriteLine();
            retry = true;
        }
    } while (retry);

    return true;
}

void PrintReservedAndRemainingSeats(Event? theEvent)
{
    Console.WriteLine();
    Console.WriteLine($"Number of reserved seats: {theEvent?.ReservedSeats}");
    Console.WriteLine($"Number of free seats: {theEvent?.RemainingSeats}");
    Console.WriteLine();
}

Event? CreateEvent()
{
    var retry = false;
    Event? theEvent = null;

    do
    {
        try
        {
            Console.WriteLine("Please, enter the event details:");

            Console.Write("Title: ");
            var title = Console.ReadLine()!;

            Console.Write("Date (dd/mm/yyyy): ");
            var date = DateTime.Parse(Console.ReadLine()!);

            Console.Write("Max Seats: ");
            var maxSeats = Convert.ToInt32(Console.ReadLine());
        
            theEvent = new Event(title, date, maxSeats);
            retry = false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Try again.");
            Console.WriteLine();
            retry = true;
        }
    } while (retry);

    return theEvent;
}

void CreateEvents(EventProgrammer? programmer)
{
    Console.WriteLine("How many events do you want to insert?");
    
    Console.Write("> ");
    int numOfTotalEvents = Convert.ToInt32(Console.ReadLine());

    for (int i = 1; i <= numOfTotalEvents; i++)
    {
        Console.WriteLine($"Event #{i}");
        
        var newEvent = CreateEvent();

        try
        {
            programmer?.Add(newEvent!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            i--;
        }
        
        Console.WriteLine();
    }
}

EventProgrammer? CreateEventProgrammer()
{
    var retry = false;
    EventProgrammer? eventProgrammer = null;

    do
    {
        try
        {
            Console.WriteLine("Please, enter the event programmer details:");

            Console.Write("Title: ");
            var title = Console.ReadLine()!;

            eventProgrammer = new EventProgrammer(title);
            retry = false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Try again.");
            Console.WriteLine();
            retry = true;
        }
    } while (retry);

    return eventProgrammer;
}
