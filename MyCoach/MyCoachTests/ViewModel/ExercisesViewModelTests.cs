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
        [DataRow("CategoryWarmUpActive")]
        [DataRow("Category1Active")]
        [DataRow("Category2Active")]
        [DataRow("Category3Active")]
        [DataRow("Category4Active")]
        [DataRow("Category5Active")]
        [DataRow("Category6Active")]
        [DataRow("Category7Active")]
        [DataRow("Category8Active")]
        [DataRow("CategoryCoolDownActive")]
        public void BooleanCategoryProperty_Changes_RaisesPropertyChangedAndHasUnsavedChangesIsTrue(string propertyName)
        {
            var value = this.sut.GetType().GetProperty(propertyName).GetValue(sut, null) as bool?;

            this.sut.GetType().GetProperty(propertyName).SetValue(sut, !value);

            Assert.AreEqual(1, this.propertyChangedEvents.Count);
            Assert.AreEqual(this.propertyChangedEvents[0], propertyName);
            Assert.IsTrue(this.sut.HasUnsavedCategories);
        }
    }
}
