using System;
using System.Text;
using NPlaylist.Pls;
using Xunit;

namespace NPlaylist.Tests.PlsTests
{
    public class PlsSerializerTests
    {
        [Fact]
        public void Serialize_EmptyPlaylistCorrectSerialized_True()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[playlist]");
            sb.AppendLine();
            sb.AppendLine($"NumberOfEntries={0}");
            sb.AppendLine($"Version=2");

            var serializer = new PlsSerializer();
            var result = serializer.Serialize(new PlsPlaylist());

            Assert.Equal(sb.ToString(),result);
        }

        [Fact]
        public void Serialize_VersionSerializedCorrect_True()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[playlist]");
            sb.AppendLine();
            sb.AppendLine($"NumberOfEntries={0}");
            sb.AppendLine($"Version=2");

            var serializer = new PlsSerializer();
            var result = serializer.Serialize(new PlsPlaylist()
            {
                Version = "2"
            });

            Assert.Equal(sb.ToString(),result);
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
            var sb = new StringBuilder();
            sb.AppendLine("[playlist]");
            sb.AppendLine();
            sb.AppendLine($"NumberOfEntries={0}");
            sb.AppendLine($"Version=2");

            var serializer = new PlsSerializer();
            var result = serializer.Serialize(new PlsPlaylist()
            {
                Version = "ete"
            });

            Assert.Equal(sb.ToString(), result);
        }

        [Fact]
        public void Serialize_ItemTitleSerializedCorrect_True()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[playlist]");
            sb.AppendLine();
            sb.AppendLine($"File1=test path");
            sb.AppendLine($"Title1=test title");
            sb.AppendLine();
            sb.AppendLine($"NumberOfEntries=1");
            sb.AppendLine($"Version=2");

            var serializer = new PlsSerializer();
            var pls = new PlsPlaylist
            {
                Version = "2"
            };
            pls.Add(new PlsPlaylistItem("test path")
            {
                Title = "test title"
            });

            var result = serializer.Serialize(pls);

            Assert.Equal(sb.ToString(),result);
        }

        [Fact]
        public void Serialize_LengthNotNumberBecomesZero_True()
        {
            var sb = new StringBuilder();
            sb.AppendLine("[playlist]");
            sb.AppendLine();
            sb.AppendLine($"File1=test path");
            sb.AppendLine($"Length1=0");
            sb.AppendLine();
            sb.AppendLine($"NumberOfEntries=1");
            sb.AppendLine($"Version=2");

            var serializer = new PlsSerializer();
            var pls = new PlsPlaylist
            {
                Version = "2"
            };
            pls.Add(new PlsPlaylistItem("test path")
            {
                Length = "sgsgsgsg"
            });

            var result = serializer.Serialize(pls);

            Assert.Equal(sb.ToString(),result);
        }

        [Fact]
        public void Serialize_CorrectNumberOfEntries_True()
        {
            var serializer = new PlsSerializer();
            var pls = new PlsPlaylist
            {
                Version = "2"
            };
            pls.Add(new PlsPlaylistItem("test path")
            {
                Title = "test title",
                Length = "sgsgsgsg"
            });
            pls.Add(new PlsPlaylistItem("test path")
            {
                Title = "test title2",
                Length = "10"
            });

            var result = serializer.Serialize(pls);

            Assert.Contains("NumberOfEntries=2", result);
        }
    }
}
