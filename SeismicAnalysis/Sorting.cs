using System.Reflection;

namespace SeismicAnalysis {

    internal class Sorting {

        /// <summary>
        /// Quick sort an array of SeismicRecord based on a particular property
        /// Call with: quickSort(data, 0, data.length-1, property)
        /// </summary>
        /// <param name="data">The data array</param>
        /// <param name="sortProperty">Property to sort by</param>
        /// <returns>A sorted array</returns>
        public static void quickSort(ref SeismicRecord[] data, int low, int high, PropertyInfo sortProperty, ref int steps) {
            if (low < high) {
                steps++;
                int p = partition(ref data, low, high, sortProperty);
                quickSort(ref data, low, p - 1, sortProperty, ref steps);
                quickSort(ref data, p + 1, high, sortProperty, ref steps);
            }
        }

        private static int partition(ref SeismicRecord[] data, int low, int high, PropertyInfo sortProperty) {
            SeismicRecord pivot = data[high];
            int i = low - 1;

            for (int j = low; j < high; j++) {
                //extract data that is being compared and cast correctly for accurate comparison
                if (sortProperty.PropertyType == typeof(string)) {
                    string a = (string)sortProperty.GetValue(data[j]);
                    string b = (string)sortProperty.GetValue(pivot);
                    if (string.Compare(a, b) <= 0) {
                        i++;
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                } else if (sortProperty.PropertyType == typeof(int)) {
                    int a = (int)sortProperty.GetValue(data[j]);
                    int b = (int)sortProperty.GetValue(pivot);
                    if (a <= b) {
                        i++;
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                } else if (sortProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)sortProperty.GetValue(data[j]);
                    decimal b = (decimal)sortProperty.GetValue(pivot);
                    if (a <= b) {
                        i++;
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                } else if (sortProperty.PropertyType == typeof(long)) {
                    long a = (long)sortProperty.GetValue(data[j]);
                    long b = (long)sortProperty.GetValue(pivot);
                    if (a <= b) {
                        i++;
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                }
            }
            SeismicRecord temp2 = (SeismicRecord)data[i + 1].Clone();
            data[i + 1] = data[high];
            data[high] = temp2;
            return i + 1;
        }
    }
}