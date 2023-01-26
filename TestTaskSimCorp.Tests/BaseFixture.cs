using AutoFixture;
using Moq.AutoMock;

namespace TestTaskSimCorp.Tests;

public class BaseFixture<T> : BaseFixture where T : class 
{
    protected BaseFixture()
    {
        _subjectLazy = new Lazy<T>(() => Mocker.CreateInstance<T>(true));
    }

    private readonly Lazy<T> _subjectLazy;
    protected T Subject => _subjectLazy.Value;
}
    
public class BaseFixture : IDisposable 
{
    protected AutoMocker Mocker { get; } = new();
    protected Fixture Fixture { get; } = new Fixture();
    protected CancellationToken Token { get; } = new();

    public void Dispose() => Mocker.VerifyAll();
}