﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;

namespace MyCoachTests.DataHandling.DataManager
{
    [TestClass]
    public class XmlFileDataManagerTests
    {
        private XmlFileDataManager sut;
        private XmlFileReaderWriterMock readerWriterMock;

        [TestInitialize]
        public void Init()
        {
            this.readerWriterMock = new XmlFileReaderWriterMock();
            this.readerWriterMock.SetUpInnerXml(TestDtos.Collection);
            this.sut = new XmlFileDataManager(this.readerWriterMock);
        }

        [TestMethod]
        public void GetDataTransferObjects_AfterSuccessfullInitialization_ReturnsCorrectDataLoadedFromFile()
        {
            var loadedCategories = this.sut.GetData<Category>();
            var loadedExercises = this.sut.GetData<Exercise>();
            var loadedSettings = this.sut.GetData<Settings>();
            var loadedTrainingSchedules = this.sut.GetData<TrainingSchedule>();
            var loadedTrainingScores = this.sut.GetData<Month>();

            Assert.IsTrue(DtoUtilities.AreEqual(loadedCategories, TestDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedExercises, TestDtos.Exercises));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedSettings, TestDtos.Settings));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedTrainingSchedules, TestDtos.TrainingSchedules));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedTrainingScores, TestDtos.TrainingScores));
        }

        [TestMethod]
        public void GetDataTransferObjects_AfterUnsuccessfullInitialization_ReturnsDefaultData()
        {
            this.readerWriterMock.Exception = new Exception();
            this.sut = new XmlFileDataManager(this.readerWriterMock);
            
            var loadedCategories = this.sut.GetData<Category>();
            var loadedExercises = this.sut.GetData<Exercise>();
            var loadedSettings = this.sut.GetData<Settings>();
            var loadedTrainingSchedules = this.sut.GetData<TrainingSchedule>();
            var loadedTrainingScores = this.sut.GetData<Month>();
            Assert.IsTrue(DtoUtilities.AreEqual(loadedCategories, DefaultDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedExercises, DefaultDtos.Exercises));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedSettings, DefaultDtos.Settings));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedTrainingSchedules, DefaultDtos.TrainingSchedules));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedTrainingScores, DefaultDtos.TrainingScores));
        }

        [TestMethod]
        public void SaveData_HappyPath_SavesAllBufferDataToFile()
        {
            var innerXmlBeforeSaving = this.readerWriterMock.InnerXml;
            this.readerWriterMock.InnerXml = string.Empty;

            this.sut.SaveData<Category>();

            Assert.IsTrue(innerXmlBeforeSaving == readerWriterMock.InnerXml);
        }

        [TestMethod]
        public void TryImportExerciseSet_HappyPath_SavesBufferToFile()
        {
            var defaultExerciseSet = new ExerciseSet()
            {
                Exercises = DefaultDtos.Exercises,
                Categories = DefaultDtos.Categories
            };
            this.readerWriterMock.SetUpInnerXml(defaultExerciseSet);

            this.sut.TryImportExerciseSet("path");
            this.sut = new XmlFileDataManager(this.readerWriterMock); // Create new instance to load Buffer from file

            var loadedCategories = this.sut.GetData<Category>();
            var loadedExercises = this.sut.GetData<Exercise>();
            var loadedSettings = this.sut.GetData<Settings>();
            var loadedTrainingSchedules = this.sut.GetData<TrainingSchedule>();
            var loadedTrainingScores = this.sut.GetData<Month>();
            Assert.IsTrue(DtoUtilities.AreEqual(loadedCategories, DefaultDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedExercises, DefaultDtos.Exercises));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedSettings, TestDtos.Settings));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedTrainingSchedules, TestDtos.TrainingSchedules));
            Assert.IsTrue(DtoUtilities.AreEqual(loadedTrainingScores, TestDtos.TrainingScores));
        }

        [TestMethod]
        public void UnsuccessfullInitialization_SetsErrorText()
        {
            //this.readerWriterMock.Exception = new Exception();

            this.sut = new XmlFileDataManager(this.readerWriterMock);

            Assert.AreNotEqual(this.sut.ErrorMessageInitialLoading, string.Empty);
        }
    }
}
