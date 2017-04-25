using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeismicAnalysis {
    class Program {
        //year
        private static int[] year;
        //month
        private static string[] month;
        //day
        private static int[] day;
        //time
        private static string[] time;
        //magnitude
        private static decimal[] mag;
        //latitude
        private static decimal[] lat;
        //longitude
        private static decimal[] lon;
        //depth
        private static decimal[] depth;
        //region
        private static string[] region;
        //IRIS_ID
        private static string[] id;
        //timestamp 
        private static long[] timestamp;

        static void Main(string[] args) {
            //read in files: *_1.txt
            populateArraysOne();
            SeismicRecord[] data1 = new SeismicRecord[year.Length];
            data1 = fillRecordArray();
            selectDataToAnalyse();
        }

        //task 1
        private static int selectDataToAnalyse() {
            //output options
            Console.WriteLine("Select what data you want to analyse: ");
            Console.WriteLine("1:\tYear\n2:\tMonth\n3:\tDay\n4:\tTime\n5:\tMagnitude\n6:\tLatitude\n7:\tLongitude\n8:\tDepth\n9:\tRegion\n10:\tIRIS ID\n11:\tTimestamp\n");
            int selection = getUserInput(1, 11);
            return selection;
        }


        private static int getUserInput(int lowerBound, int upperBound) {            
            bool valid = false;
            int val = 0;
            do {
                string input = Console.ReadLine();              
                if (int.TryParse(input,out val)) {
                    if (val >= lowerBound && val <= upperBound) {
                        valid = true;                       
                    }
                }
                if (!valid) {
                    Console.WriteLine("Invalid input, please try again...");
                }
            } while (!valid);
            return val;
        }

        /// <summary>
        /// Populate an array from the global data arrays
        /// </summary>
        /// <returns>An array of SeismicRecord objects</returns>
        private static SeismicRecord[] fillRecordArray() {
            SeismicRecord[] data = new SeismicRecord[year.Length];
            for(int i = 0; i < year.Length; i++) {
                data[i] = new SeismicRecord(year[i], month[i], day[i], time[i], mag[i], lat[i], lon[i], depth[i], region[i], id[i], timestamp[i]);
            }
            return data;
        }

        private static void populateArraysOne() {
            //year
            string yearRaw = Properties.Resources.Year_1;
            string[] yearLines = yearRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            year = new int[yearLines.Length];
            for (int i = 0; i < yearLines.Length; i++) {
                year[i] = Convert.ToInt32(yearLines[i]);
            }

            //month
            string monthRaw = Properties.Resources.Month_1;
            string[] monthLines = monthRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            month = monthLines;

            //day
            string dayRaw = Properties.Resources.Day_1;
            string[] dayLines = dayRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            day = new int[dayLines.Length];
            for (int i = 0; i < dayLines.Length; i++) {
                day[i] = Convert.ToInt32(dayLines[i]);
            }

            //time
            string timeRaw = Properties.Resources.Time_1;
            string[] timeLines = timeRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            time = timeLines;

            //magnitude
            string magRaw = Properties.Resources.Magnitude_1;
            string[] magLines = magRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            mag = new decimal[magLines.Length];
            for (int i = 0; i < magLines.Length; i++) {
                mag[i] = Convert.ToDecimal(magLines[i]);
            }

            //latitude
            string latRaw = Properties.Resources.Latitude_1;
            string[] latLines = latRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lat = new decimal[latLines.Length];
            for (int i = 0; i < latLines.Length; i++) {
                lat[i] = Convert.ToDecimal(latLines[i]);
            }

            //longitude
            string lonRaw = Properties.Resources.Longitude_1;
            string[] lonLines = lonRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lon = new decimal[lonLines.Length];
            for (int i = 0; i < lonLines.Length; i++) {
                lon[i] = Convert.ToDecimal(lonLines[i]);
            }

            //depth
            string depthRaw = Properties.Resources.Depth_1;
            string[] depthLines = depthRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            depth = new decimal[depthLines.Length];
            for (int i = 0; i < depthLines.Length; i++) {
                depth[i] = Convert.ToDecimal(depthLines[i]);
            }

            //region
            string regionRaw = Properties.Resources.Region_1;
            string[] regionLines = regionRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            region = regionLines;

            //id
            string idRaw = Properties.Resources.IRIS_ID_1;
            string[] idLines = idRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            id = idLines;

            //timestamp
            string timestampRaw = Properties.Resources.Timestamp_1;
            string[] timestampLines = timestampRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            timestamp = new long[timestampLines.Length];
            for (int i = 0; i < timestampLines.Length; i++) {
                timestamp[i] = Convert.ToInt64(timestampLines[i]);
            }
        }
    }
}
