using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.Model.DataTransferObjects;
using MyExtensions.IEnumerable;

namespace MyCoachTests.DataHandling.DataManager
{
    [TestClass]
    public class DataManagerBaseTests
    {
        private TestDataManger sut;
        private XmlFileReaderWriterMock readerWriterMock;

        [TestInitialize]
        public void Init()
        {
            this.readerWriterMock = new XmlFileReaderWriterMock();
            this.sut = new TestDataManger(true, this.readerWriterMock);            
        }

        [TestMethod]
        public void Initialization_InitialLoadingSucceedes_NewDataManagerCreated()
        {
            Assert.IsInstanceOfType(this.sut, typeof(DataManagerBase));
        }

        [TestMethod]
        public void Initialization_InitialLoadingFailes_BufferInitializedWithDefaultValues()
        {
            this.sut = new TestDataManger(false, this.readerWriterMock);

            this.AssertThatSutBufferHasDefaultValues();
        }

        [TestMethod]
        public void SetDefaults_HappyPath_SetsDefaultData()
        {
            this.sut.GetBuffer().Categories.Clear();
            this.sut.GetBuffer().Exercises.Clear();
            this.sut.GetBuffer().Settings.First().RepeatsRound1 = 111;
            this.sut.GetBuffer().TrainingSchedules.First().StartMonth = new DateTime(1989, 11, 14);
            this.sut.GetBuffer().TrainingScores.Clear();

            this.sut.SetDefaults<Category>();
            this.sut.SetDefaults<Exercise>();
            this.sut.SetDefaults<Settings>();
            this.sut.SetDefaults<TrainingSchedule>();
            this.sut.SetDefaults<Month>();

            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Categories, DefaultDtos.Categories));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Exercises, DefaultDtos.Exercises));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Settings, DefaultDtos.Settings));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().TrainingSchedules, DefaultDtos.TrainingSchedules));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().TrainingScores, DefaultDtos.TrainingScores));
        }

        [TestMethod]
        public void TryExportAndImportExerciseSet_HappyPath_ExercisesAreExportedAndReimportedCorrectly()
        {
            this.sut.GetBuffer().Categories = TestDtos.Categories;
            this.sut.GetBuffer().Exercises = TestDtos.Exercises;

            var successExport = this.sut.TryExportExerciseSet("path");
            var successImport = this.sut.TryImportExerciseSet("path");

            Assert.IsTrue(successExport);
            Assert.IsTrue(successImport);
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Categories, TestDtos.Categories));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Exercises, TestDtos.Exercises));
        }

        [TestMethod]
        public void TryExportExerciseSet_Exception_ErrorMessageIsSet()
        {
            // ToDo: Fehlerfälle erweitern
            this.readerWriterMock.Exception = new Exception();
            this.sut.TryExportExerciseSet("path");
            StringAssert.Equals(this.sut.ErrorMessageExerciseSetExport, Constants.ExportError);
        }

        [TestMethod]
        public void TryImportExerciseSet_Exception_ErrorMessageIsSet()
        {
            // ToDo: Fehlerfälle erweitern
            this.readerWriterMock.Exception = new Exception();
            this.sut.TryImportExerciseSet("path");
            StringAssert.Equals(this.sut.ErrorMessageExerciseSetImport, Constants.ImportError);
        }

        [TestMethod]
        public void TryExportAndImportTraining_HappyPath_ExercisesAreExportedAndReimportedCorrectly()
        {
            var training = new List<TrainingElement>();
            TestDtos.Exercises.ForEach(e => training.Add(new TrainingElement { Type = TrainingElementType.Exercise, Exercise = e }));

            var successExport = this.sut.TryExportTraining("path", training);
            var successImport = this.sut.TryImportTraining("path", out var trainingAfterReimport);

            Assert.IsTrue(successExport);
            Assert.IsTrue(successImport);
            Assert.IsTrue(Utilities.AreEqual(training.Select(t => t.Exercise).ToList(), trainingAfterReimport.Select(t => t.Exercise).ToList()));
        }

        [TestMethod]
        public void TryExportTraining_Exception_ErrorMessageIsSet()
        {
            var training = new List<TrainingElement>();
            TestDtos.Exercises.ForEach(e => training.Add(new TrainingElement { Type = TrainingElementType.Exercise, Exercise = e }));

            this.readerWriterMock.Exception = new Exception();
            this.sut.TryExportTraining("path", training);
            StringAssert.Equals(this.sut.ErrorMessageTrainingExport, Constants.ExportError);
        }

        [TestMethod]
        public void TryImportTraining_Exception_ErrorMessageIsSet()
        {
            this.readerWriterMock.Exception = new Exception();
            this.sut.TryImportTraining("path", out var _);
            StringAssert.Equals(this.sut.ErrorMessageExerciseSetImport, Constants.ImportError);
        }

        private void AssertThatSutBufferHasDefaultValues()
        {
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Categories, DefaultDtos.Categories));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Exercises, DefaultDtos.Exercises));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().Settings, DefaultDtos.Settings));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().TrainingSchedules, DefaultDtos.TrainingSchedules));
            Assert.IsTrue(Utilities.AreEqual(this.sut.GetBuffer().TrainingScores, DefaultDtos.TrainingScores));
        }
    }

    public class TestDataManger : DataManagerBase
    {
        private readonly bool initialLoadingSuccess;

        public TestDataManger(bool initialLoadingSuccess, IXmlFileReaderWriter xmlFileReaderWriterMock) : base (xmlFileReaderWriterMock)
        {
            this.initialLoadingSuccess = initialLoadingSuccess;
        }

        public DtoCollection GetBuffer()
        {
            return this.Buffer;
        }

        protected override bool TryInitialLoading()
        {
            return this.initialLoadingSuccess;
        }
    }
}
