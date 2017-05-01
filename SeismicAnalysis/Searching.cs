using System;
using System.Collections;
using System.Reflection;

namespace SeismicAnalysis {

    /// <summary>
    /// Class containing static methods for searching
    /// </summary>
    internal class Searching {

        /// <summary>
        /// Use a linear type search to go through the entire array and find the lowest value
        /// </summary>
        /// <param name="data">data to search through</param>
        /// <param name="searchProperty">property to search in</param>
        /// <returns>A record with minimum value</returns>
        public static SeismicRecord findMinimumValue (SeismicRecord[] data, PropertyInfo searchProperty) {
            //validate data first
            if(data.Length <= 0) {
                throw new Exception("Data cannot be empty");
            }
            //lowest found so far variable
            SeismicRecord currentMin = data[0];
            //iterate through entire array
            for(int i = 1; i < data.Length; i++) {
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[i]);
                    int b = (int)searchProperty.GetValue(currentMin);
                    //is this item smaller than the current min?
                    if(a < b) {
                        //yes, set it as the new current min
                        currentMin = data[i];
                    }
                } else if(searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[i]);
                    string b = (string)searchProperty.GetValue(currentMin);
                    if(string.Compare(a, b) < 0) {
                        currentMin = data[i];
                    }
                } else if(searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[i]);
                    decimal b = (decimal)searchProperty.GetValue(currentMin);
                    if(a < b) {
                        currentMin = data[i];
                    }
                } else if(searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[i]);
                    long b = (long)searchProperty.GetValue(currentMin);
                    if(a < b) {
                        currentMin = data[i];
                    }
                }
            }
            return currentMin;
        }

        /// <summary>
        /// Use a linear type search to go through the entire array and find the highest value
        /// </summary>
        /// <param name="data">data to search through</param>
        /// <param name="searchProperty">property to search in</param>
        /// <returns>A record with maximum value</returns>
        public static SeismicRecord findMaximumValue (SeismicRecord[] data, PropertyInfo searchProperty) {
            //validate data first
            if(data.Length <= 0) {
                throw new Exception("Data cannot be empty");
            }
            //highest found so far variable
            SeismicRecord currentMax = data[0];
            //iterate through entire array
            for(int i = 1; i < data.Length; i++) {
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[i]);
                    int b = (int)searchProperty.GetValue(currentMax);
                    //is this item bigger than the current max?
                    if(a > b) {
                        //yes, set it as the new current max
                        currentMax = data[i];
                    }
                } else if(searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[i]);
                    string b = (string)searchProperty.GetValue(currentMax);
                    if(string.Compare(a, b) > 0) {
                        currentMax = data[i];
                    }
                } else if(searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[i]);
                    decimal b = (decimal)searchProperty.GetValue(currentMax);
                    if(a > b) {
                        currentMax = data[i];
                    }
                } else if(searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[i]);
                    long b = (long)searchProperty.GetValue(currentMax);
                    if(a > b) {
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
            //iterate through the array
            foreach(SeismicRecord s in data) {
                steps++;
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(s);
                    int b = Convert.ToInt32(value);
                    if(a == b) {
                        //match found, return it
                        return s;
                    }
                } else if(searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(s);
                    string b = (string)value;
                    if(a == b) {
                        return s;
                    }
                } else if(searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(s);
                    decimal b = 0;
                    decimal.TryParse(value.ToString(), out b);
                    if(a == b) {
                        return s;
                    }
                } else if(searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(s);
                    long b = Convert.ToInt64(value);
                    if(a == b) {
                        return s;
                    }
                } else if(searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(s);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);
                    if(a == b) {
                        return s;
                    }
                }
            }
            //entire array searched and no match found, return null
            return null;
        }

        /// <summary>
        /// Search through the entire array and return ALL matches
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        /// <param name="searchProperty"></param>
        /// <returns></returns>
        public static SeismicRecord[] linearSearchForMultiple (SeismicRecord[] data, object value, PropertyInfo searchProperty, ref int steps) {
            //declare array list to store an as-yet unknown number of matches
            ArrayList matches = new ArrayList();
            int numOfMatches = 0;

            foreach(SeismicRecord s in data) {
                steps++;
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(s);
                    int b = Convert.ToInt32(value);
                    if(a == b) {
                        //match found, add to arraylist
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if(searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(s);
                    string b = (string)value;
                    if(a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if(searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(s);
                    decimal b = 0;
                    decimal.TryParse(value.ToString(), out b);
                    if(a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if(searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(s);
                    long b = (long)value;
                    if(a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if(searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(s);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);
                    if(a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                }
            }
            //convert arraylist
            if(numOfMatches > 0) {
                //convert arraylist to array to return
                SeismicRecord[] output = new SeismicRecord[numOfMatches];
                int count = 0;
                foreach(var match in matches) {
                    output[count] = (SeismicRecord)match;
                    count++;
                }
                return output;
            } else {
                //no matches, return null
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
        public static int binarySearch (SeismicRecord[] data, object value, PropertyInfo searchProperty, ref int steps) {
            int low = 0;
            int high = data.Length - 1;
            //repeat until found or all options searched
            while(low <= high) {
                steps++;
                //pick the midpoint
                int mid = (low + high) / 2;
                if(searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[mid]);
                    string b = (string)value;
                    //determine which half of the list to discard
                    if(string.Compare(a, b) > 0) {
                        high = mid - 1;
                    } else if(string.Compare(a, b) < 0) {
                        low = mid + 1;
                    } else {
                        //found, return the index
                        return mid;
                    }
                } else if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[mid]);
                    int b = Convert.ToInt32(value);
                    if(a > b) {
                        high = mid - 1;
                    } else if(a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                } else if(searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[mid]);
                    decimal b = 0;
                    decimal.TryParse(value.ToString(), out b);
                    if(a > b) {
                        high = mid - 1;
                    } else if(a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                } else if(searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[mid]);
                    long b = (long)value;
                    if(a > b) {
                        high = mid - 1;
                    } else if(a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                } else if(searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(data[mid]);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);

                    if(a > b) {
                        high = mid - 1;
                    } else if(a < b) {
                        low = mid + 1;
                    } else {
                        return mid;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Binary search for multiple values using leftmost and rightmost insertion point binary searches
        /// </summary>
        /// <param name="data">data to search</param>
        /// <param name="value">value to search for</param>
        /// <param name="searchProperty">property to search in</param>
        /// <param name="steps">reference to steps counter</param>
        /// <returns>An array of matches</returns>
        public static SeismicRecord[] binarySearchForMultiple (SeismicRecord[] data, object value, PropertyInfo searchProperty, ref int steps) {
            int lower = binarySearchLeft(data, value, searchProperty, ref steps);
            int upper = binarySearchRight(data, value, searchProperty, ref steps);

            SeismicRecord[] output = new SeismicRecord[(upper - lower)];
            Array.Copy(data, lower, output, 0, upper - lower);
            return output;
        }

        /// <summary>
        /// Perform a binary search and return the rightmost match's index
        /// </summary>
        /// <param name="data">data to search</param>
        /// <param name="value">value to search for</param>
        /// <param name="searchProperty">property to search in</param>
        /// <param name="steps">reference to steps counter</param>
        /// <returns>Index of rightmost match</returns>
        private static int binarySearchRight (SeismicRecord[] data, object value, PropertyInfo searchProperty, ref int steps) {
            int low = 0;
            int high = data.Length - 1;

            while(low <= high) {
                int mid = (low + high) / 2;
                steps++;
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[mid]);
                    int b = Convert.ToInt32(value);
                    if(a > b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[mid]);
                    string b = (string)value;
                    if(string.Compare(a, b) > 0) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[mid]);
                    long b = (long)value;
                    if(a > b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[mid]);
                    decimal b = 0;
                    decimal.TryParse(value.ToString(), out b);
                    if(a > b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(data[mid]);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);
                    if(a > b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                }
            }
            return low;
        }

        /// <summary>
        /// Perform a binary search and return the leftmost match's index
        /// </summary>
        /// <param name="data">data to search</param>
        /// <param name="value">value to search for</param>
        /// <param name="searchProperty">property to search in</param>
        /// <param name="steps">reference to steps counter</param>
        /// <returns>Index of leftmost match</returns>
        private static int binarySearchLeft (SeismicRecord[] data, object value, PropertyInfo searchProperty, ref int steps) {
            int low = 0;
            int high = data.Length - 1;

            while(low <= high) {
                int mid = (low + high) / 2;
                steps++;
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[mid]);
                    int b = Convert.ToInt32(value);
                    if(a >= b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[mid]);
                    string b = (string)value;
                    if(string.Compare(a, b) >= 0) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[mid]);
                    long b = (long)value;
                    if(a >= b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[mid]);
                    decimal b = 0;
                    decimal.TryParse(value.ToString(), out b);
                    if(a >= b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                } else if(searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(data[mid]);
                    DateTime b = new DateTime();
                    DateTime.TryParse(value.ToString(), out b);
                    if(a >= b) {
                        high = mid - 1;
                    } else {
                        low = mid + 1;
                    }
                }
            }
            return low;
        }
    }
}