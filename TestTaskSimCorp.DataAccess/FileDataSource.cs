namespace TestTaskSimCorp.DataAccess;

public class FileDataSource : IDataSource
{
    public Stream Open(string path)
    {
        var file = new FileInfo(path);

        return file.OpenRead();
    }
}