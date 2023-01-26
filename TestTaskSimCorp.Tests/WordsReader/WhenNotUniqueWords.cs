using System.Text;
using AutoFixture;
using AutoFixture.NUnit3;
using NUnit.Framework;

namespace TestTaskSimCorp.Tests.WordsReader;

public class WhenNotUniqueWords : UniqueWordsReaderFixture
{		
	[Test]
	[AutoData]
	public async Task ReturnDictionaryWithCounts(string filePath, byte repeats)
	{
		const long bufferSize = 100;
		var uniqueArray = Fixture.CreateMany<string>(10).ToArray();
		var strArray = Enumerable.Repeat(uniqueArray, repeats)
			.SelectMany(x =>x).ToArray();
		var buffer = Encoding.Unicode.GetBytes(string.Join(" ", strArray));
		WithOpenStream(buffer);

		var result = await Subject.GetUniqueWordsAsync(filePath, bufferSize, Token);

		Assert.That(result.Keys, Is.EquivalentTo(uniqueArray));
		Assert.That(result.Values, Is.All.EqualTo(repeats));
	}
}