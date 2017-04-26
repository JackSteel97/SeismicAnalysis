using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SeismicAnalysis {

    internal class Program {

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

        //import for maximising the console window from: http://stackoverflow.com/questions/22053112/maximizing-console-window-c-sharp
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private static void Main(string[] args) {
            //maximise the window
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
            Console.SetBufferSize(9001, 9001);

            int datasetSelection = chooseDataset();
            Console.Clear();

            SeismicRecord[] data = loadDataset(datasetSelection);

            int actionSelection = actionChoice();
            Console.Clear();
            string dataSelection = "";
            switch (actionSelection) {
                case 1:
                    //sort
                    dataSelection = selectDataToAnalyse("sort with respect to");
                    outputCurrentState(sortSelectedData(dataSelection, data), dataSelection);
                    break;

                case 2:
                    //search
                    int searchSelection = searchChoice();
                    handleSearch(searchSelection, data);
                    break;

                case 3:
                    //min
                    dataSelection = selectDataToAnalyse("find a minimum of");
                    Console.Clear();
                    Console.WriteLine("Minimum value found: \n");
                    outputCurrentState(Searching.findMinimumValue(data, data[0].GetType().GetProperty(dataSelection)));
                    break;

                case 4:
                    //min
                    dataSelection = selectDataToAnalyse("find a maximum of");
                    Console.Clear();
                    Console.WriteLine("Maximum value found: \n");
                    outputCurrentState(Searching.findMaximumValue(data, data[0].GetType().GetProperty(dataSelection)));
                    break;

                default:
                    //sort
                    dataSelection = selectDataToAnalyse("sort with respect to");
                    sortSelectedData(dataSelection, data);
                    break;
            }

            Console.Read();
        }

        private static void handleSearch(int selection, SeismicRecord[] data) {
            switch (selection) {
                case 1:
                    //one date
                    SeismicRecord result = findADate(data);
                    if (result != null) {
                        outputCurrentState(result);
                    } else {
                        Console.WriteLine("No record with this date exists.");
                    }
                    break;

                case 2:
                    //search by month
                    SeismicRecord[] result2 = findAMonth(data);
                    if (result2 != null) {
                        outputCurrentState(result2);
                    } else {
                        Console.WriteLine("No records exist for this month.");
                    }
                    break;

                case 3:
                    //custom search
                    string dataSelection = selectDataToAnalyse("search through");
                    int searchType = searchTypeChoice();
                    switch (searchType) {
                        case 1:
                            //linear for one
                            SeismicRecord result3 = linearSearchForOne(data, dataSelection);
                            if (result3 != null) {
                                outputCurrentState(result3);
                            } else {
                                Console.WriteLine("No result found.");
                            }
                            break;

                        case 2:
                            //binary search
                            SeismicRecord result4 = binarySearchForOne(data, dataSelection);
                            if (result4 != null) {
                                outputCurrentState(result4);
                            } else {
                                Console.WriteLine("No result found.");
                            }
                            break;

                        case 3:
                            //linear for multiple
                            SeismicRecord[] result5 = linearSearchForMultiple(data, dataSelection);
                            if (result5 != null) {
                                outputCurrentState(result5);
                            } else {
                                Console.WriteLine("No results found.");
                            }
                            break;
                    }
                    break;
            }
        }

        private static SeismicRecord[] linearSearchForMultiple(SeismicRecord[] data, string dataSelection) {
            Console.Write("\nEnter your search term: ");
            string input = Console.ReadLine();
            return Searching.linearSearchForMultiple(data, input, data[0].GetType().GetProperty(dataSelection));
        }

        private static SeismicRecord binarySearchForOne(SeismicRecord[] data, string dataSelection) {
            Console.Write("\nEnter your search term: ");
            string input = Console.ReadLine();
            int steps = 0;
            Sorting.quickSort(ref data, 0, data.Length - 1, data[0].GetType().GetProperty(dataSelection), ref steps);
            int result = Searching.binarySearch(data, input, data[0].GetType().GetProperty(dataSelection));
            if (result < 0) {
                return null;
            } else {
                return data[result];
            }
        }

        private static SeismicRecord linearSearchForOne(SeismicRecord[] data, string dataSelection) {
            Console.Write("\nEnter your search term: ");
            string input = Console.ReadLine();
            return Searching.linearSearchForOne(data, input, data[0].GetType().GetProperty(dataSelection));
        }

        private static int searchTypeChoice() {
            Console.WriteLine("How do you want to search this data?");
            Console.WriteLine("\t1:\tLinear search for one record\n\t2:\tBinary search for one record\n\t3:\tLinear search for multiple records");
            return getUserInput(1, 3);
        }

        private static SeismicRecord[] findAMonth(SeismicRecord[] data) {
            Console.WriteLine("Enter a month name in full, or a number between 1 and 12: ");
            string input = Console.ReadLine();
            int month = getMonthNumber(input);
            return Searching.linearSearchForMultiple(data, month, data[0].GetType().GetProperty("Month"));
        }

        private static int getMonthNumber(string input) {
            int num = 0;
            do {
                if (int.TryParse(input, out num)) {
                    //number
                    if (num <= 12 && num >= 1) {
                        return num;
                    }
                } else {
                    //string
                    if (input.Trim().ToLower().Contains("jan")) {
                        return 1;
                    } else if (input.Trim().ToLower().Contains("feb")) {
                        return 2;
                    } else if (input.Trim().ToLower().Contains("mar")) {
                        return 3;
                    } else if (input.Trim().ToLower().Contains("apr")) {
                        return 4;
                    } else if (input.Trim().ToLower().Contains("may")) {
                        return 5;
                    } else if (input.Trim().ToLower().Contains("jun")) {
                        return 6;
                    } else if (input.Trim().ToLower().Contains("jul")) {
                        return 7;
                    } else if (input.Trim().ToLower().Contains("aug")) {
                        return 8;
                    } else if (input.Trim().ToLower().Contains("sep")) {
                        return 9;
                    } else if (input.Trim().ToLower().Contains("oct")) {
                        return 10;
                    } else if (input.Trim().ToLower().Contains("nov")) {
                        return 11;
                    } else if (input.Trim().ToLower().Contains("dec")) {
                        return 12;
                    }
                }
                Console.WriteLine("invalid input, please try again.");
                input = Console.ReadLine();
            } while (true);
        }

        private static SeismicRecord findADate(SeismicRecord[] data) {
            Console.Write("Enter a date (dd/mm/yyyy): ");
            string input = Console.ReadLine();
            DateTime target = new DateTime();
            if (DateTime.TryParse(input, out target)) {
                int steps = 0;
                Sorting.quickSort(ref data, 0, data.Length - 1, data[0].GetType().GetProperty("Date"), ref steps);
                int result = Searching.binarySearch(data, target, data[0].GetType().GetProperty("Date"));
                if (result != -1) {
                    return data[result];
                }
            } else {
                Console.WriteLine("Invalid date format.");
            }
            return null;
        }

        private static int searchChoice() {
            Console.WriteLine("How do you want to search?");
            Console.WriteLine("\t1:\tFind a specific date\n\t2:\tSearch by month\n\t3:\tCustom search");
            return getUserInput(1, 3);
        }

        private static int actionChoice() {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("\t1:\tSort the dataset\n\t2:\tSearch the dataset\n\t3:\tFind minimum values\n\t4:\tFind maximum values");
            return getUserInput(1, 4);
        }

        private static SeismicRecord[] loadDataset(int selection) {
            switch (selection) {
                case 2:
                    //dataset 2
                    populateArraysTwo();
                    return fillRecordArray();

                case 3:
                    //task 9
                    //both datasets
                    populateArraysOne();
                    SeismicRecord[] first = fillRecordArray();
                    populateArraysTwo();
                    SeismicRecord[] second = fillRecordArray();
                    SeismicRecord[] output = new SeismicRecord[first.Length + second.Length];
                    first.CopyTo(output, 0);
                    first.CopyTo(output, first.Length);
                    return output;

                default:
                    //dataset 1
                    populateArraysOne();
                    return fillRecordArray();
            }
        }

        private static int chooseDataset() {
            Console.WriteLine("Please choose which data set you wish to analyse: ");
            Console.WriteLine("\t1:\tRegion one\n\t2:\tRegion two\n\t3:\tBoth regions");
            return getUserInput(1, 3);
        }

        //task 1
        private static string selectDataToAnalyse(string dataAction = "analyse") {
            //output options
            Console.WriteLine("Select whih data you want to {0}: ", dataAction);
            Console.WriteLine("\t1:\tYear\n\t2:\tMonth\n\t3:\tDay\n\t4:\tTime\n\t5:\tMagnitude\n\t6:\tLatitude\n\t7:\tLongitude\n\t8:\tDepth\n\t9:\tRegion\n\t10:\tIRIS ID\n\t11:\tTimestamp\n");
            int selection = getUserInput(1, 11);
            switch (selection) {
                case 1:
                    return "Year";

                case 2:
                    return "Month";

                case 3:
                    return "Day";

                case 4:
                    return "Time";

                case 5:
                    return "Magnitude";

                case 6:
                    return "Lat";

                case 7:
                    return "Lon";

                case 8:
                    return "Depth";

                case 9:
                    return "Region";

                case 10:
                    return "ID";

                case 11:
                    return "Timestamp";

                default:
                    return "Year";
            }
        }

        //task 2
        private static SeismicRecord[] sortSelectedData(string dataSelected, SeismicRecord[] data) {
            Console.WriteLine("How do you want to sort this data?");
            Console.WriteLine("\t1:\tAscending order\n\t2:\tDescending order\n");
            int selection = getUserInput(1, 2);
            switch (selection) {
                case 2:
                    SeismicRecord[] temp = sortDataAscending(dataSelected, data);
                    //reverse the ascending order array to get a decending order one
                    SeismicRecord[] output = new SeismicRecord[temp.Length];
                    int count = 0;
                    for (int i = temp.Length - 1; i >= 0; i--) {
                        output[count] = (SeismicRecord)temp[i].Clone();
                        count++;
                    }
                    return output;

                default:
                    return sortDataAscending(dataSelected, data);
            }
        }

        private static SeismicRecord[] sortDataAscending(string selector, SeismicRecord[] data) {
            PropertyInfo p = data[0].GetType().GetProperty(selector);
            int steps = 0;
            Sorting.quickSort(ref data, 0, data.Length - 1, p, ref steps);
            Console.WriteLine("Sorted in {0} steps using quick sort", steps);
            return data;
        }

        //task 7 and 5
        private static void outputCurrentState(SeismicRecord[] data) {
            //headers
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", "YEAR", "MONTH", "DAY", "TIME", "MAGNITUDE", "LATITUDE", "LONGITUDE", "DEPTH (km)", "REGION", "IRIS ID", "TIMESTAMP");
            //data
            foreach (SeismicRecord s in data) {
                Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", s.Year, s.getMonth(), s.getDay(), s.Time, s.Magnitude, s.Lat, s.Lon, s.Depth, s.Region, s.ID, s.Timestamp);
            }
        }

        private static void outputCurrentState(SeismicRecord[] data, string highlightCol) {
            Console.WriteLine("\nThe sorted column is highlighted: \n");
            //headers
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", "YEAR", "MONTH", "DAY", "TIME", "MAGNITUDE", "LATITUDE", "LONGITUDE", "DEPTH (km)", "REGION", "IRIS ID", "TIMESTAMP");
            //data
            foreach (SeismicRecord s in data) {
                if (highlightCol == "Year") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Year);
                Console.ResetColor();
                if (highlightCol == "Month") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.getMonth());
                Console.ResetColor();
                if (highlightCol == "Day") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.getDay());
                Console.ResetColor();
                if (highlightCol == "Time") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Time);
                Console.ResetColor();
                if (highlightCol == "Magnitude") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Magnitude);
                Console.ResetColor();
                if (highlightCol == "Lat") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Lat);
                Console.ResetColor();
                if (highlightCol == "Lon") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Lon);
                Console.ResetColor();
                if (highlightCol == "Depth") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Depth);
                Console.ResetColor();
                if (highlightCol == "Region") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Region);
                Console.ResetColor();
                if (highlightCol == "ID") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.ID);
                Console.ResetColor();
                if (highlightCol == "Timestamp") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\n", s.Timestamp);
                Console.ResetColor();

                //Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", s.Year, s.getMonth(), s.getDay(), s.Time, s.Magnitude, s.Lat, s.Lon, s.Depth, s.Region, s.ID, s.Timestamp);
            }
        }

        private static void outputCurrentState(SeismicRecord record) {
            //headers
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", "YEAR", "MONTH", "DAY", "TIME", "MAGNITUDE", "LATITUDE", "LONGITUDE", "DEPTH (km)", "REGION", "IRIS ID", "TIMESTAMP");
            //data
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", record.Year, record.getMonth(), record.getDay(), record.Time, record.Magnitude, record.Lat, record.Lon, record.Depth, record.Region, record.ID, record.Timestamp);
        }

        private static int getUserInput(int lowerBound, int upperBound) {
            bool valid = false;
            int val = 0;
            do {
                string input = Console.ReadLine();
                if (int.TryParse(input, out val)) {
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
            for (int i = 0; i < year.Length; i++) {
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

        private static void populateArraysTwo() {
            //year
            string yearRaw = Properties.Resources.Year_2;
            string[] yearLines = yearRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            year = new int[yearLines.Length];
            for (int i = 0; i < yearLines.Length; i++) {
                year[i] = Convert.ToInt32(yearLines[i]);
            }

            //month
            string monthRaw = Properties.Resources.Month_2;
            string[] monthLines = monthRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            month = monthLines;

            //day
            string dayRaw = Properties.Resources.Day_2;
            string[] dayLines = dayRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            day = new int[dayLines.Length];
            for (int i = 0; i < dayLines.Length; i++) {
                day[i] = Convert.ToInt32(dayLines[i]);
            }

            //time
            string timeRaw = Properties.Resources.Time_2;
            string[] timeLines = timeRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            time = timeLines;

            //magnitude
            string magRaw = Properties.Resources.Magnitude_2;
            string[] magLines = magRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            mag = new decimal[magLines.Length];
            for (int i = 0; i < magLines.Length; i++) {
                mag[i] = Convert.ToDecimal(magLines[i]);
            }

            //latitude
            string latRaw = Properties.Resources.Latitude_2;
            string[] latLines = latRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lat = new decimal[latLines.Length];
            for (int i = 0; i < latLines.Length; i++) {
                lat[i] = Convert.ToDecimal(latLines[i]);
            }

            //longitude
            string lonRaw = Properties.Resources.Longitude_2;
            string[] lonLines = lonRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lon = new decimal[lonLines.Length];
            for (int i = 0; i < lonLines.Length; i++) {
                lon[i] = Convert.ToDecimal(lonLines[i]);
            }

            //depth
            string depthRaw = Properties.Resources.Depth_2;
            string[] depthLines = depthRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            depth = new decimal[depthLines.Length];
            for (int i = 0; i < depthLines.Length; i++) {
                depth[i] = Convert.ToDecimal(depthLines[i]);
            }

            //region
            string regionRaw = Properties.Resources.Region_2;
            string[] regionLines = regionRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            region = regionLines;

            //id
            string idRaw = Properties.Resources.IRIS_ID_2;
            string[] idLines = idRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            id = idLines;

            //timestamp
            string timestampRaw = Properties.Resources.Timestamp_2;
            string[] timestampLines = timestampRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            timestamp = new long[timestampLines.Length];
            for (int i = 0; i < timestampLines.Length; i++) {
                timestamp[i] = Convert.ToInt64(timestampLines[i]);
            }
        }
    }
}