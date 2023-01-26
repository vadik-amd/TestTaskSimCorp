using System.Text;
using AutoFixture;
using AutoFixture.NUnit3;
using NUnit.Framework;

namespace TestTaskSimCorp.Tests.WordsReader;

public class WhenSmallBuffer : UniqueWordsReaderFixture
{
	[Test]
	[InlineAutoData(1)]
	[InlineAutoData(0)]
	[InlineAutoData(-1)]
	[InlineAutoData(-5)]
	public void ThrowException(long bufferSizeDefect, string filePath, byte repeats)
	{
		var uniqueArray = Fixture.CreateMany<string>(10).ToArray();
		var bufferSize = Encoding.Unicode.GetByteCount(uniqueArray.First()) + bufferSizeDefect;
		var strArray = Enumerable.Repeat(uniqueArray, repeats)
			.SelectMany(x =>x).ToArray();
		var buffer = Encoding.Unicode.GetBytes(string.Join(" ", strArray));
		WithOpenStream(buffer);

		Assert.That(async () => await Subject.GetUniqueWordsAsync(filePath, bufferSize, Token), 
			Throws.Exception.TypeOf<InternalBufferOverflowException>());
	}

	[Test]
	[InlineAutoData(2)]
	[InlineAutoData(10)]
	public async Task ReturnResult(long bufferSizeDefect, string filePath, byte wordsCont)
	{
		var strArray = Fixture.CreateMany<string>(wordsCont).ToArray();
		var bufferSize =  Encoding.Unicode.GetByteCount(strArray.First()) + bufferSizeDefect;
		var buffer = Encoding.Unicode.GetBytes(string.Join(" ", strArray));
		WithOpenStream(buffer);
			
		var result = await Subject.GetUniqueWordsAsync(filePath, bufferSize, Token);

		Assert.That(result.Count, Is.EqualTo(strArray.Length));
		Assert.That(result.Values, Is.All.EqualTo(1));
	}
}