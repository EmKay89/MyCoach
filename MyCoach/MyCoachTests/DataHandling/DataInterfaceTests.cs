﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCoachTests.DataHandling
{
    [TestClass]
    public class DataInterfaceTests
    {
        private IDataManager dataManager;
        private ObservableCollection<Exercise> exerciseList;
        private DataInterface dataInterface;
        private List<TrainingElement> exercises;

        [TestInitialize]
        public void Init()
        {
            this.exerciseList = new ObservableCollection<Exercise>();
            this.dataManager = Mock.Of<IDataManager>(manager => manager.TryExportExerciseSet("path") == true &&
                                                                manager.TryImportExerciseSet("path") == true &&
                                                                manager.TryExportTraining("path", It.IsAny<List<TrainingElement>>()) == true &&
                                                                manager.TryImportTraining("path", out exercises) == true &&
                                                                manager.GetData<Exercise>() == this.exerciseList &&
                                                                manager.SaveData<Exercise>() == true); 
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
        public void ExportTraining_CallsTryExportTrainingOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.ExportTraining("path", new List<TrainingElement>()));
        }

        [TestMethod]
        public void ImportTraining_CallsTryImportTrainingOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.ImportTraining("path", out _));
        }

        [TestMethod]
        public void GetDataTransferObjects_CallsGetDataTransferObjectsOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.GetData<Exercise>() == this.exerciseList);
        }

        [TestMethod]
        public void SetDefaults_CallsSetDefaultsOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            this.dataInterface.SetDefaults<Exercise>();

            Mock.Get(this.dataManager).Verify((m) => m.SetDefaults<Exercise>(), Times.Once);
        }

        [TestMethod]
        public void SetDataTransferObjects_CallsGetDataTransferObjectsOfDataManager()
        {
            this.dataInterface = DataInterface.GetInstance();
            DataInterface.SetDataManager(dataManager);

            Assert.IsTrue(this.dataInterface.SaveData<Exercise>());
        }
    }
}
