using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MyCoachTests.ViewModel
{
    public abstract class ViewModelTestBase : DataInterfaceTestBase
    {
        protected List<string> PropertyChangedEvents;

        protected new void Initialize()
        {
            this.PropertyChangedEvents = new List<string>();
            base.Initialize();
        }

        protected void CleanUp()
        {
            base.CleanupTestBase();
        }

        protected virtual void OnSutPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChangedEvents.Add(e.PropertyName);
        }

        protected virtual void AssertPropertyChangedInvokation(int count)
        {
            Assert.AreEqual(count, this.PropertyChangedEvents.Count());
        }

        protected virtual void AssertPropertyChangedInvokation(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                Assert.IsTrue(this.PropertyChangedEvents.Contains(propertyName));
            }
        }

        protected virtual void AssertPropertyChangedInvokation(int count, params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                Assert.AreEqual(count, this.PropertyChangedEvents.Count(e => e == propertyName));
            }
        }

        protected virtual void AssertPropertyChangedInvokation(int count, string propertyName,  params int[] positions)
        {
            Assert.AreEqual(count, this.PropertyChangedEvents.Count);
            foreach (var position in positions)
            {
                Assert.AreEqual(propertyName, this.PropertyChangedEvents[position]);
            }
        }
    }
}
