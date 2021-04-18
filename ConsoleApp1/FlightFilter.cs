using System;
using System.Collections.Generic;
using System.Text;


namespace Gridnine.FlightCodingTest
{
    public interface IFlightFilter
    {
        public bool Check(Flight flight);
    }

    public class FlightBeforeDateTimeFilter : IFlightFilter
    {
        private DateTime dateTime;
        private bool isTrueBefore;
        public FlightBeforeDateTimeFilter(DateTime dateTime, bool isTrueBefore=true)
        {
            this.dateTime = dateTime;
            this.isTrueBefore = isTrueBefore;
        }
        public bool Check(Flight flight)
        {
            //assert(flight.Segments.Count > 0);

            if (flight.Segments[0].DepartureDate < dateTime)
                return isTrueBefore;
            else
                return !isTrueBefore;
        }
    }

    public class FlightValidateSegmentsFilter : IFlightFilter
    {
        public FlightValidateSegmentsFilter() { }
        public bool Check(Flight flight)
        {
            foreach (var segment in flight.Segments)
                if (segment.ArrivalDate < segment.DepartureDate)
                    return false;

            return true;
        }
    }

    public class FlightLandTimeFilter : IFlightFilter
    {
        private TimeSpan timeDifference;

        public FlightLandTimeFilter(TimeSpan timeDifference)
        {
            this.timeDifference = timeDifference;
        }
        public bool Check(Flight flight)
        {
            for (int i = 1; i < flight.Segments.Count; i++)
            {
                if (flight.Segments[i].DepartureDate - flight.Segments[i - 1].ArrivalDate > timeDifference)
                    return false;
            }
            return true;
        }
    }

    public class FilterCollection
    {
        private List<IFlightFilter> flightFilters;

        public FilterCollection()
        {
            flightFilters = new List<IFlightFilter>();
        }
        public void AddFilter(IFlightFilter flightFilter)
        {
            flightFilters.Add(flightFilter);
        }
        private bool Check(Flight flight)
        {
            foreach (var filter in flightFilters)
                if (!filter.Check(flight))
                    return false;

            return true;
        }
        public List<Flight> GetFilteredList(IEnumerable<Flight> flights)
        {
            List<Flight> result = new List<Flight>();
            foreach (var flight in flights)
            {
                if (Check(flight))
                    result.Add(flight);
            }

            return result;
        }
    }
}
