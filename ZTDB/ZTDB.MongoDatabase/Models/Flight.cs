using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ZTDB.MongoDatabase.Models
{
    public class Flight
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FL_DATE { get; set; }
        public string OP_CARRIER { get; set; }
        public string OP_CARRIER_FL_NUM { get; set; }
        public string ORIGIN { get; set; }
        public string DEST { get; set; }
        public string CRS_DEP_TIME { get; set; }
        public string DEP_TIME { get; set; }
        public string DEP_DELAY { get; set; }
        public string TAXI_OUT { get; set; }
        public string WHEELS_OFF { get; set; }
        public string TAXI_IN { get; set; }
        public string WHEELS_ON { get; set; }
        public string CRS_ARR_TIME { get; set; }
        public string ARR_TIME { get; set; }
        public string ARR_DELAY { get; set; }
        public string CANCELLED { get; set; }
        public string CANCELLATION_CODE { get; set; }
        public string DIVERTED { get; set; }
        public string CRS_ELAPSED_TIME { get; set; }
        public string ACTUAL_ELAPSED_TIME { get; set; }
        public string AIR_TIME { get; set; }
        public string DISTANCE { get; set; }
        public string CARRIER_DELAY { get; set; }
        public string WEATHER_DELAY { get; set; }
        public string NAS_DELAY { get; set; }
        public string SECURITY_DELAY { get; set; }
        public string LATE_AIRCRAFT_DELAY { get; set; }
    }
}
