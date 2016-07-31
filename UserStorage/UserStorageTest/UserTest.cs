using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorage;

namespace UserStorageTest
{
    [TestClass]
    public class UserTest
    { 
        [TestMethod]
        public void OperatorEquality_TwoUsers_ReturnTrue()
        {
            var left = new User();
            var right = left;
            bool equal = left == right;
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void OperatorNotEquality_TwoUsers_ReturnTrue()
        {
            var repo = new UserRepository();
            var left = new User();
            var right = new User();
            repo.Add(left);
            repo.Add(right);
            bool equal = left != right;
            Assert.IsTrue(equal);
        }

        [TestMethod]
        public void Equals_TwoUsers_ReturnTrue()
        {
            var left = new User();
            var right = left;
            bool equal = left.Equals(right);
            Assert.IsTrue(equal);
        }      
    }
}
