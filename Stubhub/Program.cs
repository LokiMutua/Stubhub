namespace Stubhub {
    public class Event
    {
        public string Name{ get; set; }
        public string City{ get; set; }
    }
    
    public class Customer
    {
        public string Name{ get; set; }
        public string City{ get; set; }
    }

    public class Solution
    {
        static void Main(string[] args)
        {
            List<Event> events = new List<Event>
            {
                new Event {Name = "Phantom of the Opera", City = "New York"},
                new Event {Name = "Metallica", City = "Los Angeles"},
                new Event {Name = "Metallica", City = "New York"},
                new Event {Name = "Metallica", City = "Boston"},
                new Event {Name = "LadyGaGa", City = "New York"},
                new Event {Name = "LadyGaGa", City = "Boston"},
                new Event {Name = "LadyGaGa", City = "Chicago"},
                new Event {Name = "LadyGaGa", City = "San Francisco"},
                new Event {Name = "LadyGaGa", City = "Washington"}
            };

            
            var customer = new Customer {Name = "Mr. Fake", City = "New York"};

            //Get all customer events using linq and lambda expressions
            List<Event> customerEvents = events.FindAll(e => e.City == customer.City);

            // 1. TASK
            /*
             * Add customer events above to customer's email
             */
            foreach (Event @event in customerEvents)
            {
                AddToEmail(customer, @event);
            }

            /*
            * We want you to send an email to this customer with all events in their city
            * Just call AddToEmail(customer, event) for each event you think they should get
            */
        }

        // You do not need to know how these methods work
        static void AddToEmail(Customer c, Event e, int? price = null)
        {
            var distance = GetDistance(c.City, e.City);
            Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
                                  + (distance > 0 ? $" ({distance} miles away)" : "")
                                  + (price.HasValue ? $" for ${price}" : ""));
        }

        static int GetPrice(Event e)
        {
            return (AlphebiticalDistance(e.City, "") + AlphebiticalDistance(e.Name, "")) / 10;
        }

        static int GetDistance(string fromCity, string toCity)
        {
            return AlphebiticalDistance(fromCity, toCity);
        }

        private static int AlphebiticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;
            for (i = 0; i < Math.Min(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                result += Math.Abs(s[i] - t[i]);
            }

            for (; i < Math.Max(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                result += s.Length > t.Length ? s[i] : t[i];
            }

            return result;
        }

        //TASK 2
        
        /*
         * This method gets 5 events closest to customer
         */
        static List<Event> GetClosestLocations(Customer customer, List<Event> events)
        {
            List<KeyValuePair<Event, int>> eventsDistance = new List<KeyValuePair<Event, int>>();
            foreach (Event @event in events)
            {
                int distance = GetDistance(customer.City, @event.City);
                if (distance == 0)
                {
                    continue;
                }

                eventsDistance.Add(KeyValuePair.Create(@event, distance));
            }

            //Sort the list ascending and take 5
            eventsDistance.Sort(
                delegate(KeyValuePair<Event, int> pair1, KeyValuePair<Event, int> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                });

            var selectedEvents = eventsDistance.Take(5);

            return eventsDistance.Select(v => v.Key).ToList();
        }

        //Task 3

        /*
         * We could asynchronous programing or multi threading
         */
        static List<Event> GetClosestLocations2(Customer customer, List<Event> events)
        {
            List<KeyValuePair<Event, int>> eventsDistance = new List<KeyValuePair<Event, int>>();
            foreach (Event @event in events)
            {
                try
                {
                    int distance = GetDistance(customer.City, @event.City);
                    eventsDistance.Add(KeyValuePair.Create(@event, distance));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }

            //Sort the list ascending and take 5
            eventsDistance.Sort(
                delegate(KeyValuePair<Event, int> pair1, KeyValuePair<Event, int> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                });

            var selectedEvents = eventsDistance.Take(5);

            return eventsDistance.Select(v => v.Key).ToList();


        }
        //Task 4
        /*
         * We can use asynchronous program to allow the other tasks to continue while the method executes
         * 
         */



        //Task 5
        static List<KeyValuePair<Event, int>> EventsWithPrice(List<Event> events)
        {
            List<KeyValuePair<Event, int>> selectedEvents = new List<KeyValuePair<Event, int>>();

            foreach (var @event in events)
            {
                int price = GetPrice(@event);
                selectedEvents.Add(KeyValuePair.Create(@event, price));
            }

            /* Select desired events based on price
             * selectedEvents.Where(kv => kv.Value == 4).ToList();
             * 
             */
            return selectedEvents;
        }
    }

}

/*
var customers = new List<Customer>{
new Customer{ Name = "Nathan", City = "New York"},
new Customer{ Name = "Bob", City = "Boston"},
new Customer{ Name = "Cindy", City = "Chicago"},
new Customer{ Name = "Lisa", City = "Los Angeles"}
};
*/