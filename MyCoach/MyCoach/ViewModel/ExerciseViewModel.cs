﻿using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using MyCoach.ViewModel.ModelExtensions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace MyCoach.ViewModel
{
    public class ExerciseViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories;

        public ObservableCollection<Exercise> Exercises;

        public ExerciseViewModel()
        {
            this.Categories = DataInterface.GetInstance().GetDataTransferObjects<Category>();
            this.Exercises = DataInterface.GetInstance().GetDataTransferObjects<Exercise>();

            this.Categories.CollectionChanged += this.OnCategoriesChanged;
            this.Exercises.CollectionChanged += this.OnExercisesChanges;
        }

        public bool CategoryWarmUpActive
        {
            get => this.Categories.IsActive(ExerciseCategory.WarmUp);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.WarmUp) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.WarmUp, value);
                this.InvokePropertyChanged();
            }
        }

        public string CategoryWarmUpName
        {
            get => this.Categories.GetName(ExerciseCategory.WarmUp);

            set 
            {
                if (this.Categories.GetName(ExerciseCategory.WarmUp) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.WarmUp, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category1Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category1);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category1) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category1, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category1Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category1);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category1) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category1, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category2Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category2);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category2) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category2, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category2Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category2);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category2) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category2, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category3Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category3);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category3) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category3, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category3Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category3);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category3) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category3, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category4Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category4);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category4) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category4, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category4Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category4);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category4) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category4, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category5Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category5);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category5) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category5, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category5Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category5);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category5) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category5, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category6Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category6);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category6) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category6, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category6Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category6);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category6) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category6, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category7Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category7);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category7) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category7, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category7Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category7);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category7) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category7, value);
                this.InvokePropertyChanged();
            }
        }

        public bool Category8Active
        {
            get => this.Categories.IsActive(ExerciseCategory.Category8);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.Category8) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.Category8, value);
                this.InvokePropertyChanged();
            }
        }

        public string Category8Name
        {
            get => this.Categories.GetName(ExerciseCategory.Category8);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.Category8) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.Category8, value);
                this.InvokePropertyChanged();
            }
        }

        public bool CategoryCoolDownActive
        {
            get => this.Categories.IsActive(ExerciseCategory.CoolDown);

            set
            {
                if (this.Categories.IsActive(ExerciseCategory.CoolDown) == value)
                {
                    return;
                }

                this.Categories.SetActive(ExerciseCategory.CoolDown, value);
                this.InvokePropertyChanged();
            }
        }

        public string CategoryCoolDownName
        {
            get => this.Categories.GetName(ExerciseCategory.CoolDown);

            set
            {
                if (this.Categories.GetName(ExerciseCategory.CoolDown) == value)
                {
                    return;
                }

                this.Categories.SetName(ExerciseCategory.CoolDown, value);
                this.InvokePropertyChanged();
            }
        }

        private void OnCategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvokePropertyChanged("Categories");
        }

        private void OnExercisesChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvokePropertyChanged("Exercises");
        }
    }
}
