static bool IsPrime(int v)
{
    if (v == 2 || v == 3)
        return true;

    if (v <= 1 || v % 2 == 0 || v % 3 == 0)
        return false;

    for (int i = 5; i * i <= v; i += 6)
    {
        if (v % i == 0 || v % (i + 2) == 0)
            return false;
    }

    return true;
}