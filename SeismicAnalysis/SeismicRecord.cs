using System;

namespace SeismicAnalysis {

    /// <summary>
    /// Class to represent and store data from IRIS repository
    /// </summary>
    internal class SeismicRecord : ICloneable {
        private int year;
        private int month;
        private int day;
        private string time;
        public decimal mag;
        private decimal lat;
        private decimal lon;
        private decimal depth;
        private string region;
        private string id;
        private long timestamp;

        /// <summary>
        /// Year
        /// </summary>
        public int Year {
            get {
                return this.year;
            }
        }

        /// <summary>
        /// Month (1-12)
        /// </summary>
        public int Month {
            get {
                return this.month;
            }
        }

        /// <summary>
        /// Day of month (1-28/29/30/31)
        /// </summary>
        public int Day {
            get {
                return this.day;
            }
        }

        /// <summary>
        /// UTC time
        /// </summary>
        public string Time {
            get {
                return this.time;
            }
        }

        /// <summary>
        /// Magnitude on richter scale
        /// </summary>
        public decimal Magnitude {
            get {
                return this.mag;
            }
        }

        /// <summary>
        /// Latitude
        /// </summary>
        public decimal Lat {
            get {
                return this.lat;
            }
        }

        /// <summary>
        /// Longitude
        /// </summary>
        public decimal Lon {
            get {
                return this.lon;
            }
        }

        /// <summary>
        /// Depth in km
        /// </summary>
        public decimal Depth {
            get {
                return this.depth;
            }
        }

        /// <summary>
        /// Region
        /// </summary>
        public string Region {
            get {
                return this.region;
            }
        }

        /// <summary>
        /// IRIS ID
        /// </summary>
        public string ID {
            get {
                return this.id;
            }
        }

        /// <summary>
        /// Unix timestamp
        /// </summary>
        public long Timestamp {
            get {
                return this.timestamp;
            }
        }

        /// <summary>
        /// DateTime from year, month and day
        /// </summary>
        public DateTime Date {
            get {
                return new DateTime(this.year, this.month, this.day);
            }
        }

        /// <summary>
        /// Construct a new SeismicRecord object
        /// </summary>
        /// <param name="year">The year (i.e. 2015)</param>
        /// <param name="month">The month (i.e. March)</param>
        /// <param name="day">The day of the month (i.e. 25)</param>
        /// <param name="time">The time in the format HH:mm:ss</param>
        /// <param name="magnitude">The magnitude</param>
        /// <param name="lat">The latitude</param>
        /// <param name="lon">The longitude</param>
        /// <param name="depth">The depth</param>
        /// <param name="region">The region</param>
        /// <param name="id">A unique ID</param>
        /// <param name="timestamp">The Unix timestamp</param>
        public SeismicRecord (int year, string month, int day, string time, decimal magnitude, decimal lat, decimal lon, decimal depth, string region, string id, long timestamp) {
            this.year = year;
            //convert month string to integer for easy sorting
            switch(month.Trim().ToLower()) {
                case "january":
                    this.month = 1;
                    break;

                case "february":
                    this.month = 2;
                    break;

                case "march":
                    this.month = 3;
                    break;

                case "april":
                    this.month = 4;
                    break;

                case "may":
                    this.month = 5;
                    break;

                case "june":
                    this.month = 6;
                    break;

                case "july":
                    this.month = 7;
                    break;

                case "august":
                    this.month = 8;
                    break;

                case "september":
                    this.month = 9;
                    break;

                case "october":
                    this.month = 10;
                    break;

                case "november":
                    this.month = 11;
                    break;

                case "december":
                    this.month = 12;
                    break;
            }
            this.day = day;
            this.time = time;
            this.mag = magnitude;
            this.lat = lat;
            this.lon = lon;
            this.depth = depth;
            this.region = region;
            this.id = id;
            this.timestamp = timestamp;
        }

        /// <summary>
        /// Get month string
        /// </summary>
        /// <returns>Full name of month</returns>
        public string getMonth () {
            DateTime d = new DateTime(year, month, day);
            return d.ToString("MMMM");
        }

        /// <summary>
        /// Get day number
        /// </summary>
        /// <returns>Two digit formatted date</returns>
        public string getDay () {
            DateTime d = new DateTime(year, month, day);
            return d.ToString("dd");
        }

        /// <summary>
        /// Shallow copy this object
        /// </summary>
        /// <returns>A memberwise clone of this object</returns>
        public object Clone () {
            return this.MemberwiseClone();
        }
    }
}