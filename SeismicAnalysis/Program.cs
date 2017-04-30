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
        public static extern bool ShowWindow (System.IntPtr hWnd, int cmdShow);

        /// <summary>
        /// Entry point for application
        /// </summary>
        /// <param name="args">Parameter array for CLI arguments</param>
        private static void Main (string[] args) {
            //maximise the window
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
            Console.SetBufferSize(9001, 1920);
            
            //repeat forever so the program doesn't need to be restarted
            while(true) {
                //get which data set the user wants to use
                int datasetSelection = chooseDataset();
                Console.Clear();
                
                //load the selected dataset
                SeismicRecord[] data = loadDataset(datasetSelection);
                AlgorithmEvaluation.runEvaluation(data);
                //get what the user wants to do with the data
                int actionSelection = actionChoice();
                Console.Clear();

                string dataSelection = "";
                //based on what they want to do, call appropriate methods
                switch(actionSelection) {
                    case 1:
                        //sort
                        //get the property the user wants to sort by
                        dataSelection = selectDataToAnalyse("sort with respect to");
                        //sort and output the data
                        outputCurrentState(sortSelectedData(dataSelection, data), dataSelection);
                        break;

                    case 2:
                        //search
                        //get how the user wants to search
                        int searchSelection = searchChoice();
                        handleSearch(searchSelection, data);
                        break;

                    case 3:
                        //task 6
                        //min
                        dataSelection = selectDataToAnalyse("find a minimum of");
                        Console.Clear();
                        //find and output the minimum value from their selected data column
                        Console.WriteLine("Minimum value found: \n");
                        outputCurrentState(Searching.findMinimumValue(data, data[0].GetType().GetProperty(dataSelection)));
                        break;

                    case 4:
                        //task 6
                        //max
                        dataSelection = selectDataToAnalyse("find a maximum of");
                        Console.Clear();
                        Console.WriteLine("Maximum value found: \n");
                        //find and output the maximum value from their selected data column
                        outputCurrentState(Searching.findMaximumValue(data, data[0].GetType().GetProperty(dataSelection)));
                        break;

                    default:
                        //sort
                        dataSelection = selectDataToAnalyse("sort with respect to");
                        sortSelectedData(dataSelection, data);
                        break;
                }
                //give them a chance to read results before restarting
                Console.WriteLine("\n\nPress enter to start again...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        /// <summary>
        /// Entry point for searching
        /// </summary>
        /// <param name="selection">How the user wants to search</param>
        /// <param name="data">data to search</param>
        private static void handleSearch (int selection, SeismicRecord[] data) {
            switch(selection) {
                case 1:
                    //task 3
                    //search for one date
                    SeismicRecord result = findADate(data);
                    if(result != null) {
                        //output the result
                        outputCurrentState(result);
                    } else {
                        Console.WriteLine("No record with this date exists.");
                    }
                    break;

                case 2:
                    //task 4
                    //search by month
                    SeismicRecord[] result2 = findAMonth(data);
                    if(result2 != null) {
                        //output the result
                        outputCurrentState(result2);
                    } else {
                        Console.WriteLine("No records exist for this month.");
                    }
                    break;

                case 3:
                    //custom search
                    //get data column to search through
                    string dataSelection = selectDataToAnalyse("search through");
                    Console.Clear();
                    //get how the user wants to search
                    int searchType = searchTypeChoice();
                    switch(searchType) {
                        case 1:
                            //linear for one
                            SeismicRecord result3 = linearSearchForOne(data, dataSelection);
                            if(result3 != null) {
                                //output result
                                outputCurrentState(result3);
                            } else {
                                Console.WriteLine("No result found.");
                            }
                            break;

                        case 2:
                            //binary search
                            SeismicRecord result4 = binarySearchForOne(data, dataSelection);
                            if(result4 != null) {
                                //output result
                                outputCurrentState(result4);
                            } else {
                                Console.WriteLine("No result found.");
                            }
                            break;

                        case 3:
                            //linear for multiple
                            SeismicRecord[] result5 = linearSearchForMultiple(data, dataSelection);
                            if(result5 != null) {
                                //output result
                                outputCurrentState(result5);
                            } else {
                                Console.WriteLine("No results found.");
                            }
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Perform a linear search for ALL matching values
        /// </summary>
        /// <param name="data">Data to search through</param>
        /// <param name="dataSelection">data column to search for a match</param>
        /// <returns>An array of record containing matches</returns>
        private static SeismicRecord[] linearSearchForMultiple (SeismicRecord[] data, string dataSelection) {
            //get search term
            Console.Write("\nEnter your search term: ");
            string input = Console.ReadLine();

            return Searching.linearSearchForMultiple(data, input, data[0].GetType().GetProperty(dataSelection));
        }

        /// <summary>
        /// Perform a binary search
        /// </summary>
        /// <param name="data">data to search through</param>
        /// <param name="dataSelection">data column to search in</param>
        /// <returns>A matching record or null if a match cannot be found</returns>
        private static SeismicRecord binarySearchForOne (SeismicRecord[] data, string dataSelection) {
            Console.Write("\nEnter your search term: ");
            string input = Console.ReadLine();
            int steps = 0;
            //timing measurement
            DateTime start = DateTime.Now;
            //sort the data first
            Sorting.quickSort(ref data, 0, data.Length - 1, data[0].GetType().GetProperty(dataSelection), ref steps);
            //search
            int result = Searching.binarySearch(data, input, data[0].GetType().GetProperty(dataSelection), ref steps);
            //timing measurment
            DateTime end = DateTime.Now;
            Console.WriteLine("\nSorted and searched {0} elements in {1} steps using binary search\nTime elapsed: {2}ms", data.Length, steps, (end - start).TotalMilliseconds);
            if(result < 0) {
                return null;
            } else {
                return data[result];
            }
        }

        /// <summary>
        /// Perform a linear search
        /// </summary>
        /// <param name="data">data to search through</param>
        /// <param name="dataSelection">data column to search</param>
        /// <returns>A matching record or null if a match cannot be found</returns>
        private static SeismicRecord linearSearchForOne (SeismicRecord[] data, string dataSelection) {
            Console.Write("\nEnter your search term: ");
            string input = Console.ReadLine();
            int steps = 0;
            //timing measurment
            DateTime start = DateTime.Now;
            //search
            SeismicRecord result = Searching.linearSearchForOne(data, input, data[0].GetType().GetProperty(dataSelection), ref steps);
            //timing measurement
            DateTime end = DateTime.Now;
            Console.WriteLine("\nFound in {0} steps using linear search\nTime elapsed: {1}ms", steps, (end - start).TotalMilliseconds);
            return result;
        }

        /// <summary>
        /// Ask the user what search method they want to use
        /// </summary>
        /// <returns>A value 1-3 corresponding to a search method</returns>
        private static int searchTypeChoice () {
            Console.WriteLine("How do you want to search this data?");
            Console.WriteLine("\t1:\tLinear search for one record\n\t2:\tBinary search for one record\n\t3:\tLinear search for multiple records");
            return getUserInput(1, 3);
        }

        /// <summary>
        /// Find all records for a given month
        /// </summary>
        /// <param name="data">Data to search through</param>
        /// <returns>An array of records with a matching month, or null if none exist</returns>
        private static SeismicRecord[] findAMonth (SeismicRecord[] data) {
            Console.WriteLine("Enter a month name in full, or a number between 1 and 12: ");
            string input = Console.ReadLine();
            //get a month number from user input
            int month = getMonthNumber(input);
            return Searching.linearSearchForMultiple(data, month, data[0].GetType().GetProperty("Month"));
        }

        /// <summary>
        /// Get and validate an month number from user input
        /// </summary>
        /// <param name="input">First input</param>
        /// <returns></returns>
        private static int getMonthNumber (string input) {
            int num = 0;
            //repeat until something returns a valid output
            do {
                //is it an integer?
                if(int.TryParse(input, out num)) {
                    //yes, is it between 1 and 12
                    if(num <= 12 && num >= 1) {
                        //yes, valid, return it
                        return num;
                    }
                } else {
                    //no, its a string
                    //determine if it is a month string
                    if(input.Trim().ToLower().Contains("jan")) {
                        return 1;
                    } else if(input.Trim().ToLower().Contains("feb")) {
                        return 2;
                    } else if(input.Trim().ToLower().Contains("mar")) {
                        return 3;
                    } else if(input.Trim().ToLower().Contains("apr")) {
                        return 4;
                    } else if(input.Trim().ToLower().Contains("may")) {
                        return 5;
                    } else if(input.Trim().ToLower().Contains("jun")) {
                        return 6;
                    } else if(input.Trim().ToLower().Contains("jul")) {
                        return 7;
                    } else if(input.Trim().ToLower().Contains("aug")) {
                        return 8;
                    } else if(input.Trim().ToLower().Contains("sep")) {
                        return 9;
                    } else if(input.Trim().ToLower().Contains("oct")) {
                        return 10;
                    } else if(input.Trim().ToLower().Contains("nov")) {
                        return 11;
                    } else if(input.Trim().ToLower().Contains("dec")) {
                        return 12;
                    }
                }
                //invalid, get another input
                Console.WriteLine("invalid input, please try again.");
                input = Console.ReadLine();
            } while(true);
        }

        /// <summary>
        /// Find an entry with a specific date
        /// </summary>
        /// <param name="data">data to search through</param>
        /// <returns>A single entry or null if no match can be found</returns>
        private static SeismicRecord findADate (SeismicRecord[] data) {
            string input = "";
            while(true) {
                //ask for a date
                Console.Write("Enter a date (dd/mm/yyyy): ");
                input = Console.ReadLine();
                //validate
                DateTime target = new DateTime();
                if(DateTime.TryParse(input, out target)) {
                    //search
                    return binarySearchForOne(data, "Date");
                } else {
                    //invalid, ask again
                    Console.WriteLine("Invalid date format. Try again");
                    input = Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Ask for user choice on what they want to search for
        /// </summary>
        /// <returns>a number 1-3 corresponding to a search option</returns>
        private static int searchChoice () {
            Console.WriteLine("How do you want to search?");
            Console.WriteLine("\t1:\tFind a specific date\n\t2:\tSearch by month\n\t3:\tCustom search");
            return getUserInput(1, 3);
        }

        /// <summary>
        /// Ask for user choice on what they want to do with the dataset
        /// </summary>
        /// <returns>A number 1-4 corresponding to a action option</returns>
        private static int actionChoice () {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("\t1:\tSort the dataset\n\t2:\tSearch the dataset\n\t3:\tFind minimum values\n\t4:\tFind maximum values");
            return getUserInput(1, 4);
        }

        //task 8 and task 9
        /// <summary>
        /// Load the selected dataset
        /// </summary>
        /// <param name="selection">Number 1-3 corresponding to a dataset choice</param>
        /// <returns>The loaded dataset</returns>
        private static SeismicRecord[] loadDataset (int selection) {
            switch(selection) {
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
                    //concatenate the datasets into a single array
                    SeismicRecord[] output = new SeismicRecord[first.Length + second.Length];
                    first.CopyTo(output, 0);
                    second.CopyTo(output, first.Length);
                    return output;

                default:
                    //dataset 1
                    populateArraysOne();
                    return fillRecordArray();
            }
        }

        
        /// <summary>
        /// Ask user to choose a dataset
        /// </summary>
        /// <returns>A number 1-3 corresponding to a dataset choice</returns>
        private static int chooseDataset () {
            Console.WriteLine("Please choose which data set you wish to analyse: ");
            Console.WriteLine("\t1:\tRegion one\n\t2:\tRegion two\n\t3:\tBoth regions");
            return getUserInput(1, 3);
        }

        //task 1
        /// <summary>
        /// Ask user to choose which property they want to use
        /// </summary>
        /// <param name="dataAction">verb phrase</param>
        /// <returns>A number 1-11 corresponding to a property choice</returns>
        private static string selectDataToAnalyse (string dataAction = "analyse") {
            //output options
            Console.WriteLine("Select whih data you want to {0}: ", dataAction);
            Console.WriteLine("\t1:\tYear\n\t2:\tMonth\n\t3:\tDay\n\t4:\tTime\n\t5:\tMagnitude\n\t6:\tLatitude\n\t7:\tLongitude\n\t8:\tDepth\n\t9:\tRegion\n\t10:\tIRIS ID\n\t11:\tTimestamp\n");
            int selection = getUserInput(1, 11);
            switch(selection) {
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
        /// <summary>
        /// Start the sorting process
        /// Ask user to choose an algorithm and order
        /// </summary>
        /// <param name="dataSelected">data column to sort by</param>
        /// <param name="data">data to sort</param>
        /// <returns>Sorted data array</returns>
        private static SeismicRecord[] sortSelectedData (string dataSelected, SeismicRecord[] data) {
            Console.Clear();
            //get user choice of algorithm
            Console.WriteLine("Which sorting algorithm do you want to use?");
            Console.WriteLine("\t1:\tQuick Sort\n\t2:\tMerge Sort\n\t3:\tInsertion Sort\n\t4:\tHeap Sort\n\t5:\tBubble Sort\n");
            int algoSelection = getUserInput(1, 5);

            //get user choice of order
            Console.WriteLine("\tHow do you want to sort this data?");
            Console.WriteLine("\t\t1:\tAscending order\n\t\t2:\tDescending order\n");
            int selection = getUserInput(1, 2);

            //sort with chosen algorithm
            SeismicRecord[] sortedData = new SeismicRecord[data.Length];
            switch(algoSelection) {
                case 1:
                    sortedData = quickSortData(data, dataSelected);
                    break;

                case 2:
                    sortedData = mergeSortData(data, dataSelected);
                    break;

                case 3:
                    sortedData = insertionSortData(data, dataSelected);
                    break;

                case 4:
                    sortedData = heapSortData(data, dataSelected);
                    break;

                case 5:
                    sortedData = bubbleSortData(data, dataSelected);
                    break;
            }

            //return sorted data, if neccessary reverse order first
            switch(selection) {
                case 2:
                    //reverse the ascending order array to get a decending order one
                    SeismicRecord[] output = new SeismicRecord[sortedData.Length];
                    int count = 0;
                    for(int i = sortedData.Length - 1; i >= 0; i--) {
                        output[count] = (SeismicRecord)sortedData[i].Clone();
                        count++;
                    }
                    return output;

                default:
                    return sortedData;
            }
        }

        /// <summary>
        /// Perform a merge sort
        /// </summary>
        /// <param name="data">data to sort</param>
        /// <param name="selector">data column to sort by</param>
        /// <returns>Sorted data array</returns>
        private static SeismicRecord[] mergeSortData (SeismicRecord[] data, string selector) {
            PropertyInfo p = data[0].GetType().GetProperty(selector);
            int steps = 0;
            //timing measurement
            DateTime start = DateTime.Now;
            //sort
            data = Sorting.mergeSort(data, p, ref steps);
            //timing measurement
            DateTime end = DateTime.Now;

            Console.WriteLine("\n\t\t\tSorted {0:n0} elements in {1:n0} steps using merge sort\n\t\t\tTime elapsed: {2:n2}ms", data.Length, steps, (end - start).TotalMilliseconds);
            return data;
        }

        /// <summary>
        /// Perform a quick sort
        /// </summary>
        /// <param name="data">data to sort</param>
        /// <param name="selector">data column to sort by</param>
        /// <returns>Sorted data array</returns>
        private static SeismicRecord[] quickSortData (SeismicRecord[] data, string selector) {
            PropertyInfo p = data[0].GetType().GetProperty(selector);
            int steps = 0;
            //timing measurment
            DateTime start = DateTime.Now;
            //sort
            Sorting.quickSort(ref data, 0, data.Length - 1, p, ref steps);
            //timing measurment
            DateTime end = DateTime.Now;

            Console.WriteLine("\n\t\t\tSorted {0:n0} elements in {1:n0} steps using quick sort\n\t\t\tTime elapsed: {2:n2}ms", data.Length, steps, (end - start).TotalMilliseconds);
            return data;
        }

        /// <summary>
        /// Perform an insertion sort
        /// </summary>
        /// <param name="data">data to sort</param>
        /// <param name="selector">data column to sort by</param>
        /// <returns>Sorted data array</returns>
        private static SeismicRecord[] insertionSortData (SeismicRecord[] data, string selector) {
            PropertyInfo p = data[0].GetType().GetProperty(selector);
            int steps = 0;
            //timing measurment
            DateTime start = DateTime.Now;
            //sort
            data = Sorting.insertionSort(data, p, ref steps);
            //timing measurment
            DateTime end = DateTime.Now;

            Console.WriteLine("\n\t\t\tSorted {0:n0} elements in {1:n0} steps using insertion sort\n\t\t\tTime elapsed: {2:n2}ms", data.Length, steps, (end - start).TotalMilliseconds);
            return data;
        }

        /// <summary>
        /// Perform a heap sort
        /// </summary>
        /// <param name="data">data to sort</param>
        /// <param name="selector">data column to sort by</param>
        /// <returns>Sorted data array</returns>
        private static SeismicRecord[] heapSortData (SeismicRecord[] data, string selector) {
            PropertyInfo p = data[0].GetType().GetProperty(selector);
            int steps = 0;

            //timing measurment
            DateTime start = DateTime.Now;
            //sort
            Sorting.heapSort(ref data, p, ref steps);
            //timing measurment
            DateTime end = DateTime.Now;

            Console.WriteLine("\n\t\t\tSorted {0:n0} elements in {1:n0} steps using heap sort\n\t\t\tTime elapsed: {2:n2}ms", data.Length, steps, (end - start).TotalMilliseconds);
            return data;
        }

        /// <summary>
        /// Perform a bubble sort
        /// </summary>
        /// <param name="data">data to sort</param>
        /// <param name="selector">data column to sort by</param>
        /// <returns>Sorted data array</returns>
        private static SeismicRecord[] bubbleSortData (SeismicRecord[] data, string selector) {
            PropertyInfo p = data[0].GetType().GetProperty(selector);
            int steps = 0;
            //timing measurment
            DateTime start = DateTime.Now;
            //sort
            data = Sorting.bubbleSort(data, p, ref steps);
            //timing measurment
            DateTime end = DateTime.Now;

            Console.WriteLine("\n\t\t\tSorted {0:n0} elements in {1:n0} steps using bubble sort\n\t\t\tTime elapsed: {2:n2}ms", data.Length, steps, (end - start).TotalMilliseconds);
            return data;
        }

        //task 7 and task 5
        /// <summary>
        /// Output a dataset
        /// </summary>
        /// <param name="data">Data to output</param>
        private static void outputCurrentState (SeismicRecord[] data) {
            //headers
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", "YEAR", "MONTH", "DAY", "TIME", "MAGNITUDE", "LATITUDE", "LONGITUDE", "DEPTH (km)", "REGION", "IRIS ID", "TIMESTAMP");
            //data
            foreach(SeismicRecord s in data) {
                Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", s.Year, s.getMonth(), s.getDay(), s.Time, s.Magnitude, s.Lat, s.Lon, s.Depth, s.Region, s.ID, s.Timestamp);
            }
        }

        /// <summary>
        /// output a dataset
        /// </summary>
        /// <param name="data">data to output</param>
        /// <param name="highlightCol">data column to highlight</param>
        private static void outputCurrentState (SeismicRecord[] data, string highlightCol) {
            Console.WriteLine("\nThe sorted column is highlighted: \n");
            //headers
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", "YEAR", "MONTH", "DAY", "TIME", "MAGNITUDE", "LATITUDE", "LONGITUDE", "DEPTH (km)", "REGION", "IRIS ID", "TIMESTAMP");
            //data
            foreach(SeismicRecord s in data) {
                if(highlightCol == "Year") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Year);
                Console.ResetColor();
                if(highlightCol == "Month") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.getMonth());
                Console.ResetColor();
                if(highlightCol == "Day") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.getDay());
                Console.ResetColor();
                if(highlightCol == "Time") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Time);
                Console.ResetColor();
                if(highlightCol == "Magnitude") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Magnitude);
                Console.ResetColor();
                if(highlightCol == "Lat") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Lat);
                Console.ResetColor();
                if(highlightCol == "Lon") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Lon);
                Console.ResetColor();
                if(highlightCol == "Depth") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Depth);
                Console.ResetColor();
                if(highlightCol == "Region") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.Region);
                Console.ResetColor();
                if(highlightCol == "ID") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\t|", s.ID);
                Console.ResetColor();
                if(highlightCol == "Timestamp") {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("\t{0,-25}\n", s.Timestamp);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Output a single record
        /// </summary>
        /// <param name="record">Record to output</param>
        private static void outputCurrentState (SeismicRecord record) {
            //headers
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", "YEAR", "MONTH", "DAY", "TIME", "MAGNITUDE", "LATITUDE", "LONGITUDE", "DEPTH (km)", "REGION", "IRIS ID", "TIMESTAMP");
            //data
            Console.WriteLine("\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t|\t{3,-25}\t|\t{4,-25}\t|\t{5,-25}\t|\t{6,-25}\t|\t{7,-25}\t|\t{8,-25}\t|\t{9,-25}\t|\t{10,-25}", record.Year, record.getMonth(), record.getDay(), record.Time, record.Magnitude, record.Lat, record.Lon, record.Depth, record.Region, record.ID, record.Timestamp);
        }

        /// <summary>
        /// Get and validate user input of an integer between two numbers
        /// </summary>
        /// <param name="lowerBound">Lower bound integer</param>
        /// <param name="upperBound">Upper bound integer</param>
        /// <returns>The user's chosen integer</returns>
        private static int getUserInput (int lowerBound, int upperBound) {
            bool valid = false;
            int val = 0;
            do {
                string input = Console.ReadLine();
                //is it an integer?
                if(int.TryParse(input, out val)) {
                    //yes, is it between the upper and lower bounds?
                    if(val >= lowerBound && val <= upperBound) {
                        //yes, it is valid
                        valid = true;
                    }
                }
                if(!valid) {
                    Console.WriteLine("Invalid input, please try again...");
                }
            } while(!valid);
            return val;
        }

        /// <summary>
        /// Populate an array from the global data arrays
        /// </summary>
        /// <returns>An array of SeismicRecord objects</returns>
        private static SeismicRecord[] fillRecordArray () {
            SeismicRecord[] data = new SeismicRecord[year.Length];
            for(int i = 0; i < year.Length; i++) {
                data[i] = new SeismicRecord(year[i], month[i], day[i], time[i], mag[i], lat[i], lon[i], depth[i], region[i], id[i], timestamp[i]);
            }
            return data;
        }

        /// <summary>
        /// Populate the arrays from the region one source files
        /// </summary>
        private static void populateArraysOne () {
            //year
            string yearRaw = Properties.Resources.Year_1;
            string[] yearLines = yearRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            year = new int[yearLines.Length];
            for(int i = 0; i < yearLines.Length; i++) {
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
            for(int i = 0; i < dayLines.Length; i++) {
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
            for(int i = 0; i < magLines.Length; i++) {
                mag[i] = Convert.ToDecimal(magLines[i]);
            }

            //latitude
            string latRaw = Properties.Resources.Latitude_1;
            string[] latLines = latRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lat = new decimal[latLines.Length];
            for(int i = 0; i < latLines.Length; i++) {
                lat[i] = Convert.ToDecimal(latLines[i]);
            }

            //longitude
            string lonRaw = Properties.Resources.Longitude_1;
            string[] lonLines = lonRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lon = new decimal[lonLines.Length];
            for(int i = 0; i < lonLines.Length; i++) {
                lon[i] = Convert.ToDecimal(lonLines[i]);
            }

            //depth
            string depthRaw = Properties.Resources.Depth_1;
            string[] depthLines = depthRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            depth = new decimal[depthLines.Length];
            for(int i = 0; i < depthLines.Length; i++) {
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
            for(int i = 0; i < timestampLines.Length; i++) {
                timestamp[i] = Convert.ToInt64(timestampLines[i]);
            }
        }

        /// <summary>
        /// populate the arrays from the region two source files
        /// </summary>
        private static void populateArraysTwo () {
            //year
            string yearRaw = Properties.Resources.Year_2;
            string[] yearLines = yearRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            year = new int[yearLines.Length];
            for(int i = 0; i < yearLines.Length; i++) {
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
            for(int i = 0; i < dayLines.Length; i++) {
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
            for(int i = 0; i < magLines.Length; i++) {
                mag[i] = Convert.ToDecimal(magLines[i]);
            }

            //latitude
            string latRaw = Properties.Resources.Latitude_2;
            string[] latLines = latRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lat = new decimal[latLines.Length];
            for(int i = 0; i < latLines.Length; i++) {
                lat[i] = Convert.ToDecimal(latLines[i]);
            }

            //longitude
            string lonRaw = Properties.Resources.Longitude_2;
            string[] lonLines = lonRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lon = new decimal[lonLines.Length];
            for(int i = 0; i < lonLines.Length; i++) {
                lon[i] = Convert.ToDecimal(lonLines[i]);
            }

            //depth
            string depthRaw = Properties.Resources.Depth_2;
            string[] depthLines = depthRaw.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            depth = new decimal[depthLines.Length];
            for(int i = 0; i < depthLines.Length; i++) {
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
            for(int i = 0; i < timestampLines.Length; i++) {
                timestamp[i] = Convert.ToInt64(timestampLines[i]);
            }
        }
    }
}