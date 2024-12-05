using System.Buffers.Text;
using System.Runtime.CompilerServices;

#region FastIO
sealed class Reader : IDisposable
{
    readonly Stream input = Console.OpenStandardInput();
    byte[] buffer = new byte[1 << 12];
    char[] stringBuffer = new char[1 << 12];
    ulong[] delimiters = new ulong[4];
    int position = 0;
    int length = 0;
    bool streamEnd = false;

    Span<byte> Fragment => buffer.AsSpan(position, length - position);

    public Reader() => delimiters[0] = 0x1FFFFFFFF;
    public Reader(int bufferSize) : this() => buffer = new byte[bufferSize];
    public Reader(Stream stream, int bufferSize = 1 << 12) : this(bufferSize) => input = stream;
    public Reader(string path, int bufferSize = 1 << 12) : this(new FileStream(path, FileMode.Open), bufferSize) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)] void SkipDelimiter() { while (position < length && IsDelimiter(buffer[position])) position++; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] bool IsDelimiter(byte c) => (delimiters[c / 64] & (1UL << (c % 64))) > 0;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void AddDelimiter(char c) => delimiters[c / 64] |= 1UL << (c % 64);
    [MethodImpl(MethodImplOptions.AggressiveInlining)] public void RemoveDelimiter(char c) => delimiters[c / 64] &= ~(1UL << (c % 64));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool TokenReady()
    {
        for (int i = position; i < length; i++)
            if (IsDelimiter(buffer[i]))
                return true;

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void ReadToken(bool delim = true)
    {
        if (delim)
            SkipDelimiter();

        if (streamEnd)
            return;

        if (position == length)
            position = length = 0;

        if (!TokenReady())
        {
            var remain = Fragment.Length;
            Fragment.CopyTo(buffer);
            position = 0;
            length = remain;
        }

        if (position == 0)
        {
            int read = input.Read(buffer, length, buffer.Length - length);
            if (read == 0)
                streamEnd = true;
        
            length += read;

            if (delim)
                SkipDelimiter();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int NextInt()
    {
        ReadToken();

        if (!Utf8Parser.TryParse(Fragment, out int value, out int bytesConsumed))
            throw new FormatException();

        position += bytesConsumed;

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long NextLong()
    {
        ReadToken();

        if (!Utf8Parser.TryParse(Fragment, out long value, out int bytesConsumed))
            throw new FormatException();

        position += bytesConsumed;

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double NextDouble()
    {
        ReadToken();

        if (!Utf8Parser.TryParse(Fragment, out double value, out int bytesConsumed))
            throw new FormatException();

        position += bytesConsumed;

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public char NextChar()
    {
        ReadToken();
        return (char)buffer[position++];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string NextString()
    {
        int strPos = 0;

        ReadToken();
        while (true)
        {
            if (position == length)
                ReadToken();

            if (IsDelimiter(buffer[position]))
                break;

            if (strPos >= stringBuffer.Length)
                Array.Resize(ref stringBuffer, stringBuffer.Length << 1);

            stringBuffer[strPos++] = (char)buffer[position++];
        }

        return strPos > 0 ? stringBuffer.AsSpan(0, strPos).ToString() : "";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string NextLine()
    {
        int strPos = 0;

        ReadToken(false);
        while (true)
        {
            if (position == length)
                ReadToken(false);

            if (buffer[position] == '\r' || buffer[position] == '\n')
            {
                if (buffer[position] == '\r' && buffer[position + 1] == '\n')
                    position++;

                position++;
                break;
            }

            if (strPos >= stringBuffer.Length)
                Array.Resize(ref stringBuffer, stringBuffer.Length << 1);

            stringBuffer[strPos++] = (char)buffer[position++];
        }

        return strPos > 0 ? stringBuffer.AsSpan(0, strPos).ToString() : "";
    }

    // TODO: This method is not working as intented.
    public bool IsStreamEnd() => streamEnd && position >= length;

    public void Dispose()
    {
        input.Dispose();
    }
}

sealed class Writer : IDisposable
{
    readonly Stream output = Console.OpenStandardOutput();
    byte[] buffer = new byte[1 << 12];
    int cursor = 0;

    public Writer() {}
    public Writer(int bufferSize) => buffer = new byte[bufferSize];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(string value)
    {
        int consume = 0, length = value.Length;
        while (consume < length)
        {
            if (cursor == buffer.Length)
                Flush();
            
            buffer[cursor++] = (byte)value[consume++];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(char value)
    {
        if (cursor == buffer.Length)
            Flush();

        buffer[cursor++] = (byte)value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write<T>(T value)
    {
        Write(value.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLine()
    {
        Write('\n');
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteLine<T>(T value)
    {
        Write(value);
        Write('\n');
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Flush()
    {
        output.Write(buffer.AsSpan(0, cursor));
        cursor = 0;
    }

    public void Dispose()
    {
        Flush();
        output.Flush();
        output.Dispose();
    }
}
#endregion