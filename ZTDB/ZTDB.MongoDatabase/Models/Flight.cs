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
        public int OP_CARRIER_FL_NUM { get; set; }
        public string ORIGIN { get; set; }
        public string DEST { get; set; }
        public int CRS_DEP_TIME { get; set; }
        public int DEP_TIME { get; set; }
        public int DEP_DELAY { get; set; }
        public int TAXI_OUT { get; set; }
        public int WHEELS_OFF { get; set; }
        public int TAXI_IN { get; set; }
        public int WHEELS_ON { get; set; }
        public int CRS_ARR_TIME { get; set; }
        public int ARR_TIME { get; set; }
        public int ARR_DELAY { get; set; }
        public int CANCELLED { get; set; }
        public string CANCELLATION_CODE { get; set; }
        public int DIVERTED { get; set; }
        public int CRS_ELAPSED_TIME { get; set; }
        public int ACTUAL_ELAPSED_TIME { get; set; }
        public int AIR_TIME { get; set; }
        public int DISTANCE { get; set; }
        public dynamic CARRIER_DELAY { get; set; }
        public dynamic WEATHER_DELAY { get; set; }
        public dynamic NAS_DELAY { get; set; }
        public dynamic SECURITY_DELAY { get; set; }
        public dynamic LATE_AIRCRAFT_DELAY { get; set; }
        [BsonElement("Unnamed: 27")]
        public dynamic Unnamed { get; set; }
    }
}
