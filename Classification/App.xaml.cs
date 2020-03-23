namespace Classification {
	using Xamarin.Forms;

	public partial class App : Application {
		public App() {
			InitializeComponent();

			if(DesignMode.IsDesignModeEnabled) return;

			MainPage = new AppShell();
		}

		protected override void OnStart() {
		}

		protected override void OnSleep() {
		}

		protected override void OnResume() {
		}
	}
}
