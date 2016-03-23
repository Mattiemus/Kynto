using System;
using System.Collections.Generic;

namespace KyntoLib.Helpers
{
    /// <summary>
    /// Provides a set of methods for array management.
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Adds a value to an array.
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="OriginalArray">The original array.</param>
        /// <param name="ToAdd">The item too add to the array.</param>
        /// <returns>The new array.</returns>
        public static T[] AddToArray<T>(T[] OriginalArray, T ToAdd)
        {
            // Store the old array.
            List<T> CurrentArray = new List<T>(OriginalArray);
            // Add the item.
            CurrentArray.Add(ToAdd);
            // Return the compleated array.
            return CurrentArray.ToArray();
        }

        /// <summary>
        /// Adds two arrays together.
        /// </summary>
        /// <typeparam name="T">The type of array.</typeparam>
        /// <param name="ArrayA">The first array.</param>
        /// <param name="ArrayB">The array to add.</param>
        /// <returns>The combined arrays.</returns>
        public static T[] AddArrays<T>(T[] ArrayA, T[] ArrayB)
        {
            // Create a list from the first array.
            List<T> Buffer = new List<T>(ArrayA);
            // Add the arrays.
            Buffer.AddRange(ArrayB);
            // Return the compleated array.
            return Buffer.ToArray();
        }
    }
}
