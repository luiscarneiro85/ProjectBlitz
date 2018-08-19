using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

    //extension method to aid in algorithm implementation
    public static class Extensions
    {
        //converts string representation of number to a double
        //public static IEnumerable<double> ConvertToDouble<T>(this IEnumerable<T> array)
        //{
        //    dynamic ds;
        //    foreach (object st in array)
        //    {
        //        ds = st;
        //        yield return Convert.ToDouble(ds);
        //    }
        //}

        //returns a row in a 2D array
        public static T[] Row<T>(this T[,] array, int r)
        {
            T[] output = new T[array.GetLength(1)];
            if (r < array.GetLength(0))
            {
                for (int i = 0; i < array.GetLength(1); i++)
                    output[i] = array[r, i];
            }
            return output;
        }

        //converts a List of Lists to a 2D matrix
        public static T[,] ToMatrix<T>(this IEnumerable<List<T>> collection, int depth, int length)
        {
            T[,] output = new T[depth, length];
            int i = 0, j = 0;
            foreach (var list in collection)
            {
                foreach (var val in list)
                {
                    output[i, j] = val;
                    j++;
                }
                i++; j = 0;
            }

            return output;
        }

        //returns the classification that appears most frequently in the array of classifications
        public static string Majority<T>(this T[] array)
        {
            if (array.Length > 0)
            {
                int unique = array.Distinct().Count();
                if (unique == 1)
                    return array[0].ToString();

                return (from item in array
                        group item by item into g
                        orderby g.Count() descending
                        select g.Key).First().ToString();
            }
            else
                return "";
        }
    }

    /// <summary>
    /// kNN class implements the K Nearest Neighbor instance based classifier
    /// </summary>
    

    //class EntryPoint
    //{
    //    static void Main(string[] args)
    //    {
    //        kNN examplekNN = kNN.initialiseKNN(1, "teste.txt", true);

    //        List<double> instance2Classify = new List<double> { 54, 16, 36, 31, 63, 51, 89 };
    //        string result = examplekNN.Classify(instance2Classify);
    //        Console.WriteLine("This instance is classified as: {0}", result);
    //        Console.ReadLine();
    //    }
    //}
