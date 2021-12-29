using Metro.Models;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Metro.Tests
{
    public class MetroTests
    {
        [Fact]
        public void When_I_select_a_no_color_train()
        {
            //Arrange
            const string expectedResult = "A->B->C->D->E->F";
            const string NO_COLOR_TRAIN_NETWORK = "{ \"train\": { \"color\": \"None\" }, \"stations\": [ { \"name\": \"A\", \"color\": \"None\", \"type\": \"Initial\", \"nextStations\": [ \"B\" ] }, { \"name\": \"B\", \"color\": \"None\", \"nextStations\": [ \"C\" ] }, { \"name\": \"C\", \"color\": \"None\", \"nextStations\": [ \"D\", \"G\" ] }, { \"name\": \"D\", \"color\": \"None\", \"nextStations\": [ \"E\" ] }, { \"name\": \"E\", \"color\": \"None\", \"nextStations\": [ \"F\" ] }, { \"name\": \"F\", \"color\": \"None\", \"type\": \"Final\", \"nextStations\": [] }, { \"name\": \"G\", \"color\": \"Green\", \"nextStations\": [ \"H\" ] }, { \"name\": \"H\", \"color\": \"Red\", \"nextStations\": [ \"I\" ] }, { \"name\": \"I\", \"color\": \"Green\", \"nextStations\": [ \"F\" ] } ]}";

            //Act
            var sut = new MetroManager(NO_COLOR_TRAIN_NETWORK);
            var resultRoutes = sut.GetShortestRoutes();
            var resultRoute = string.Join("->", resultRoutes.FirstOrDefault().Select(s => s.Name));
            //Assert
            Assert.Equal(expectedResult, resultRoute);

        }

        [Fact]
        public void When_I_select_a_red_train()
        {
            //Arrange
            const string firstExpectedResult = "A->B->C->H->F";
            const string NO_COLOR_TRAIN_NETWORK = "{ \"train\": { \"color\": \"Red\" }, \"stations\": [ { \"name\": \"A\", \"color\": \"None\", \"type\": \"Initial\", \"nextStations\": [ \"B\" ] }, { \"name\": \"B\", \"color\": \"None\", \"nextStations\": [ \"C\" ] }, { \"name\": \"C\", \"color\": \"None\", \"nextStations\": [ \"D\", \"G\" ] }, { \"name\": \"D\", \"color\": \"None\", \"nextStations\": [ \"E\" ] }, { \"name\": \"E\", \"color\": \"None\", \"nextStations\": [ \"F\" ] }, { \"name\": \"F\", \"color\": \"None\", \"type\": \"Final\", \"nextStations\": [] }, { \"name\": \"G\", \"color\": \"Green\", \"nextStations\": [ \"H\" ] }, { \"name\": \"H\", \"color\": \"Red\", \"nextStations\": [ \"I\" ] }, { \"name\": \"I\", \"color\": \"Green\", \"nextStations\": [ \"F\" ] } ]}";

            //Act
            var sut = new MetroManager(NO_COLOR_TRAIN_NETWORK);
            var resultRoutes = sut.GetShortestRoutes();
            var resultRoute = string.Join("->", resultRoutes.FirstOrDefault().Select(s => s.Name));
            //Assert
            Assert.Equal(firstExpectedResult, resultRoute);
        }

        [Fact]
        public void When_I_select_a_green_train()
        {
            //Arrange
            const string firstExpectedResult = "A->B->C->D->E->F";
            const string secondExpectedResult = "A->B->C->G->I->F";
            const string NO_COLOR_TRAIN_NETWORK = "{ \"train\": { \"color\": \"Green\" }, \"stations\": [ { \"name\": \"A\", \"color\": \"None\", \"type\": \"Initial\", \"nextStations\": [ \"B\" ] }, { \"name\": \"B\", \"color\": \"None\", \"nextStations\": [ \"C\" ] }, { \"name\": \"C\", \"color\": \"None\", \"nextStations\": [ \"D\", \"G\" ] }, { \"name\": \"D\", \"color\": \"None\", \"nextStations\": [ \"E\" ] }, { \"name\": \"E\", \"color\": \"None\", \"nextStations\": [ \"F\" ] }, { \"name\": \"F\", \"color\": \"None\", \"type\": \"Final\", \"nextStations\": [] }, { \"name\": \"G\", \"color\": \"Green\", \"nextStations\": [ \"H\" ] }, { \"name\": \"H\", \"color\": \"Red\", \"nextStations\": [ \"I\" ] }, { \"name\": \"I\", \"color\": \"Green\", \"nextStations\": [ \"F\" ] } ]}";

            //Act
            var sut = new MetroManager(NO_COLOR_TRAIN_NETWORK);
            var resultRoutes = sut.GetShortestRoutes();
            var firstResultRoute = string.Join("->", resultRoutes.FirstOrDefault().Select(s => s.Name));
            var secondResultRoute = string.Join("->", resultRoutes.LastOrDefault().Select(s => s.Name));
            //Assert
            Assert.Equal(firstExpectedResult, firstResultRoute);
            Assert.Equal(secondExpectedResult, secondResultRoute);

        }

    }
}
