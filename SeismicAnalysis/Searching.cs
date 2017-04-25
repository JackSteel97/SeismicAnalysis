using System;
using System.Collections;
using System.Reflection;

namespace SeismicAnalysis {
    class Searching {

        public static SeismicRecord findMinimumValue(SeismicRecord[] data, PropertyInfo searchProperty) {
            if (data.Length <= 0) {
                throw new Exception("Data cannot be empty");
            }
            SeismicRecord currentMin = data[0];
            for(int i = 1; i < data.Length; i++) {
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[i]);
                    int b = (int)searchProperty.GetValue(currentMin);
                    if (a < b) {
                        currentMin = data[i];
                    }
                }else if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(data[i]);
                    string b = (string)searchProperty.GetValue(currentMin);
                    if (string.Compare(a,b)<0) {
                        currentMin = data[i];
                    }
                }else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[i]);
                    decimal b = (decimal)searchProperty.GetValue(currentMin);
                    if (a<b) {
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

        public static SeismicRecord linearSearchForOne(SeismicRecord[] data, object value, PropertyInfo searchProperty) {
            foreach (SeismicRecord s in data) {
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
                    decimal b = (decimal)value;
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
                    DateTime b = (DateTime)value;
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
            foreach(SeismicRecord s in data) {
                if(searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(s);
                    int b = (int)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;                        
                    }
                }else if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(s);
                    string b = (string)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                }else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(s);
                    decimal b = (decimal)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                }else if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(s);
                    long b = (long)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                } else if (searchProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)searchProperty.GetValue(s);
                    DateTime b = (DateTime)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                }
            }
            if (numOfMatches > 0) {
                SeismicRecord[] output = new SeismicRecord[numOfMatches];
                int count = 0;
                foreach(var match in matches) {
                    output[count] = (SeismicRecord)match;
                    count++;
                }
                return output;
            }else {
                return null;
            }
        }

        public static int binarySearch(SeismicRecord[] data, object value, PropertyInfo searchProperty) {
            uint low = 0;
            uint high = (uint) data.Length - 1;

            while (low <= high) {
                uint mid = (low + high) / 2;
                if(searchProperty.PropertyType == typeof(string)) {
                    string a =(string) searchProperty.GetValue(data[mid]);
                    string b = (string)value;
                    if(string.Compare(a,b) > 0) {
                        high = mid - 1;
                    }else if (string.Compare(a, b) < 0) {
                        low = mid + 1;
                    }else {
                        return (int)mid;
                    }
                }else if (searchProperty.PropertyType == typeof(int)) {
                    int a = (int)searchProperty.GetValue(data[mid]);
                    int b = (int)value;
                    if (a > b) {
                        high = mid - 1;
                    } else if (a<b) {
                        low = mid + 1;
                    } else {
                        return (int)mid;
                    }
                } else if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(data[mid]);
                    decimal b = (decimal)value;
                    if (a > b) {
                        high = mid - 1;
                    } else if (a < b) {
                        low = mid + 1;
                    } else {
                        return (int)mid;
                    }
                } else if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(data[mid]);
                    long b = (long)value;
                    if (a > b) {
                        high = mid - 1;
                    } else if (a < b) {
                        low = mid + 1;
                    } else {
                        return (int)mid;
                    }
                }

            }
            return -1;
        }
    }
}
