using Moq;
using TestTaskSimCorp.DataAccess;

namespace TestTaskSimCorp.Tests.WordsReader;

public class UniqueWordsReaderFixture : BaseFixture<UniqueWordsReader>
{
	protected void WithOpenStream(byte[] buffer)
	{
		Mocker
			.GetMock<IDataSource>()
			.Setup(x => x.Open(It.IsAny<string>()))
			.Returns(new MemoryStream(buffer));
	}
}