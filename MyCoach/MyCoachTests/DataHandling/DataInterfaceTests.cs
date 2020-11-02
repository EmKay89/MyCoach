﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace MyCoachTests.DataHandling
{
    [TestClass]
    public class DataInterfaceTests
    {
        private IDataManager dataManager;
        private List<Exercise> exerciseList;
        private DataInterface dataInterface;

        [TestInitialize]
        public void Init()
        {
            this.exerciseList = new List<Exercise>();
            this.dataManager = Mock.Of<IDataManager>(manager => manager.TryExportExerciseSet("path") == true &&
                                                                manager.TryImportExerciseSet("path") == true &&
                                                                manager.GetDataTransferObjects<Exercise>() == this.exerciseList &&
                                                                manager.SetDataTransferObjects<Exercise>(this.exerciseList) == true); 
        }

        [TestMethod]
        public void GetInstance_InstanceIsNull_NewInstanceReturned()
        {
            Assert.IsInstanceOfType(DataInterface.GetInstance(), typeof(DataInterface));
        }

        [TestMethod]
        public void ExportExerciseSet_CallsTryExportExerciseSetOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.ExportExerciseSet("path"));
        }

        [TestMethod]
        public void ImportExerciseSet_CallsTryImportExerciseSetOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.ImportExerciseSet("path"));
        }

        [TestMethod]
        public void GetDataTransferObjects_CallsGetDataTransferObjectsOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.GetDataTransferObjects<Exercise>() == this.exerciseList);
        }

        [TestMethod]
        public void SetDataTransferObjects_CallsGetDataTransferObjectsOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.SetDataTransferObjects<Exercise>(this.exerciseList));
        }
    }
}
