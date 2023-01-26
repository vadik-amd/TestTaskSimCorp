namespace TestTaskSimCorp.DataAccess;

public interface IDataSource
{
    Stream Open(string path);
}