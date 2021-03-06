﻿using AutoMapper;
using DaysAway.Common;
using DaysAway.DataModel;
using DaysAway.ViewModel;
using DaysAway.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Hub Application template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace DaysAway
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class CommitmentGridView : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly MainViewModel defaultViewModel = App.Locator.Main;
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        public CommitmentGridView()
        {
            this.InitializeComponent();

            // Hub is only supported in Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

     
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

    
        public MainViewModel DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
    
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            await this.DefaultViewModel.Bind();           
          
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Save the unique state of the page here.
        }     

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #region Event Handlers

        private void GroupSection_ItemClick(object sender, ItemClickEventArgs e)
        {
            var vm = (CommitmentViewModel)e.ClickedItem;
            vm.NavigateToCommitment.Execute(vm);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = ((Control)sender).DataContext as MainViewModel;
            vm.NavigateToAddNewCommitment.Execute(null);
        }

        #endregion
    }
}
