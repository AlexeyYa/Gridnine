using System;
using System.Collections;
using System.Collections.Generic;

namespace Gridnine.FlightCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var fb = new Gridnine.FlightCodingTest.FlightBuilder();
            var flights = fb.GetFlights();

            FilterCollection filterCollection1 = new FilterCollection();
            filterCollection1.AddFilter(new FlightBeforeDateTimeFilter(DateTime.Now, false));

            FilterCollection filterCollection2 = new FilterCollection();
            filterCollection2.AddFilter(new FlightValidateSegmentsFilter());

            FilterCollection filterCollection3 = new FilterCollection();
            filterCollection3.AddFilter(new FlightLandTimeFilter(new TimeSpan(2, 0, 0)));

            var filtered1 = filterCollection1.GetFilteredList(flights);
            var filtered2 = filterCollection2.GetFilteredList(flights);
            var filtered3 = filterCollection3.GetFilteredList(flights);

            PrintFlights(flights);
            Console.WriteLine("\nFilter 1");
            PrintFlights(filtered1);
            Console.WriteLine("\nFilter 2");
            PrintFlights(filtered2);
            Console.WriteLine("\nFilter 3");
            PrintFlights(filtered3);
        }

        static void PrintFlights(IEnumerable<Flight> flights)
        {
            foreach (var flight in flights)
            {
                foreach (var segment in flight.Segments)
                    Console.Write(segment.DepartureDate.ToString("g") + " - " + segment.ArrivalDate.ToString("g") + "; ");
                Console.Write('\n');
            }
        }
    }
}
