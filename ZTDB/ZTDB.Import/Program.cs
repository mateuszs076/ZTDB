using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ZTDB.SQLDatabase;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.Import
{
    internal class Program
    {
        private const int NUMBER_OF_RECORDS = 2000;
        private const int NUMBER_OF_ERRORS_TO_SHOW = 10;
        private static List<string> errorString;
        private static int errorsNumber = 0;
        private static NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

        private static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("==== MENU ====");
                Console.WriteLine("1.\tImport z tabeli DataToImport");
                Console.WriteLine("2.\tImport z tabeli DataToImport z usuwaniem\n\tzaimportowanych rekordów z tabeli DataToImport");
                Console.WriteLine("Q.\tWyjście");
                var key = Console.ReadKey();
                Console.Write("\b \b");
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    errorsNumber = 0;
                    errorString = new List<string>();
                    Console.WriteLine("Rozpoczynam import");
                    ImportFromTable();
                    if (errorsNumber != 0)
                    {
                        WriteErrors();
                    }
                }
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    errorsNumber = 0;
                    errorString = new List<string>();
                    Console.WriteLine("Rozpoczynam import");
                    ImportFromTable(true);
                    if (errorsNumber != 0)
                    {
                        WriteErrors();
                    }
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    exit = true;
                }
            }
        }

        private static void WriteErrors()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Podczas importu wystąpiły pewne problemy w ilości: " + errorsNumber);
                Console.WriteLine("1. Wyświetl spis problemów");
                Console.WriteLine("Q. Wyjście");
                var key = Console.ReadKey();
                Console.Write("\b \b");
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    bool end = false;
                    int i = 0;
                    int numberToSkip = 0;
                    while (!end)
                    {
                        foreach (var item in errorString.Skip(numberToSkip).Take(NUMBER_OF_ERRORS_TO_SHOW))
                        {
                            Console.WriteLine(item);
                        }
                        ++i;
                        numberToSkip = NUMBER_OF_ERRORS_TO_SHOW * i;
                        if (numberToSkip > errorString.Count())
                        {
                            end = true;
                        }
                        else
                        {
                            Console.WriteLine("Wyświetlono rekordy od " + (numberToSkip - NUMBER_OF_ERRORS_TO_SHOW) + " do " + numberToSkip + " z " + errorsNumber);
                            Console.WriteLine("Wciśnij dowolny klawisz by wyświetlać dalej");
                            Console.WriteLine("Q. Wyjście");
                            var key2 = Console.ReadKey();
                            Console.Write("\b \b");
                            if (key2.Key == ConsoleKey.Q)
                            {
                                end = true;
                            }
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    exit = true;
                }
            }
        }

        private static void ImportFromTable(bool doDelete = false)
        {
            using (var context = new SQLContext())
            {
                context.Database.Migrate();
                context.Database.SetCommandTimeout(300);
                bool end = false;
                int i = 0;
                var codes = context.Set<CancelCode>().ToDictionary(a => a.Code, a => a);
                var airlines = context.Set<Airline>().ToDictionary(a => a.Code, a => a);
                var locations = context.Set<Location>().ToDictionary(a => a.Code, a => a);
                var count = context.Set<DataToImport>().LongCount();
                Console.WriteLine("Ilość rekordów do zaimportowania: " + count);
                while (!end)
                {
                    List<Flight> flights = new List<Flight>();
                    List<DataToImport> data;
                    List<DataToImport> recordsToDelete = new List<DataToImport>();
                    if (doDelete)
                    {
                        data = context.Set<DataToImport>().Skip(NUMBER_OF_RECORDS * i).Take(NUMBER_OF_RECORDS).ToList();
                    }
                    else
                    {
                        data = context.Set<DataToImport>().Skip(errorsNumber).Take(NUMBER_OF_RECORDS).ToList();
                    }
                    foreach (var record in data)
                    {
                        if (!GetOrCreateCancelCode(record, codes, out CancelCode code))
                        {
                            code = null;
                        }
                        if (!GetOrCreateAirline(record, airlines, out Airline airline))
                        {
                            continue;
                        }
                        if (!GetOrCreateDestinationLocation(record, locations, out Location destLocation))
                        {
                            continue;
                        }
                        if (!GetOrCreateOriginLocation(record, locations, out Location originLocation))
                        {
                            continue;
                        }
                        if (!CreateFlight(record, originLocation, destLocation, code, airline, out Flight flight))
                        {
                            continue;
                        }
                        flights.Add(flight);
                        if (doDelete)
                        {
                            recordsToDelete.Add(record);
                        }
                    }
                    if (doDelete)
                    {
                        context.Set<DataToImport>().RemoveRange(recordsToDelete);
                    }
                    context.Set<Flight>().AddRange(flights);
                    context.SaveChanges();
                    Console.WriteLine((NUMBER_OF_RECORDS * i) + data.Count + "/" + count);
                    ++i;
                    if (data.Count < NUMBER_OF_RECORDS)
                    {
                        end = true;
                    }
                }
            }
        }

        private static bool CreateFlight(DataToImport record, Location originLocation, Location destLocation, CancelCode code, Airline airline, out Flight flight)
        {
            flight = new Flight();

            if (DateTime.TryParse(record.FL_DATE, out DateTime flightDate))
            {
                flight.FlightDate = flightDate;
            }
            else
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t FL_DATE" + record.FL_DATE);
                return false;
            }

            if (int.TryParse(record.OP_CARRIER_FL_NUM, out int opCarrierFlightNumber))
            {
                flight.OpCarrierFlightNumber = opCarrierFlightNumber;
            }
            else
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t OP_CARRIER_FL_NUM" + record.OP_CARRIER_FL_NUM);
                return false;
            }

            if (decimal.TryParse(record.CRS_DEP_TIME, style, culture, out decimal plannedDepartureTime))
            {
                flight.PlannedDepartureTime = plannedDepartureTime;
            }
            else
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t CRS_DEP_TIME" + record.CRS_DEP_TIME);
                return false;
            }

            if (decimal.TryParse(record.DEP_TIME, style, culture, out decimal actualDepartureTime))
            {
                flight.ActualDepartureTime = actualDepartureTime;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t DEP_TIME" + record.DEP_TIME);
                    return false;
                }
            }

            if (decimal.TryParse(record.DEP_DELAY, style, culture, out decimal departureDelay))
            {
                flight.DepartureDelay = departureDelay;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t DEP_DELAY" + record.DEP_DELAY);
                    return false;
                }
            }

            if (decimal.TryParse(record.TAXI_OUT, style, culture, out decimal taxiOut))
            {
                flight.TaxiOut = taxiOut;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t TAXI_OUT" + record.TAXI_OUT);
                    return false;
                }
            }

            if (decimal.TryParse(record.TAXI_IN, style, culture, out decimal taxiIn))
            {
                flight.TaxiIn = taxiIn;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t TAXI_IN" + record.TAXI_IN);
                    return false;
                }
            }

            if (decimal.TryParse(record.WHEELS_OFF, style, culture, out decimal wheelsOff))
            {
                flight.WheelsOff = wheelsOff;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t WHEELS_OFF" + record.WHEELS_OFF);
                    return false;
                }
            }

            if (decimal.TryParse(record.WHEELS_ON, style, culture, out decimal wheelsOn))
            {
                flight.WheelsOn = wheelsOn;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t WHEELS_ON" + record.WHEELS_ON);
                    return false;
                }
            }

            if (decimal.TryParse(record.CRS_ARR_TIME, style, culture, out decimal plannedArrivalTime))
            {
                flight.PlannedArrivalTime = plannedArrivalTime;
            }
            else
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t CRS_ARR_TIME" + record.CRS_ARR_TIME);
                return false;
            }

            if (decimal.TryParse(record.ARR_TIME, style, culture, out decimal actualArrivalTime))
            {
                flight.ActualArrivalTime = actualArrivalTime;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t ARR_TIME" + record.ARR_TIME);
                    return false;
                }
            }

            if (decimal.TryParse(record.ARR_DELAY, style, culture, out decimal arrivalDelay))
            {
                flight.ArrivalDelay = arrivalDelay;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t ARR_DELAY" + record.ARR_DELAY);
                    return false;
                }
            }

            if (decimal.TryParse(record.DIVERTED, style, culture, out decimal diverted))
            {
                flight.Diverted = diverted;
            }
            else
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t DIVERTED" + record.DIVERTED);
                return false;
            }

            if (decimal.TryParse(record.CRS_ELAPSED_TIME, style, culture, out decimal plannedElapsedTime))
            {
                flight.PlannedElapsedTime = plannedElapsedTime;
            }
            else
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t CRS_ELAPSED_TIME" + record.CRS_ELAPSED_TIME);
                return false;
            }

            if (decimal.TryParse(record.ACTUAL_ELAPSED_TIME, style, culture, out decimal actualElapsedTime))
            {
                flight.ActualElapsedTime = actualElapsedTime;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t ACTUAL_ELAPSED_TIME" + record.ACTUAL_ELAPSED_TIME);
                    return false;
                }
            }

            if (decimal.TryParse(record.AIR_TIME, style, culture, out decimal airTime))
            {
                flight.AirTime = airTime;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t AIR_TIME" + record.AIR_TIME);
                    return false;
                }
            }

            if (decimal.TryParse(record.DISTANCE, style, culture, out decimal distance))
            {
                flight.Distance = distance;
            }
            else
            {
                if (code != null)
                {
                    ++errorsNumber;
                    errorString.Add("id:" + record.Id + "\t DISTANCE" + record.DISTANCE);
                    return false;
                }
            }

            if (decimal.TryParse(record.CARRIER_DELAY, style, culture, out decimal carrierDelay))
            {
                flight.CarrierDelay = carrierDelay;
            }
            else
            {
                flight.CarrierDelay = null;
            }

            if (decimal.TryParse(record.WEATHER_DELAY, style, culture, out decimal weatherDelay))
            {
                flight.WeatherDelay = weatherDelay;
            }
            else
            {
                flight.WeatherDelay = null;
            }

            if (decimal.TryParse(record.NAS_DELAY, style, culture, out decimal nasDelay))
            {
                flight.NasDelay = nasDelay;
            }
            else
            {
                flight.NasDelay = null;
            }

            if (decimal.TryParse(record.SECURITY_DELAY, style, culture, out decimal securityDelay))
            {
                flight.SecurityDelay = securityDelay;
            }
            else
            {
                flight.SecurityDelay = null;
            }

            if (decimal.TryParse(record.LATE_AIRCRAFT_DELAY, style, culture, out decimal lateAircraftDelay))
            {
                flight.LateAircraftDelay = lateAircraftDelay;
            }
            else
            {
                flight.LateAircraftDelay = null;
            }
            flight.Airline = airline;
            flight.DestinationLocation = destLocation;
            flight.OriginLocation = originLocation;
            if (code != null)
            {
                flight.CancelCode = code;
            }

            return true;
        }

        private static bool GetOrCreateOriginLocation(DataToImport record, Dictionary<string, Location> locations, out Location location)
        {
            if (string.IsNullOrEmpty(record.ORIGIN))
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t ORIGIN" + record.FL_DATE);
                location = new Location();
                return false;
            }
            if (locations.TryGetValue(record.ORIGIN, out Location location1))
            {
                location = location1;
            }
            else
            {
                location = new Location
                {
                    Code = record.ORIGIN
                };
                locations.Add(location.Code, location);
            }
            return true;
        }

        private static bool GetOrCreateDestinationLocation(DataToImport record, Dictionary<string, Location> locations, out Location location)
        {
            if (string.IsNullOrEmpty(record.DEST))
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t DEST" + record.FL_DATE);
                location = new Location();
                return false;
            }
            if (locations.TryGetValue(record.DEST, out Location location1))
            {
                location = location1;
            }
            else
            {
                location = new Location
                {
                    Code = record.DEST
                };
                locations.Add(location.Code, location);
            }
            return true;
        }

        private static bool GetOrCreateAirline(DataToImport record, Dictionary<string, Airline> airlines, out Airline airline)
        {
            if (string.IsNullOrEmpty(record.OP_CARRIER))
            {
                ++errorsNumber;
                errorString.Add("id:" + record.Id + "\t OP_CARRIER" + record.FL_DATE);
                airline = new Airline();
                return false;
            }
            if (airlines.TryGetValue(record.OP_CARRIER, out Airline airline1))
            {
                airline = airline1;
            }
            else
            {
                airline = new Airline
                {
                    Code = record.OP_CARRIER
                };
                airlines.Add(airline.Code, airline);
            }
            return true;
        }

        private static bool GetOrCreateCancelCode(DataToImport record, Dictionary<string, CancelCode> codes, out CancelCode code)
        {
            if (string.IsNullOrEmpty(record.CANCELLATION_CODE))
            {
                code = new CancelCode();
                return false;
            }
            if (codes.TryGetValue(record.CANCELLATION_CODE, out CancelCode code1))
            {
                code = code1;
            }
            else
            {
                code = new CancelCode
                {
                    Code = record.CANCELLATION_CODE
                };
                codes.Add(code.Code, code);
            }
            return true;
        }
    }
}