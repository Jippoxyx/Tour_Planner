using NUnit.Framework;
using Tour_Planner.Model;
using Tour_Planner.ViewModels;

namespace Tour_Planner_Test
{
    public class Tests
    {
        MainViewModel main {get; set;}
        public TourViewModel tour { get; set; }


        [SetUp]
        public void Setup()
        {
            tour = new TourViewModel();
            main = new MainViewModel(null, tour, null, null);

            tour.TourData.Add(new Tour());
            tour.TourData.Add(new Tour());
        }

        [Test]
        public void TestData_ShouldContainInitialList()
        {
            //Act
            int expected = 2;
            int actual = tour.TourData.Count;

            //Assert
            Assert.AreEqual(expected, actual, "List should contain 2 tours!");
        }

        [Test]
        public void TestData_ShouldAddTour()
        {
            //Arrange
            tour.TourData.Add(new Tour());

            //Act
            int expected = 3;
            int actual = tour.TourData.Count;

            //Assert
            Assert.AreEqual(expected, actual, "List should contain 3 tours!");
        }

        [Test]
        public void TestData_ShouldRemoveTour()
        {
            //Arrange
            tour.TourData.RemoveAt(0);

            //Act
            int expected = 1;
            int actual = tour.TourData.Count;

            //Assert
            Assert.AreEqual(expected, actual, "List should contain 1 tours!");
        }

    }
}