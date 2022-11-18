using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CodingChallenge.FamilyTree
{
    public class Solution
    {
        public string GetBirthMonth(Person person, string descendantName)
        {
            var descendant = FindPerson(person, descendantName);
            return descendant != default ? descendant.Birthday.ToString("MMMM", CultureInfo.InvariantCulture) : string.Empty;
        }

        public Person FindPerson(Person person, string descendantName)
        {
            return person.Name == descendantName
                    ? person
                    : person.Descendants.FirstOrDefaultRecursiveScan(p => p.Descendants, p => p.Name == descendantName);
        }

    }

    public static class IEnumerableExtensions
    {
        public static T FirstOrDefaultRecursiveScan<T>(this IEnumerable<T> owner, 
            Func<T, IEnumerable<T>> childrenSelector,
            Predicate<T> condition)
        {
            // Handle no items in collection
            if (owner == null || !owner.Any())
            {
                return default;
            }
            // When found return item
            var result = owner.FirstOrDefault(t => condition(t));
            if (!Equals(result, default(T)))
            {
                return result;
            }
            // Call recursively
            return owner
                .SelectMany(childrenSelector)
                .FirstOrDefaultRecursiveScan(childrenSelector, condition);
        }
    }
}