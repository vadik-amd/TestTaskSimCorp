using System.Text;
using TestTaskSimCorp.DataAccess;

namespace TestTaskSimCorp;

public class UniqueWordsReader : IUniqueWordsReader
{
    private readonly IDataSource _dataSource;
    
    public UniqueWordsReader(IDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<IDictionary<string, int>> GetUniqueWordsAsync(string path, long bufferSize,
        CancellationToken cancellationToken)
    {
        if (bufferSize <= 0)
        {
            throw new ArgumentException("BufferSize should be great than 0.");
        }

        var result = new Dictionary<string, int>();

        await using var stream = _dataSource.Open(path);
        int numRead;
        
        var buffer = new byte[bufferSize];
        while ((numRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) != 0)
        {
            var (words, totalWordsCount, shiftPosition) = ReadWords(buffer, numRead);

            ShiftReadingPosition(stream, shiftPosition);

            for (var i = 0; i < totalWordsCount; i++)
            {
                var word = words[i];
                result.TryGetValue(word, out var count);
                result[word] = ++count;
            }
        }

        return result;
    }

    private (string[], int, int) ReadWords(byte[] buffer, int numRead)
    {
        var text = Encoding.Unicode.GetString(buffer, 0, numRead);
        var words = text.Split(' ');
        var totalWordsCount = words.Length;
        var shiftPosition = 0;
        if (numRead == buffer.Length)
        {
            var lastWordLength = Encoding.Unicode.GetByteCount(words.Last());
            --totalWordsCount;

            if (totalWordsCount <= 0)
            {
                throw new InternalBufferOverflowException("Buffer size if not enough.");
            }

            shiftPosition = lastWordLength;
        }

        return (words, totalWordsCount, shiftPosition);
    }

    private void ShiftReadingPosition(Stream stream, int shiftPosition)
    {
        if (shiftPosition != 0)
        {
            stream.Seek(stream.Position - shiftPosition, SeekOrigin.Begin);
        }
    }
}