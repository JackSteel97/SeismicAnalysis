using System;
using System.Collections;
using System.Reflection;

namespace SeismicAnalysis {

    internal class Searching {

        public static SeismicRecord findMinimumValue(SeismicRecord[] data, PropertyInfo searchProperty) {
            if (data.Length <= 0) {
                throw new Exception("Data cannot be empty");
            }
            SeismicRecord currentMin = data[0];
            for (int i = 1; i < data.Length; i++) {
                if (searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[i]);
                    int b = (int)searchProperty.GetValue(currentMin);
                    if (a < b) {
                        currentMin = data[i];
                    }
                } else if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[i]);
                    string b = (string)searchProperty.GetValue(currentMin);
                    if (string.Compare(a, b) < 0) {
                        currentMin = data[i];
                    }
                } else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[i]);
                    decimal b = (decimal)searchProperty.GetValue(currentMin);
                    if (a < b) {
                        currentMin = data[i];
                    }
                } else if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[i]);
                    long b = (long)searchProperty.GetValue(currentMin);
                    if (a < b) {
                        currentMin = data[i];
                    }
                }
            }
            return currentMin;
        }

        public static SeismicRecord findMaximumValue(SeismicRecord[] data, PropertyInfo searchProperty) {
            if (data.Length <= 0) {
                throw new Exception("Data cannot be empty");
            }
            SeismicRecord currentMax = data[0];
            for (int i = 1; i < data.Length; i++) {
                if (searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[i]);
                    int b = (int)searchProperty.GetValue(currentMax);
                    if (a > b) {
                        currentMax = data[i];
                    }
                } else if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[i]);
                    string b = (string)searchProperty.GetValue(currentMax);
                    if (string.Compare(a, b) > 0) {
                        currentMax = data[i];
                    }
                } else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[i]);
                    decimal b = (decimal)searchProperty.GetValue(currentMax);
                    if (a > b) {
                        currentMax = data[i];
                    }
                } else if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[i]);
                    long b = (long)searchProperty.GetValue(currentMax);
                    if (a > b) {
                        currentMax = data[i];
                    }
                }
            }
            return currentMax;
        }

        /// <summary>
        /// Linear search
        /// Space Complexity: O(1)
        /// Time Complexity:
        ///     Best: O(1)
        ///  Average: O(n)
        ///    Worst: O(n)
        /// </summary>
        /// <param name="data">Data to search through</param>
        /// <param name="value">Value to search for</param>
        /// <param name="searchProperty">Property to search</param>
        /// <param name="steps">reference to step counter</param>
        /// <returns>A single SeismicRecord containing the search value, or, null if the value cannot be found</returns>
        public static SeismicRecord linearSearchForOne (SeismicRecord[] data, object value, PropertyInfo searchProperty, ref int steps) {
            foreach (SeismicRecord s in data) {
                steps++;
                if (searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(s);
                    int b = (int)value;
                    if (a == b) {
                        return s;
                    }
                } else if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(s);
                    string b = (string)value;
                    if (a == b) {
                        return s;
                    }
                } else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(s);
                    decimal b = 0;
                    decimal.TryParse((string)value, out b);
                    if (a == b) {
                        return s;
                    }
                } else if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(s);
                    long b = (long)value;
                    if (a == b) {
                        return s;
                    }
                } else if (searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(s);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);
                    if (a == b) {
                        return s;
                    }
                }
            }
            return null;
        }

        public static SeismicRecord[] linearSearchForMultiple(SeismicRecord[] data, object value, PropertyInfo searchProperty) {
            ArrayList matches = new ArrayList();
            int numOfMatches = 0;
            foreach (SeismicRecord s in data) {
                if (searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(s);
                    int b = (int)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(s);
                    string b = (string)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(s);
                    decimal b = 0;
                    decimal.TryParse((string)value, out b);
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(s);
                    long b = (long)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if (searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(s);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                }
            }
            if (numOfMatches > 0) {
                SeismicRecord[] output = new SeismicRecord[numOfMatches];
                int count = 0;
                foreach (var match in matches) {
                    output[count] = (SeismicRecord)match;
                    count++;
                }
                return output;
            } else {
                return null;
            }
        }

        /// <summary>
        /// Binary search for single value
        /// Space complexity: O(1)
        /// Time complexity:
        ///     Best: O(1)
        ///  Average: O(log(n))
        ///    Worst: O(log(n))
        /// </summary>
        /// <param name="data">Data to search through</param>
        /// <param name="value">Value to search for</param>
        /// <param name="searchProperty">Property to search</param>
        /// <returns>An index integer to the searched value, or, -1 if the value cannot be found</returns>
        public static int binarySearch(SeismicRecord[] data, object value, PropertyInfo searchProperty, ref int steps) {
            int low = 0;
            int high = data.Length - 1;

            while (low <= high) {
                steps++;
                int mid = (low + high) / 2;
                if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[mid]);
                    string b = (string)value;
                    if (string.Compare(a, b) > 0) {
                        high = mid - 1;
                    } else if (string.Compare(a, b) < 0) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                } else if (searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[mid]);
                    int b = (int)value;
                    if (a > b) {
                        high = mid - 1;
                    } else if (a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                } else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[mid]);
                    decimal b = 0;
                    decimal.TryParse((string)value, out b);
                    if (a > b) {
                        high = mid - 1;
                    } else if (a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                } else if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[mid]);
                    long b = (long)value;
                    if (a > b) {
                        high = mid - 1;
                    } else if (a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                } else if (searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(data[mid]);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);
                   
                    if (a > b) {
                        high = mid - 1;
                    } else if (a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                }
            }
            return -1;
        }
    }
}