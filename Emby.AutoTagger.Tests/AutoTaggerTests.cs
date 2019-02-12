using Emby.AutoTagger.Domain;
using Emby.AutoTagger.Factories;
using MediaBrowser.Controller.Entities;
using NUnit.Framework;

namespace Emby.AutoTagger.Tests
{
    public class AutoTaggerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Loading_json_works()
        {
            // arrange
            TagFactory target = new TagFactory(new Serializer());

            // act
            var actual = target.CreateTagRuleset();

            // assert
            Assert.That(actual, Is.Not.Null);
        }

        [Test, TestCaseSource("RuleCases")]
        public void Test_rules(string name, Video subject, string expression, string field, bool expected)
        {
            // arrange
            var target = new TagRule
            {
                Expression = expression,
                Field = field,
                Name = "booh"
            };

            // act
            bool actual = target.IsValidFor(subject, new ConsoleLogger());

            // assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        // item - expression - field - negative - outcome
        private static object[] RuleCases =
        {
            new object[] {"NOKIDS", new Video {Tags = new[] {"NOKIDS"}}, "NOKIDS", "Tags", true},

            new object[] {"3", new Video {InheritedParentalRatingValue = 3}, "^[1-5]$", "InheritedParentalRatingValue", true},
            new object[] {"6", new Video {InheritedParentalRatingValue = 6}, "^[1-5]$", "InheritedParentalRatingValue", false},

            new object[] {"PG-13", new Video {OfficialRating = "PG-13"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", false},
            new object[] {"PG-13 For blablabla", new Video {OfficialRating = "PG-13 For blablabla"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", false},
            new object[] {"Rated PG-13", new Video {OfficialRating = "Rated PG-13"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", false},
            new object[] {"Rated QQQ", new Video {OfficialRating = "Rated QQQ"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", false},
            new object[] {"PG", new Video {OfficialRating = "PG"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", true},
            new object[] {"Rated PG", new Video {OfficialRating = "Rated PG"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", true},
            new object[] {"Rated PG for XyZ", new Video {OfficialRating = "Rated PG"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", true},
            new object[] {"Rated G", new Video {OfficialRating = "Rated G"}, @"(Rated\s)?P?G($|\s)", "OfficialRating", true}
        };

    }
}