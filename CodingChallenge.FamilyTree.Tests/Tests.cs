using System;
using NUnit.Framework;

namespace CodingChallenge.FamilyTree.Tests
{
    [TestFixture]
    public class TreeTests
    {
        [TestCase(1)]
        [TestCase(33)] // BUG: Mock data does not always generate a node with this index
        [TestCase(22)]
        public void if_the_person_exists_in_tree_the_result_should_be_their_birthday(int index)
        {
            var tree = FamilyTreeGenerator.Make();
            var result = new Solution().GetBirthMonth(tree, "Name" + index);
            // When result is valid, values will match. 
            Assert.AreEqual(!string.IsNullOrWhiteSpace(result), result == DateTime.Now.AddDays(index - 1).ToString("MMMM"));
            // When result is invalid, it is because FamilyTreeGenerator did not make it.
            Assert.AreEqual(result == string.Empty, new Solution().FindPerson(tree, "Name" + index) is null, $"FamilyTreeGenerator did not create nod for index: {index}");
        }

        [Test]
        public void if_the_person_does_not_exist_in_the_tree_the_result_should_be_empty()
        {
            var tree = FamilyTreeGenerator.Make();
            var result = new Solution().GetBirthMonth(tree, "Jeebus");
            Assert.AreEqual("",result);
        }
    }
}
