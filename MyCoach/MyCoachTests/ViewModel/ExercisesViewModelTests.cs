using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyCoach.DataHandling;
using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.ViewModel;
using MyCoach.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace MyCoachTests.ViewModel
{
    [TestClass]
    public class ExercisesViewModelTests
    {
        IDataManager dataManager;
        IMessageBoxService messageBoxService;
        ExercisesViewModel sut;
        List<string> propertyChangedEvents;

        [TestInitialize]
        public void Init()
        {
            this.dataManager = Mock.Of<IDataManager>(manager =>
                manager.GetDataTransferObjects<Category>() == TestDtos.Categories &&
                manager.SetDataTransferObjects<Category>(It.IsAny<ObservableCollection<Category>>()) == true &&
                manager.GetDataTransferObjects<Exercise>() == TestDtos.Exercises &&
                manager.SetDataTransferObjects<Exercise>(It.IsAny<ObservableCollection<Exercise>>()) == true);
            this.messageBoxService = Mock.Of<IMessageBoxService>(service =>
                service.ShowMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>()) == MessageBoxResult.Yes);
            DataInterface.SetDataManager(dataManager);
            this.sut = new ExercisesViewModel(this.messageBoxService);
            this.propertyChangedEvents = new List<string>();
            this.sut.PropertyChanged +=
                (object sender, PropertyChangedEventArgs e) => { this.propertyChangedEvents.Add(e.PropertyName); };
        }

        [TestMethod]
        public void Construction_HappyPath_LoadsBuffersAndHasNoUnsavedChanges()
        {
            Assert.IsNotNull(this.sut.Categories);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Categories, TestDtos.Categories));
            Assert.IsFalse(this.sut.HasUnsavedCategories);

            Assert.IsNotNull(this.sut.Exercises);
            Assert.IsTrue(DtoUtilities.AreEqual(sut.Exercises, TestDtos.Exercises));
            Assert.IsFalse(this.sut.HasUnsavedExercises);
        }

        [TestMethod]
        [DataRow("CategoryWarmUpActive")]
        [DataRow("CategoryWarmUpName")]
        [DataRow("Category1Active")]
        [DataRow("Category1Name")]
        [DataRow("Category2Active")]
        [DataRow("Category2Name")]
        [DataRow("Category3Active")]
        [DataRow("Category3Name")]
        [DataRow("Category4Active")]
        [DataRow("Category4Name")]
        [DataRow("Category5Active")]
        [DataRow("Category5Name")]
        [DataRow("Category6Active")]
        [DataRow("Category6Name")]
        [DataRow("Category7Active")]
        [DataRow("Category7Name")]
        [DataRow("Category8Active")]
        [DataRow("Category8Name")]
        [DataRow("CategoryCoolDownActive")]
        [DataRow("CategoryCoolDownName")]
        public void Property_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue(string propertyName)
        {
            switch (this.sut.GetType().GetProperty(propertyName).GetValue(this.sut, null))
            {
                case bool boolProperty:
                    this.sut.GetType().GetProperty(propertyName).SetValue(sut, !boolProperty);
                    break;
                case string stringProperty:
                    this.sut.GetType().GetProperty(propertyName).SetValue(sut, string.Concat(stringProperty, "SomeAddition"));
                    break;
                default:
                    throw new NotImplementedException();
            }

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], propertyName);
            Assert.IsTrue(this.sut.HasUnsavedCategories);
        }
    }
}
