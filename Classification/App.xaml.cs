using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Classification {
	public partial class App : Application {
		public App() {
			InitializeComponent();

			if(DesignMode.IsDesignModeEnabled) return;

			MainPage = new AboutView();
		}

		protected override void OnStart() {
		}

		protected override void OnSleep() {
		}

		protected override void OnResume() {
		}
	}
}
