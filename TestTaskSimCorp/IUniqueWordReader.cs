namespace TestTaskSimCorp;

public interface IUniqueWordsReader
{
    Task<IDictionary<string, int>> GetUniqueWordsAsync(string path, long bufferSize, CancellationToken cancellationToken);
}