using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeismicAnalysis {
    class Searching {

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
                }
                if (searchProperty.PropertyType == typeof(string)) {
                    string a = (string)searchProperty.GetValue(s);
                    string b = (string)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                }
                if (searchProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)searchProperty.GetValue(s);
                    decimal b = (decimal)value;
                    if (a == b) {
                        matches.Add(s);
                        numOfMatches++;
                    }
                }
                if (searchProperty.PropertyType == typeof(long)) {
                    long a = (long)searchProperty.GetValue(s);
                    long b = (long)value;
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
