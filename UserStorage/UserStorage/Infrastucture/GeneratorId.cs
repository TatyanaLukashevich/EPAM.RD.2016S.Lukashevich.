using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStorage
{
    /// <summary>
    /// Class-helper for generate id
    /// </summary>
    public class GeneratorId 
    {
        private static int amountOfIterations;

        #region Get primes
        /// <summary>
        /// Get collection of primes
        /// </summary>
        /// <returns>Collection of primes</returns>
        public IEnumerable<int> GetPrimes()
        {
            for (int i = 1; i <= int.MaxValue; i++)
            {
                if (this.IsPrime(i))
                {
                    yield return i;
                }
            }
        }
        #endregion

        #region Get Fibonacci
        /// <summary>
        /// Get collection of Fibonacci numbers
        /// </summary>
        /// <returns>Collection of Fibonacci numbers</returns>
        public IEnumerable<int> GetFibonacci()
        {
            int previous = 0, current = 1;
            yield return previous;
            yield return current;
            while (current < int.MaxValue)
            {
                int next = previous + current;
                yield return next;
                previous = current;
                current = next;
            }
        }
        #endregion

        #region Get evens
        /// <summary>
        /// Get collection of even numbers
        /// </summary>
        /// <returns>Collection of even numbers</returns>
        public IEnumerable<int> GetEvens()
        {
            for (int i = 2; i <= int.MaxValue; i += 2)
            {
                yield return i;
            }
        }
        #endregion


        /// <summary>
        /// Generate id
        /// </summary>
        /// <param name="strategy">GetPrimes/ GetFibonacci/ GetEvens method (or some other way to generate id)</param>
        /// <returns></returns>
        public int StrategyForGenerateId(Func<IEnumerable<int>> strategy)
        {
            var sequence = strategy().Skip(amountOfIterations * 1);
            var idSequence = sequence.Take(1);
            amountOfIterations++;
            var idIsArray = idSequence.ToArray();
            int id = idIsArray[0];
            return id;
        }

        /// <summary>
        /// Method-helper to check whether a number is prime.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>true if number is prime else false</returns>
        private bool IsPrime(int number)
        {
            if (number <= 0)
            {
                return false;
            }

            if (number % 2 == 0 && number != 2)
            {
                return false;
            }

            for (int i = 3; i < number; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}