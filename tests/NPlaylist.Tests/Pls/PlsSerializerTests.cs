using System;
using System.Text;
using FluentAssertions;
using NPlaylist.Pls;
using Xunit;

namespace NPlaylist.Tests.PlsTests
{
    public class PlsSerializerTests
    {
        [Fact]
        public void Serialize_EmptyPlaylistCorrectSerialized_True()
        {
            var sb = new StringBuilder()
                .AppendLine("[playlist]")
                .AppendLine()
                .AppendLine("NumberOfEntries=0")
                .AppendLine("Version=2");
            var serializer = new PlsSerializer();

            var result = serializer.Serialize(new PlsPlaylist());

            sb.ToString().Should().Be(result);
        }

        [Fact]
        public void Serialize_VersionSerializedCorrect_True()
        {
            var sb = new StringBuilder()
                .AppendLine("[playlist]")
                .AppendLine()
                .AppendLine("NumberOfEntries=0")
                .AppendLine("Version=2");
            var serializer = new PlsSerializer();

            var result = serializer.Serialize(new PlsPlaylist()
            {
                Version = "2"
            });

            sb.ToString().Should().Be(result);
        }

        [Fact]
        public void Serialize_NullInput_ArgumentNullExceptionThrown()
        {
            var serializer = new PlsSerializer();
            Assert.Throws<ArgumentNullException>(() => serializer.Serialize(null));
        }

        [Fact]
        public void Serialize_VersionNotNumberBecomesTwo_True()
        {
            var sb = new StringBuilder()
                .AppendLine("[playlist]")
                .AppendLine()
                .AppendLine("NumberOfEntries=0")
                .AppendLine("Version=2");

            var serializer = new PlsSerializer();
            var result = serializer.Serialize(new PlsPlaylist()
            {
                Version = "ete"
            });

            sb.ToString().Should().Be(result);
        }

        [Fact]
        public void Serialize_ItemTitleSerializedCorrect_True()
        {
            var sb = new StringBuilder()
                .AppendLine("[playlist]")
                .AppendLine()
                .AppendLine("File1=test path")
                .AppendLine("Title1=test title")
                .AppendLine()
                .AppendLine("NumberOfEntries=1")
                .AppendLine("Version=2");

            var serializer = new PlsSerializer();
            var pls = new PlsPlaylist
            {
                Version = "2"
            };
            pls.Add(new PlsItem("test path")
            {
                Title = "test title"
            });

            var result = serializer.Serialize(pls);

            sb.ToString().Should().Be(result);
        }

        [Fact]
        public void Serialize_LengthNotNumberBecomesZero_True()
        {
            var sb = new StringBuilder()
                .AppendLine("[playlist]")
                .AppendLine()
                .AppendLine("File1=test path")
                .AppendLine("Length1=0")
                .AppendLine()
                .AppendLine("NumberOfEntries=1")
                .AppendLine("Version=2");

            var serializer = new PlsSerializer();
            var pls = new PlsPlaylist
            {
                Version = "2"
            };
            pls.Add(new PlsItem("test path")
            {
                Length = "sgsgsgsg"
            });

            var result = serializer.Serialize(pls);

            sb.ToString().Should().Be(result);
        }

        [Fact]
        public void Serialize_CorrectNumberOfEntries_True()
        {
            var pls = new PlsPlaylist
            {
                Version = "2"
            };
            pls.Add(new PlsItem("test path")
            {
                Title = "test title",
                Length = "sgsgsgsg"
            });
            pls.Add(new PlsItem("test path")
            {
                Title = "test title2",
                Length = "10"
            });
            var serializer = new PlsSerializer();

            var result = serializer.Serialize(pls);

            result.Should().Contain("NumberOfEntries=2");
        }
    }
}
