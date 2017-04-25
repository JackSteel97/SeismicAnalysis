using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicAnalysis {
    class SeismicRecord : ICloneable{
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

        public int Year {
            get {
                return this.year;
            }
        }

        public int Month {
            get {
                return this.month;
            }
        }

        public int Day {
            get {
                return this.day;
            }
        }

        public string Time {
            get {
                return this.time;
            }
        }

        public decimal Magnitude {
            get {
                return this.mag;
            }
        }

        public decimal Lat {
            get {
                return this.lat;
            }
        }

        public decimal Lon {
            get {
                return this.lon;
            }
        }

        public decimal Depth {
            get {
                return this.depth;
            }
        }

        public string Region {
            get {
                return this.region;
            }
        }

        public string ID {
            get {
                return this.id;
            }
        }

        public long Timestamp {
            get {
                return this.timestamp;
            }
        }

        public SeismicRecord(int year, string month, int day, string time, decimal magnitude, decimal lat, decimal lon, decimal depth, string region, string id, long timestamp) {
            this.year = year;            
            switch (month.Trim().ToLower()) {
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

        public string getMonth() {
            DateTime d = new DateTime(year, month, day);
            return d.ToString("MMMM");
        }

        public string getDay() {
            DateTime d = new DateTime(year, month, day);
            return d.ToString("dd");
        }

        public object Clone() {
            return this.MemberwiseClone();
        }
    }
}
