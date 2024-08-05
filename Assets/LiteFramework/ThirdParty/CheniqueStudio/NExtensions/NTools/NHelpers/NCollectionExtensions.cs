using System;
using System.Collections.Generic;
using System.Linq;
using NExtensions.NTools.NExtensions.Rc;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NCollectionExtensions
    {
        public static void Replace<T>(this List<T> list, Predicate<T> oldItemSelector, T newItem)
        {
            //check for different situations here and throw exception
            //if list contains multiple items that match the predicate
            //or check for nullability of list and etc ...
            var oldItemIndex = list.FindIndex(oldItemSelector);
            list[oldItemIndex] = newItem;
        }
        
        public static bool InBounds (int[] array, int index) 
        {
            return (index >= 0) && (index < array.Length);
        }
        
        public static bool InBounds<T> (this List<T> list, int index) 
        {
            return (index >= 0) && (index < list.Count);
        }
        
        public static List<T> GetRandomElements<T>(this List<T> list, int elementCount = 1)
        {
            return list
                .Shuffled() // Shuffle the collection randomly
                .Take(elementCount) // Take the first 3 elements
                .ToList();
        }
    }
}