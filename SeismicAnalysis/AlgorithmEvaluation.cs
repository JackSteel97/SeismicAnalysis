using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeismicAnalysis {
    class AlgorithmEvaluation {

        //results[i][0] = method
        //results[i][1] = average steps
        //results[i][2] = average time taken
        private static string[][] results = new string[5][];

        public static void runEvaluation (SeismicRecord[] rawData) {
            Console.WriteLine("Please wait, this may take some time...");
            if(!(rawData.Length > 0)) {
                Console.WriteLine("Data empty!");
                return;
            }
            //initalise jagged array
            for(int i = 0; i < results.GetLength(0); i++) {
                results[i] = new string[3];
            }

            

            PropertyInfo[] props = rawData[0].GetType().GetProperties();
            //sorting evaluation
            Console.WriteLine("\tRunning Quick Sort...");
            evaluateQuickSort(props,rawData);
            Console.WriteLine("\tRunning Bubble Sort...");
            evaluateBubbleSort(props, rawData);
            Console.WriteLine("\tRunning Heap Sort...");
            evaluateHeapSort(props, rawData);
            Console.WriteLine("\tRunning Merge Sort...");
            evaluateMergeSort(props, rawData);
            Console.WriteLine("\tRunning Insertion Sort...");
            evaluateInsertionSort(props, rawData);




            sortResultsBySteps();
            outputResults();


        }

        private static void outputResults () {
            Console.Clear();
            Console.WriteLine("Results:");
            Console.WriteLine("\tSorting:\n");      
            //headers
            Console.WriteLine("\t\t{0,-25}\t|\t{1,-25}\t|\t{2,-25}\t", "METHOD","AVERAGE TIME ELAPSED(ms)","AVERAGE STEPS TAKEN");
            //data
            for(int i = 0; i < results.GetLength(0); i++) {
                Console.WriteLine("\t\t{0,-25}\t|\t{1,-25}\t|\t{2:n0}\t", results[i][0],Convert.ToDouble(results[i][2]),Convert.ToInt32(results[i][1]));
            }
            

        }

        private static void sortResultsBySteps () {
            //bubble sort because the array is very small and it is easy to code
            bool isSorted = true;
            for(int i = 0; i < results.GetLength(0)-1; i++){
                isSorted = true;
                for(int j=0; j < results.GetLength(0) - 1 - i; j++){
                    int a = Convert.ToInt32(results[j+1][1]);
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
            results[4][2] = Math.Round((totalTime / props.Length),2).ToString();
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

        private static void evaluateBubbleSort(PropertyInfo[] props, SeismicRecord[] rawData) {
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
                Sorting.bubbleSort(data,p,ref steps);
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
