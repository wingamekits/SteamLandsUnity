using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using UnityApp = UnityPlayer.UnityApp;
using UnityBridge = WinRTBridge.WinRTBridge;

namespace SteamLands
{
	public partial class MainPage : PhoneApplicationPage
	{
		private bool _unityStartedLoading;

		// Constructor
		public MainPage()
		{
			var bridge = new UnityBridge();
			UnityApp.SetBridge(bridge);
			InitializeComponent();
			bridge.Control = DrawingSurfaceBackground;
		}

		private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
		{
			if (!_unityStartedLoading)
			{
				_unityStartedLoading = true;

				UnityApp.SetLoadedCallback(() => { Dispatcher.BeginInvoke(Unity_Loaded); });

				var content = Application.Current.Host.Content;
				var width = (int)Math.Floor(content.ActualWidth * content.ScaleFactor / 100.0 + 0.5);
				var height = (int)Math.Floor(content.ActualHeight * content.ScaleFactor / 100.0 + 0.5);

				UnityApp.SetNativeResolution(width, height);
				UnityApp.SetRenderResolution(width, height);
				UnityPlayer.UnityApp.SetOrientation((int)Orientation);

				DrawingSurfaceBackground.SetBackgroundContentProvider(UnityApp.GetBackgroundContentProvider());
				DrawingSurfaceBackground.SetBackgroundManipulationHandler(UnityApp.GetManipulationHandler());
			}
		}

		private void Unity_Loaded()
		{
		}

		private void PhoneApplicationPage_BackKeyPress(object sender, CancelEventArgs e)
		{
			e.Cancel = UnityApp.BackButtonPressed();
		}

		private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
		{
			UnityApp.SetOrientation((int)e.Orientation);
		}
	}
}