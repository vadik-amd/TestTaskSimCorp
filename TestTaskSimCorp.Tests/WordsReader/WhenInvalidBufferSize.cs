using AutoFixture.NUnit3;
using NUnit.Framework;

namespace TestTaskSimCorp.Tests.WordsReader;

public class WhenInvalidBufferSize : UniqueWordsReaderFixture
{
	[Test]
	[InlineAutoData(0)]
	[InlineAutoData(-10)]
	public void ThrowArgumentException(int bufferSize, string filePath, byte wordsCont)
	{
		Assert.That(async () => await Subject.GetUniqueWordsAsync(filePath, bufferSize, Token), 
			Throws.Exception.TypeOf<ArgumentException>());
	}
}