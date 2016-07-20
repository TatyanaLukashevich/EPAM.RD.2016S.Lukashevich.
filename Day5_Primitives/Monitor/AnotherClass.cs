namespace Monitor
{
    // TODO: Use SpinLock to protect this structure.
    public class AnotherClass
    {
        private int _value;
        private System.Threading.SpinLock spLock = new System.Threading.SpinLock();

        public int Counter
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public void Increase()
        {
            bool gotLock = false;
            try
            {
                if (!spLock.IsHeldByCurrentThread)
                    spLock.Enter(ref gotLock);
                _value++;
            }
            finally
            {
                if (gotLock)
                    spLock.Exit();
            }
        }

        public void Decrease()
        {
            bool gotLock = false;
            try
            {
                if (!spLock.IsHeldByCurrentThread)
                    spLock.Enter(ref gotLock);
                _value--;
            }
            finally
            {
                if (gotLock)
                    spLock.Exit();
            }
        }
    }
}
