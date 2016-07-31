using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStorage;

namespace UserStorageTest
{
    [TestClass]
   public class GeneratorIdTest
    {
        [TestMethod]
        public void GenerateIdTest()
        {
            var generator = new GeneratorId();
            var id = generator.StrategyForGenerateId(generator.GetPrimes);
            var id2 = generator.StrategyForGenerateId(generator.GetPrimes);
            var id3 = generator.StrategyForGenerateId(generator.GetPrimes);
        }
    }
}
