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

        [Test]
        public void TestValidation_Difficulty()
        {
            //Arrage
            TourLog log_1 = new TourLog();
            log_1.Difficulty = 5;

            TourLog log_2 = new TourLog();
            log_2.Difficulty = 11;

            TourLog log_3 = new TourLog();
            log_3.Difficulty = -1;

            //Act
            int expected = 0;      
            int _expected = 5;

            //Assert
            //5 - should be OK
            Assert.AreEqual(_expected, log_1.Difficulty);
            //11 - should fail
            Assert.AreEqual(expected, log_2.Difficulty);
            //-1 - should fail
            Assert.AreEqual(expected, log_3.Difficulty);
        }

        [Test]
        public void TestValidation_Time()
        {
            //Arrage
            TourLog log_1 = new TourLog();
            log_1.Time = 500;

            TourLog log_2 = new TourLog();
            log_2.Time = 10000000;

            //Act
            int expected = 0;
            int _expected = 500;

            //Assert
            Assert.AreEqual(_expected, log_1.Time);
            Assert.AreEqual(expected, log_2.Time);
        }

        [Test]
        public void TestValidation_Comment()
        {
            //Arrage
            TourLog log_1 = new TourLog();
            
            for (int i = 0; i < 148; i++)
            {
                log_1.Comment += "a";
            }

            TourLog log_2 = new TourLog();
            log_2.Comment = "Tour_Planner";

            //Act
            int expected = 0;
            string _expected = "Tour_Planner";

            //Assert
            Assert.AreEqual(expected, log_1.Comment.Length);
            Assert.AreEqual(_expected, log_2.Comment);
        }

        [Test]
        public void TestValidation_TotalTime()
        {
            //Arrage
            TourLog log_1 = new TourLog();
            log_1.TotalTime = 500;

            TourLog log_2 = new TourLog();
            log_2.TotalTime = 200000;

            //Act
            int expected = 0;
            int _expected = 500;

            //Assert
            Assert.AreEqual(_expected, log_1.TotalTime);
            Assert.AreEqual(expected, log_2.TotalTime);
        }

        [Test]
        public void TestValidation_Rating()
        {
            //Arrage
            TourLog log_1 = new TourLog();
            log_1.Rating = 5;

            TourLog log_2 = new TourLog();
            log_2.Rating = 20;

            //Act
            int expected = 0;
            int _expected = 5;

            //Assert
            Assert.AreEqual(_expected, log_1.Rating);
            Assert.AreEqual(expected, log_2.Rating);
        }

        [Test]
        public void TestValidation_Date()
        {
            //Arrage
            TourLog log_1 = new TourLog();
            log_1.Date = "12.12.2022";

            TourLog log_2 = new TourLog();
            log_2.Date = "123.123.123";

            //Act
            string expected = "12.12.2022";
            string _expected = null;

            //Assert
            Assert.AreEqual(expected, log_1.Date);
            Assert.AreEqual(_expected, log_2.Date);
        }

        [Test]
        public void TestValidation_Title()
        {
            //Arrage
            Tour tour= new Tour();

            for (int i = 0; i < 148; i++)
            {
                tour.Title += "a";
            }

            Tour tour_ = new Tour();
            tour_.Title = "Tour_Planner";

            //Act
            int expected = 0;
            string _expected = "Tour_Planner";

            //Assert
            Assert.AreEqual(expected, tour.Title.Length);
            Assert.AreEqual(_expected, tour_.Title);
        }

        [Test]
        public void TestValidation_Description()
        {
            //Arrage
            Tour tour = new Tour();

            for (int i = 0; i < 148; i++)
            {
                tour.Desciption += "a";
            }

            Tour tour_ = new Tour();
            tour_.Desciption = "Tour_Planner";

            //Act
            int expected = 0;
            string _expected = "Tour_Planner";

            //Assert
            Assert.AreEqual(expected, tour.Desciption.Length);
            Assert.AreEqual(_expected, tour_.Desciption);
        }

        [Test]
        public void TestValidation_Distance()
        {
            //Arrage
            Tour tour = new Tour();

            for (int i = 0; i < 27; i++)
            {
                tour.TourDistance += "1";
            }

            Tour tour_ = new Tour();
            tour_.TourDistance = "Tour_Planner";

            //Act
            int expected = 0;
            string _expected = "Tour_Planner";

            //Assert
            Assert.AreEqual(expected, tour.TourDistance.Length);
            Assert.AreEqual(_expected, tour_.TourDistance);
        }

        [Test]
        public void TestValidation_EstimatedTime()
        {
            //Arrage
            Tour tour = new Tour();

            for (int i = 0; i < 27; i++)
            {
                tour.EstimatedTime += "1";
            }

            Tour tour_ = new Tour();
            tour_.EstimatedTime = "Tour_Planner";

            //Act
            int expected = 0;
            string _expected = "Tour_Planner";

            //Assert
            Assert.AreEqual(expected, tour.EstimatedTime.Length);
            Assert.AreEqual(_expected, tour_.EstimatedTime);
        }
    }
}