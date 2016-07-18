using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage
{
    public class GeneratorId 
    {
        private static int amountOfIterations;

        #region Get primes
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
        public IEnumerable<int> GetFibonacci()
        {
            //if(limit<2)
            // {
            //     throw new ArgumentException("not enouhg digits");
            // }
            //if (limit > 47)
            //{
            //    throw new ArgumentException("sequence is out of int. you only can get 47 elements");
            //}
            int previous = 0, current = 1;
            yield return previous;
            yield return current;
            //int amountOfIteration = 0;
            while (current < int.MaxValue)
            {
                int next = previous + current;
                yield return next;
                previous = current;
                current = next;
                //amountOfIteration++;
            }
        }
        #endregion

        #region Get evens
        public IEnumerable<int> GetEvens()
        {
            for (int i = 2; i <= int.MaxValue; i += 2)
            {
                yield return i;
            }
        }
        #endregion

        public int StrategyForGenerateId(Func<IEnumerable<int>> strategy)
        {
            var sequence = strategy().Skip(amountOfIterations*1);
            var idSequence = sequence.Take(1);
            amountOfIterations++;
            var idIsArray = idSequence.ToArray();
            int id = idIsArray[0];
            return id;
        }
    }
}

