﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using InquirerForAndroid.ViewModels;
using Xamarin.Forms;

namespace InquirerForAndroid.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SurveySelectorView : IScrollableView
    {
        public SurveySelectorView()
        {
            Debug.WriteLine(" ctor SurveySelectorView");
            InitializeComponent();
            Globals.CurrentViewModel = ViewModel;
        }

        public SurveySelectorViewModel ViewModel => BindingContext as SurveySelectorViewModel;

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
        }

        public bool IsScrolled { get; set; }

        private void SurveySelectorView_OnSizeChanged(object sender, EventArgs e)
        {
            IsScrolled = true;
        }
    }
}