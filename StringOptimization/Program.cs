using static System.Console;

var spans = new char[3][];

var str = "hello world again!";
var s2 = str.AsSpan();
var index = s2.IndexOf(' ');
var s3 = s2[..index];

spans[0] = s3.ToArray();

index = index + 1;
var i = index + s3.Length;
var s4 = s2[index..i];
spans[1] = s4.ToArray();

index = i + 1;
var s5 = s2[index..];
spans[2] = s5.ToArray();

foreach (var x3 in 1..10)
    WriteLine(x3);

foreach (var x2 in str.Splice())    
    WriteLine(x2);

foreach (var x in 10)
    WriteLine(x);


public static class Extension
{
    public static CharEnumerator Splice(this string input) => new(input);

    public static IntEnumerator GetEnumerator(this int i) => new(i);

    public static IEnumerator<int> GetEnumerator(this Range r)
    {
        for (var i = r.Start.Value; i < r.End.Value; i++)
        {
            yield return i;
        }
    }

    // public static RangeEnumerator Iterate(this Range r) => new(r.Start.Value, r.End.Value);
}

public ref struct CharEnumerator
{
    private readonly ReadOnlySpan<char> _input;
    private int _index;

    public CharEnumerator(ReadOnlySpan<char> input)
    {
        this._input = input;
        this._index = 0;
        Current = default;
    }

    public CharEnumerator GetEnumerator() => this;

    public bool MoveNext()
    {
        if (_index == _input.Length)
            return false;

        for (var i = _index; i < _input.Length; i++)
        {
            if (_input[i] != ' ')
                continue;
            Current = new Word(_input[_index..i]);
            _index = i + 1;
            return true;
        }

        Current = new Word(_input[_index..]);
        _index = _input.Length;
        return true;
    }

    public Word Current { get; private set; }
}

public readonly ref struct Word
{
    private readonly ReadOnlySpan<char> _input;

    public Word(ReadOnlySpan<char> input)
    {
        this._input = input;
    }

    public static implicit operator char[](Word word) => word._input.ToArray();
}

public ref struct IntEnumerator
{
    private readonly int _i;
    private int _index = -1;

    public IntEnumerator(int i)
    {
        _i = i;
    }

    // needed if the extension method name is not GetEnumerator
    public IntEnumerator GetEnumerator() => this;

    public bool MoveNext() => _index++ < _i;

    public int Current => _index;
}

public ref struct RangeEnumerator
{
    private readonly int _end;

    public RangeEnumerator(int start, int end)
    {
        this.Current = start - 1;
        this._end = end;
    }
    public RangeEnumerator GetEnumerator() => this;
    public bool MoveNext() => ++Current < _end;
    public int Current { get; private set; }
}