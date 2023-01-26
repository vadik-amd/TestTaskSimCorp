using System.Text;
using AutoFixture;
using AutoFixture.NUnit3;
using NUnit.Framework;

namespace TestTaskSimCorp.Tests.WordsReader;

public class WhenUniqueWords : UniqueWordsReaderFixture
{
	[Test]
	[InlineAutoData]
	public async Task ReturnDictionaryWithCounts(string filePath, byte wordsCont)
	{
		const long bufferSize = 1000;
		var strArray = Fixture.CreateMany<string>(wordsCont).ToArray();
		var buffer = Encoding.Unicode.GetBytes(string.Join(" ", strArray));
		WithOpenStream(buffer);

		var result = await Subject.GetUniqueWordsAsync(filePath, bufferSize, Token);

		Assert.That(result.Count, Is.EqualTo(strArray.Length));
		Assert.That(result.Values, Is.All.EqualTo(1));
	}
}