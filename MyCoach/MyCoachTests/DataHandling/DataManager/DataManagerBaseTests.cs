using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoachTests;

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
            this.sut.GetBuffer().Settings.Clear();
            this.sut.GetBuffer().TrainingSchedules.Clear();
            this.sut.GetBuffer().TrainingScores.Clear();

            this.sut.SetDefaults<Category>();
            this.sut.SetDefaults<Exercise>();
            this.sut.SetDefaults<Settings>();
            this.sut.SetDefaults<TrainingSchedule>();
            this.sut.SetDefaults<Month>();

            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Categories, DefaultDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Exercises, DefaultDtos.Exercises));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Settings, DefaultDtos.Settings));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().TrainingSchedules, DefaultDtos.TrainingSchedules));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().TrainingScores, DefaultDtos.TrainingScores));
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
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Categories, TestDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Exercises, TestDtos.Exercises));
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
               
        private void AssertThatSutBufferHasDefaultValues()
        {
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Categories, DefaultDtos.Categories));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Exercises, DefaultDtos.Exercises));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().Settings, DefaultDtos.Settings));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().TrainingSchedules, DefaultDtos.TrainingSchedules));
            Assert.IsTrue(DtoUtilities.AreEqual(this.sut.GetBuffer().TrainingScores, DefaultDtos.TrainingScores));
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
