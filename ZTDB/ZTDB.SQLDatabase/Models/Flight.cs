using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZTDB.SQLDatabase.Models
{
    [Table("Flights")]
    public class Flight
    {
        [Key]
        public int Id { get; set; }

        public DateTime FlightDate { get; set; }
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        public int OpCarrierFlightNumber { get; set; }
        public int OriginLocationId { get; set; }
        public Location OriginLocation { get; set; }
        public int DestinationLocationId { get; set; }
        public Location DestinationLocation { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal PlannedDepartureTime { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal ActualDepartureTime { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal DepartureDelay { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal TaxiOut { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal TaxiIn { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal WheelsOn { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal WheelsOff { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal PlannedArrivalTime { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal ActualArrivalTime { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal ArrivalDelay { get; set; }

        public int? CancelCodeId { get; set; }
        public CancelCode CancelCode { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal Diverted { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal PlannedElapsedTime { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal ActualElapsedTime { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal AirTime { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal Distance { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal? CarrierDelay { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal? WeatherDelay { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal? NasDelay { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal? SecurityDelay { get; set; }

        [Column(TypeName = "Numeric(18,2)")]
        public decimal? LateAircraftDelay { get; set; }
    }
}