﻿using System;
using System.Collections;
using System.Reflection;

namespace SeismicAnalysis {

    internal class Sorting {

        public static void heapSort (ref SeismicRecord[] data, PropertyInfo sortProperty, ref int steps) {
            //build max heap
            int heapSize = data.Length - 1;
            for(int j = (heapSize / 2) - 1; j >= 0; j--) {
                MaxHeapify(ref data, heapSize, j, sortProperty, ref steps);
            }

            for(int i = data.Length - 1; i > 0; i--) {
                //swap
                steps++;
                SeismicRecord temp = data[i];
                data[i] = data[0];
                data[0] = temp;

                heapSize--;
                MaxHeapify(ref data, heapSize, 0, sortProperty, ref steps);
            }
        }

        private static void MaxHeapify (ref SeismicRecord[] data, int heapSize, int index, PropertyInfo sortProperty, ref int steps) {
            int left = (2 * (index + 1)) - 1;
            int right = 2 * (index + 1);
            int largest = index;
            if(sortProperty.PropertyType == typeof(int)) {
                if(left < heapSize) {
                    int a = (int)sortProperty.GetValue(data[left]);
                    int b = (int)sortProperty.GetValue(data[index]);
                    if(a > b) {
                        largest = left;
                    }
                } else {
                    largest = index;
                }

                if(right < heapSize) {
                    int a = (int)sortProperty.GetValue(data[right]);
                    int b = (int)sortProperty.GetValue(data[largest]);
                    if(a > b) {
                        largest = right;
                    }
                }
            } else if(sortProperty.PropertyType == typeof(string)) {
                if(left < heapSize) {
                    string a = (string)sortProperty.GetValue(data[left]);
                    string b = (string)sortProperty.GetValue(data[index]);
                    if(string.Compare(a,b)>0) {
                        largest = left;
                    }
                } else {
                    largest = index;
                }

                if(right < heapSize) {
                    string a = (string)sortProperty.GetValue(data[right]);
                    string b = (string)sortProperty.GetValue(data[largest]);
                    if(string.Compare(a, b) > 0) {
                        largest = right;
                    }
                }
            } else if(sortProperty.PropertyType == typeof(decimal)) {
                if(left < heapSize) {
                    decimal a = (decimal)sortProperty.GetValue(data[left]);
                    decimal b = (decimal)sortProperty.GetValue(data[index]);
                    if(a > b) {
                        largest = left;
                    }
                } else {
                    largest = index;
                }

                if(right < heapSize) {
                    decimal a = (decimal)sortProperty.GetValue(data[right]);
                    decimal b = (decimal)sortProperty.GetValue(data[largest]);
                    if(a > b) {
                        largest = right;
                    }
                }
            } else if(sortProperty.PropertyType == typeof(long)) {
                if(left < heapSize) {
                    long a = (long)sortProperty.GetValue(data[left]);
                    long b = (long)sortProperty.GetValue(data[index]);
                    if(a > b) {
                        largest = left;
                    }
                } else {
                    largest = index;
                }

                if(right < heapSize) {
                    long a = (long)sortProperty.GetValue(data[right]);
                    long b = (long)sortProperty.GetValue(data[largest]);
                    if(a > b) {
                        largest = right;
                    }
                }
            } else if(sortProperty.PropertyType == typeof(DateTime)) {
                if(left < heapSize) {
                    DateTime a = (DateTime)sortProperty.GetValue(data[left]);
                    DateTime b = (DateTime)sortProperty.GetValue(data[index]);
                    if(a > b) {
                        largest = left;
                    }
                } else {
                    largest = index;
                }

                if(right < heapSize) {
                    DateTime a = (DateTime)sortProperty.GetValue(data[right]);
                    DateTime b = (DateTime)sortProperty.GetValue(data[largest]);
                    if(a > b) {
                        largest = right;
                    }
                }
            }

            if(largest != index) {
                //swap
                steps++;
                SeismicRecord temp = data[index];
                data[index] = data[largest];
                data[largest] = temp;

                MaxHeapify(ref data, heapSize, largest, sortProperty, ref steps);
            }
        }

        /// <summary>
        /// Insertion sort data
        /// Space complexity: O(1)
        /// Time complexity:
        ///     Best: O(n)
        ///  Average: O(n^2)
        ///    Worst: O(n^2)
        /// </summary>
        /// <param name="data">data to sort</param>
        /// <param name="sortProperty">property to sort by</param>
        /// <param name="steps">reference to step counter</param>
        /// <returns>A sorted array</returns>
        public static SeismicRecord[] insertionSort (SeismicRecord[] data, PropertyInfo sortProperty, ref int steps) {
            for(int i = 1; i < data.Length; i++) {
                SeismicRecord temp = data[i];
                int j = i - 1;
                while(j >= 0) {
                    steps++;
                    if(sortProperty.PropertyType == typeof(int)) {
                        int a = (int)sortProperty.GetValue(data[j]);
                        int b = (int)sortProperty.GetValue(temp);
                        if(a > b) {
                            //swap
                            data[j + 1] = data[j];
                            j--;
                        } else {
                            break;
                        }
                    } else if(sortProperty.PropertyType == typeof(string)) {
                        string a = (string)sortProperty.GetValue(data[j]);
                        string b = (string)sortProperty.GetValue(temp);
                        if(string.Compare(a, b) > 0) {
                            //swap
                            data[j + 1] = data[j];
                            j--;
                        } else {
                            break;
                        }
                    } else if(sortProperty.PropertyType == typeof(decimal)) {
                        decimal a = (decimal)sortProperty.GetValue(data[j]);
                        decimal b = (decimal)sortProperty.GetValue(temp);
                        if(a > b) {
                            //swap
                            data[j + 1] = data[j];
                            j--;
                        } else {
                            break;
                        }
                    } else if(sortProperty.PropertyType == typeof(long)) {
                        long a = (long)sortProperty.GetValue(data[j]);
                        long b = (long)sortProperty.GetValue(temp);
                        if(a > b) {
                            //swap
                            data[j + 1] = data[j];
                            j--;
                        } else {
                            break;
                        }
                    }
                    data[j + 1] = temp;
                }
            }
            return data;
        }

        /// <summary>
        /// Quick sort an array of SeismicRecord based on a particular property
        /// Call with: quickSort(data, 0, data.length-1, property)
        /// Space Complexity: O(log(n))
        /// Time Complexity:
        ///     Best:  O(nlog(n))
        ///  Average:  O(nlog(n))
        ///    Worst:  O(n^2)
        /// </summary>
        /// <param name="data">The unsorted data array</param>
        /// <param name="sortProperty">Property to sort by</param>
        /// <returns>A sorted array</returns>
        public static void quickSort (ref SeismicRecord[] data, int low, int high, PropertyInfo sortProperty, ref int steps) {
            if(low < high) {
                steps++;
                int p = partition(ref data, low, high, sortProperty, ref steps);
                quickSort(ref data, low, p - 1, sortProperty, ref steps);
                quickSort(ref data, p + 1, high, sortProperty, ref steps);
            }
        }

        /// <summary>
        /// Array partition used by quick sort
        /// </summary>
        /// <param name="data">Array</param>
        /// <param name="low">low index pointer</param>
        /// <param name="high">high index pointer</param>
        /// <param name="sortProperty">Property to sort by</param>
        /// <returns>Partition point index</returns>
        private static int partition (ref SeismicRecord[] data, int low, int high, PropertyInfo sortProperty, ref int steps) {
            SeismicRecord pivot = data[high];
            int i = low - 1;

            for(int j = low; j < high; j++) {
                steps++;
                //extract data that is being compared and cast correctly for accurate comparison
                if(sortProperty.PropertyType == typeof(string)) {
                    string a = (string)sortProperty.GetValue(data[j]);
                    string b = (string)sortProperty.GetValue(pivot);
                    if(string.Compare(a, b) <= 0) {
                        i++;
                        //swap
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                } else if(sortProperty.PropertyType == typeof(int)) {
                    int a = (int)sortProperty.GetValue(data[j]);
                    int b = (int)sortProperty.GetValue(pivot);
                    if(a <= b) {
                        i++;
                        //swap
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                } else if(sortProperty.PropertyType == typeof(decimal)) {
                    decimal a = (decimal)sortProperty.GetValue(data[j]);
                    decimal b = (decimal)sortProperty.GetValue(pivot);
                    if(a <= b) {
                        i++;
                        //swap
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                } else if(sortProperty.PropertyType == typeof(long)) {
                    long a = (long)sortProperty.GetValue(data[j]);
                    long b = (long)sortProperty.GetValue(pivot);
                    if(a <= b) {
                        i++;
                        //swap
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                } else if(sortProperty.PropertyType == typeof(DateTime)) {
                    DateTime a = (DateTime)sortProperty.GetValue(data[j]);
                    DateTime b = (DateTime)sortProperty.GetValue(pivot);

                    if(a <= b) {
                        i++;
                        //swap
                        SeismicRecord temp = (SeismicRecord)data[i].Clone();
                        data[i] = data[j];
                        data[j] = temp;
                    }
                }
            }
            //swap

            SeismicRecord temp2 = (SeismicRecord)data[i + 1].Clone();
            data[i + 1] = data[high];
            data[high] = temp2;
            return i + 1;
        }

        /// <summary>
        /// Merge sort an array of SeismicRecord based on a particular property
        /// Space complexity: O(n)
        /// Time complexity:
        ///     Best:   O(nlog(n))
        ///  Average:   O(nlog(n))
        ///    Worst:   O(nlog(n))
        /// </summary>
        /// <param name="data">The unsorted array</param>
        /// <param name="sortProperty">Property to sort by</param>
        /// <param name="steps">The number of steps taken to sort</param>
        /// <returns>A sorted array</returns>
        public static SeismicRecord[] mergeSort (SeismicRecord[] data, PropertyInfo sortProperty, ref int steps) {
            steps++;
            if(data.Length <= 1) {
                return data;
            }

            int middleIndex = (data.Length) / 2;
            SeismicRecord[] left = new SeismicRecord[middleIndex];
            SeismicRecord[] right = new SeismicRecord[data.Length - middleIndex];

            Array.Copy(data, left, middleIndex);
            Array.Copy(data, middleIndex, right, 0, right.Length);

            left = mergeSort(left, sortProperty, ref steps);
            right = mergeSort(right, sortProperty, ref steps);

            return merge(left, right, sortProperty, ref steps);
        }

        /// <summary>
        /// Merge two sorted arrays into one sorted array
        /// </summary>
        /// <param name="a">Array one</param>
        /// <param name="b">Array two</param>
        /// <param name="sortProperty">Property to sort by</param>
        /// <returns>A Sorted array</returns>
        private static SeismicRecord[] merge (SeismicRecord[] a, SeismicRecord[] b, PropertyInfo sortProperty, ref int steps) {
            ArrayList c = new ArrayList();

            while(a.Length > 0 && b.Length > 0) {
                steps++;
                if(sortProperty.PropertyType == typeof(int)) {
                    int itemA = (int)sortProperty.GetValue(a[0]);
                    int itemB = (int)sortProperty.GetValue(b[0]);
                    if(itemA > itemB) {
                        c.Add(b[0].Clone());
                        b = removeAt(0, b);
                    } else {
                        c.Add(a[0].Clone());
                        a = removeAt(0, a);
                    }
                } else if(sortProperty.PropertyType == typeof(string)) {
                    string itemA = (string)sortProperty.GetValue(a[0]);
                    string itemB = (string)sortProperty.GetValue(b[0]);
                    if(string.Compare(itemA, itemB) > 0) {
                        c.Add(b[0].Clone());
                        b = removeAt(0, b);
                    } else {
                        c.Add(a[0].Clone());
                        a = removeAt(0, a);
                    }
                } else if(sortProperty.PropertyType == typeof(decimal)) {
                    decimal itemA = (decimal)sortProperty.GetValue(a[0]);
                    decimal itemB = (decimal)sortProperty.GetValue(b[0]);
                    if(itemA > itemB) {
                        c.Add(b[0].Clone());
                        b = removeAt(0, b);
                    } else {
                        c.Add(a[0].Clone());
                        a = removeAt(0, a);
                    }
                } else if(sortProperty.PropertyType == typeof(long)) {
                    long itemA = (long)sortProperty.GetValue(a[0]);
                    long itemB = (long)sortProperty.GetValue(b[0]);
                    if(itemA > itemB) {
                        c.Add(b[0].Clone());
                        b = removeAt(0, b);
                    } else {
                        c.Add(a[0].Clone());
                        a = removeAt(0, a);
                    }
                } else if(sortProperty.PropertyType == typeof(DateTime)) {
                    DateTime itemA = (DateTime)sortProperty.GetValue(a[0]);
                    DateTime itemB = (DateTime)sortProperty.GetValue(b[0]);
                    if(itemA > itemB) {
                        c.Add(b[0].Clone());
                        b = removeAt(0, b);
                    } else {
                        c.Add(a[0].Clone());
                        a = removeAt(0, a);
                    }
                }
            }

            while(a.Length > 0) {
                steps++;
                c.Add(a[0].Clone());
                a = removeAt(0, a);
            }

            while(b.Length > 0) {
                steps++;
                c.Add(b[0].Clone());
                b = removeAt(0, b);
            }

            SeismicRecord[] output = new SeismicRecord[c.Count];
            int count = 0;
            foreach(SeismicRecord s in c) {
                output[count] = (SeismicRecord)s.Clone();
                count++;
            }

            return output;
        }

        /// <summary>
        /// Remove an item at the specified index from the array
        /// </summary>
        /// <param name="index">Index to remove</param>
        /// <param name="inputArray">Array to remove from</param>
        /// <returns>An array without the specified element</returns>
        private static SeismicRecord[] removeAt (int index, SeismicRecord[] inputArray) {
            SeismicRecord[] output = new SeismicRecord[inputArray.Length - 1];
            int count = 0;
            for(int i = 0; i < inputArray.Length; i++) {
                if(i != index) {
                    output[count] = (SeismicRecord)inputArray[i].Clone();
                    count++;
                }
            }
            return output;
        }
    }
}