sealed class EratosSieve
{
    bool[] seive;

    public EratosSieve(int limit)
    {
        seive = new bool[limit + 1];
        seive[0] = true;
        seive[1] = true;

        for (int i = 4; i <= limit; i += 2)
            seive[i] = true;

        for (int i = 6; i <= limit; i += 3)
            seive[i] = true;

        for (int i = 5; i * i <= limit; i += 6)
        {
            for (int n = i * i; n <= limit; n += i)
                seive[n] = true;

            int j = i + 2;
            for (int n = j * j; n <= limit; n += j)
                seive[n] = true;
        }
    }

    public bool IsPrime(int num) => !seive[num];

    public List<int> ToList()
    {
        var list = new List<int>();
        for (int i = 0; i < seive.Length; i++)
            if (!seive[i])
                list.Add(i);

        return list;
    }
}