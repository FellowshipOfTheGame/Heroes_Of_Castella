namespace HeroesOfCastella
{
    public static class ThreadSafeRandom
    {
        private static System.Random _global = new System.Random();
        [System.ThreadStatic]
        private static System.Random _local;

        public static int Next()
        {
            System.Random inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new System.Random(seed);
            }
            return inst.Next();
        }

        public static int Range(int a1, int a2)
        {
            if (a1 == a2)
                return a1;
            int length = a2 - a1;
            int offset = Next() % length;
            return offset + a1;
        }
    }
}

