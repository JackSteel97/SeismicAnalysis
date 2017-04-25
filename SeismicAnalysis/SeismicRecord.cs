using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicAnalysis {
    class SeismicRecord {
        private int year;
        private string month;
        private int day;
        private string time;
        private decimal mag;
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

        public string Month {
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
            this.month = month;
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
    }
}
