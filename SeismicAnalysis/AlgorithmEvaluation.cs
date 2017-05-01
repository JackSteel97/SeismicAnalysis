using System;
using System.Reflection;

namespace SeismicAnalysis {

    internal class AlgorithmEvaluation {

        //results[i][0] = method
        //results[i][1] = average steps
        //results[i][2] = average time taken
        private static string[][] results = new string[5][];
        private static string[][] searchResults = new string[6][];
        public static void runEvaluation (SeismicRecord[] rawData) {
            Console.WriteLine("Dataset size: {0} elements\n", rawData.Length);
            Console.WriteLine("Running Evaluation, please wait this may take some time...");
            
            if(!(rawData.Length > 0)) {
                Console.WriteLine("Data empty!");
                return;
            }
            //initalise jagged array
            for(int i = 0; i < results.GetLength(0); i++) {
                results[i] = new string[3];
               
            }
            //initalise jagged array
            for(int i = 0; i < searchResults.GetLength(0); i++) {
                searchResults[i] = new string[3];

            }

            PropertyInfo[] props = rawData[0].GetType().GetProperties();
            //sorting evaluation
            Console.WriteLine("\tRunning Quick Sort...");
            evaluateQuickSort(props, rawData);
            Console.WriteLine("\tRunning Bubble Sort...");
            evaluateBubbleSort(props, rawData);
            Console.WriteLine("\tRunning Heap Sort...");
            evaluateHeapSort(props, rawData);
            Console.WriteLine("\tRunning Merge Sort...");
            evaluateMergeSort(props, rawData);
            Console.WriteLine("\tRunning Insertion Sort...");
            evaluateInsertionSort(props, rawData);

            //searching evaluation        
            Console.WriteLine("\tRunning Binary Search on unsorted data...");
            evaluateBinarySearchOnUnsorted(props, rawData);
            Console.WriteLine("\tRunning Binary Search on sorted data...");
            evaluateBinarySearchOnSorted(props, rawData);
            Console.WriteLine("\tRunning Linear Search for single values...");
            evaluateLinearSearchForOne(props, rawData);
            Console.WriteLine("\tRunning Linear Search for multiple values...");
            evaluateLinearSearchForMultiple(props, rawData);
            Console.WriteLine("\tRunning Binary Search for multiple values on sorted data...");
            evaluateBinarySearchForMultiple(props, rawData);
            Console.WriteLine("\tRunning Binary Search for multiple values on unsorted data...");
            evaluateBinarySearchForMultipleUnsorted(props, rawData);

            sortResultsBySteps();
            sortSearchResultsBySteps();
            outputResults();
        }

        private static void evaluateBinarySearchForMultipleUnsorted (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);

                //get a value to search for
                Random rnd = new Random();
                int index = rnd.Next(data.Length);

                //start timer
                DateTime start = DateTime.Now;
                //sort data for binary search
                Sorting.heapSort(ref data, p, ref steps);
                //search
                Searching.binarySearchForMultiple(data, p.GetValue(data[index]), p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            searchResults[5][0] = "Binary Search for multiple inc. sorting";
            searchResults[5][1] = Math.Round((double)totalSteps / props.Length).ToString();
            searchResults[5][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateBinarySearchForMultiple (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);

                //get a value to search for
                Random rnd = new Random();
                int index = rnd.Next(data.Length);
                //sort data for binary search
                Sorting.heapSort(ref data, p, ref steps);
                steps = 0;
                //start timer
                DateTime start = DateTime.Now;
                //search
                Searching.binarySearchForMultiple(data, p.GetValue(data[index]), p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            searchResults[4][0] = "Binary Search for multiple on sorted data";
            searchResults[4][1] = Math.Round((double)totalSteps / props.Length).ToString();
            searchResults[4][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateLinearSearchForMultiple (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);

                //get a value to search for
                Random rnd = new Random();
                int index = rnd.Next(data.Length);

                //start timer
                DateTime start = DateTime.Now;
                //search
                Searching.linearSearchForMultiple(data, p.GetValue(data[index]), p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            searchResults[3][0] = "Linear Search for multiple";
            searchResults[3][1] = Math.Round((double)totalSteps / props.Length).ToString();
            searchResults[3][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateLinearSearchForOne (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);

                //get a value to search for
                Random rnd = new Random();
                int index = rnd.Next(data.Length);

                //start timer
                DateTime start = DateTime.Now;                
                //search
                Searching.linearSearchForOne(data, p.GetValue(data[index]), p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            searchResults[2][0] = "Linear Search";
            searchResults[2][1] = Math.Round((double)totalSteps / props.Length).ToString();
            searchResults[2][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateBinarySearchOnSorted (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            decimal totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);

                //get a value to search for
                Random rnd = new Random();
                int index = rnd.Next(data.Length);
                //sort it for binary search
                Sorting.heapSort(ref data, p, ref steps);
                steps = 0;

                //start timer
                DateTime start = DateTime.Now;
                //search
                Searching.binarySearch(data, p.GetValue(data[index]), p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end.Ticks - start.Ticks);
            }
            totalTime /= 10000;
            //add to results
            searchResults[1][0] = "Binary search on sorted data";
            searchResults[1][1] = Math.Round((double)totalSteps / props.Length).ToString();
            searchResults[1][2] = Math.Round((totalTime / props.Length), 2,MidpointRounding.AwayFromZero).ToString();
        }

        private static void evaluateBinarySearchOnUnsorted (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);

                //get a value to search for
                Random rnd = new Random();
                int index = rnd.Next(data.Length);
                
                //start timer
                DateTime start = DateTime.Now;
                //sort it for binary search
                Sorting.heapSort(ref data, p, ref steps);
                //search
                Searching.binarySearch(data, p.GetValue(data[index]), p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            searchResults[0][0] = "Binary search inc. sorting";
            searchResults[0][1] = Math.Round((double)totalSteps / props.Length).ToString();
            searchResults[0][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void outputResults () {
            Console.Clear();
            Console.WriteLine("Results:");
            Console.WriteLine("\tSorting:\n");
            
            //headers
            Console.WriteLine("\t\t{0,-40}\t|\t{1,-40}\t|\t{2,-40}", "METHOD", "AVERAGE TIME ELAPSED(ms)", "AVERAGE STEPS TAKEN");
            //data
            for(int i = 0; i < results.GetLength(0); i++) {
                
                Console.WriteLine("\t\t{0,-40}\t|\t{1,-40}\t|\t{2,-40}", results[i][0], Convert.ToDecimal(results[i][2]), string.Format("{0:n0}",Convert.ToInt32(results[i][1])));
            }
            Console.ResetColor();
           
            Console.WriteLine("\n\n\tSearching:\n");
            Console.WriteLine("\t\t{0,-40}\t|\t{1,-40}\t|\t{2,-40}\t", "METHOD", "AVERAGE TIME ELAPSED(ms)", "AVERAGE STEPS TAKEN");
            //data
            for(int i = 0; i < searchResults.GetLength(0); i++) {
                
                Console.WriteLine("\t\t{0,-40}\t|\t{1,-40}\t|\t{2,-40}", searchResults[i][0], Convert.ToDecimal(searchResults[i][2]), string.Format("{0:n0}",Convert.ToInt32(searchResults[i][1])));
            }
        }

        private static void sortResultsBySteps () {
            //bubble sort because the array is very small and it is easy to code
            bool isSorted = true;
            for(int i = 0; i < results.GetLength(0) - 1; i++) {
                isSorted = true;
                for(int j = 0; j < results.GetLength(0) - 1 - i; j++) {
                    int a = Convert.ToInt32(results[j + 1][1]);
                    int b = Convert.ToInt32(results[j][1]);
                    if(a < b) {
                        //swap
                        string[] temp = results[j];
                        results[j] = results[j + 1];
                        results[j + 1] = temp;
                        isSorted = false;
                    }
                }
                if(isSorted) {
                    break;
                }
            }
        }

        private static void sortSearchResultsBySteps () {
            //bubble sort because the array is very small and it is easy to code
            bool isSorted = true;
            for(int i = 0; i < searchResults.GetLength(0) - 1; i++) {
                isSorted = true;
                for(int j = 0; j < searchResults.GetLength(0) - 1 - i; j++) {
                    int a = Convert.ToInt32(searchResults[j + 1][1]);
                    int b = Convert.ToInt32(searchResults[j][1]);
                    if(a < b) {
                        //swap
                        string[] temp = searchResults[j];
                        searchResults[j] = searchResults[j + 1];
                        searchResults[j + 1] = temp;
                        isSorted = false;
                    }
                }
                if(isSorted) {
                    break;
                }
            }
        }

        private static void evaluateHeapSort (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);
                //start timer
                DateTime start = DateTime.Now;
                //sort
                Sorting.heapSort(ref data, p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            results[4][0] = "Heap Sort";
            results[4][1] = Math.Round((double)totalSteps / props.Length).ToString();
            results[4][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateMergeSort (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);
                //start timer
                DateTime start = DateTime.Now;
                //sort
                Sorting.mergeSort(data, p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            results[3][0] = "Merge Sort";
            results[3][1] = Math.Round((double)totalSteps / props.Length).ToString();
            results[3][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateInsertionSort (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);
                //start timer
                DateTime start = DateTime.Now;
                //sort
                Sorting.insertionSort(data, p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            results[2][0] = "Insertion Sort";
            results[2][1] = Math.Round((double)totalSteps / props.Length).ToString();
            results[2][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateBubbleSort (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);
                //start timer
                DateTime start = DateTime.Now;
                //sort
                Sorting.bubbleSort(data, p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            results[1][0] = "Bubble Sort";
            results[1][1] = Math.Round((double)totalSteps / props.Length).ToString();
            results[1][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static void evaluateQuickSort (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it
                SeismicRecord[] data = cloneArray(rawData);
                //start timer
                DateTime start = DateTime.Now;
                //sort
                Sorting.quickSort(ref data, 0, data.Length - 1, p, ref steps);
                //stop timer
                DateTime end = DateTime.Now;
                //add to total
                totalSteps += steps;
                totalTime += (end - start).TotalMilliseconds;
            }
            //add to results
            results[0][0] = "Quick Sort";
            results[0][1] = Math.Round((double)totalSteps / props.Length).ToString();
            results[0][2] = Math.Round((totalTime / props.Length), 2).ToString();
        }

        private static SeismicRecord[] cloneArray (SeismicRecord[] data) {
            SeismicRecord[] clone = new SeismicRecord[data.Length];
            int count = 0;
            foreach(SeismicRecord s in data) {
                clone[count] = (SeismicRecord)s.Clone();
                count++;
            }
            return clone;
        }
    }
}