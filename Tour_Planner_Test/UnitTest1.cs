using NUnit.Framework;
using System.Collections.Generic;
using Tour_Planner.DAL;
using Tour_Planner.Models;
using Tour_Planner.ViewModels;

namespace Tour_Planner_Test
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        { 
           
        }

        [Test]
        public void TestMockData_ShouldContainInitialList()
        {
            TourManager_Mock mock = new TourManager_Mock();
            List<Tour> mockData = mock.GetTourData();
            //Act
            int expected = 9;
            int actual = mockData.Count;

            //Assert
            Assert.AreEqual(expected, actual, "List should contain 9 tours!");
        }

        [Test]
        public void TestMockData_AddTour()
        {
            TourManager_Mock mock = new TourManager_Mock();
            List<Tour> mockData = mock.GetTourData();
            //Arrange
            mock.CreateTour(new Tour());

            //Act
            int expected = 10;
            int actual = mockData.Count;

            //Assert
            Assert.AreEqual(expected, actual, "List should contain 10 tours!");
        }

        
        [Test]
        public void TestMockData_DeleteTour()
        {
            TourManager_Mock mock = new TourManager_Mock();
            List<Tour> mockData = mock.GetTourData();
            //Arrange
            mock.DeleteTour(mockData[0]);

            //Act
            int expected = 8;
            int actual = mockData.Count;

            //Assert
            Assert.AreEqual(expected, actual, "List should contain 8 tours!");
        }

        [Test]
        public void TestMockData_DeleteAllTour()
        {
            TourManager_Mock mock = new TourManager_Mock();
            List<Tour> mockData = mock.GetTourData();
            //Arrange
            mock.DeleteAllTours();

            //Act
            int expected = 0;
            int actual = mockData.Count;

            //Assert
            Assert.AreEqual(expected, actual, "List should contain 0 tours!");
        }

        [Test]
        public void TestMockData_CreateLog()
        {
            TourManager_Mock mock = new TourManager_Mock();
            List<Tour> mockData = mock.GetTourData();
            //Arrange
            mock.CreateLog(mockData[0], new TourLog());

           
            //Act
            int expected = 2;
            int actual = mockData[0].Logs.Count;

            //Assert
            Assert.AreEqual(expected, actual, "Logs should be 2!");
        }

        [Test]
        public void TestMockData_DeleteLog()
        {
            TourManager_Mock mock = new TourManager_Mock();
            List<Tour> mockData = mock.GetTourData();
            //Arrange
            mock.DeleteTourLog(mockData[0], mockData[0].Logs[0]);


            //Act
            int expected = 0;
            int actual = mockData[0].Logs.Count;

            //Assert
            Assert.AreEqual(expected, actual, "Logs should be 0!");
        }
    }
}