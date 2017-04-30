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
        private string[][] results = new string[5][];

        public static void runEvaluation (SeismicRecord[] rawData) {
            Console.WriteLine("\nSorting: ");
            if(!(rawData.Length > 0)) {
                Console.WriteLine("Data empty!");
                return;
            }

            PropertyInfo[] props = rawData[0].GetType().GetProperties();


        }

        private void evaluateQuickSort (PropertyInfo[] props, SeismicRecord[] rawData) {
            int totalSteps = 0;
            double totalTime = 0;
            //sort for each property to get an average
            foreach(PropertyInfo p in props) {
                int steps = 0;
                //clone raw data first to avoid changing it since quicksort works by reference
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
            results[0][2] = Math.Round(totalTime / props.Length).ToString();
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
