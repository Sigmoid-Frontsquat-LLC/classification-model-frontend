namespace Classification {
	using Xamarin.Forms;

	public partial class AboutView : ContentPage {
		public AboutView() {
			InitializeComponent();
		}

		void Button_Clicked_Kevin(System.Object sender, System.EventArgs e) {
			Xamarin.Essentials.Browser.OpenAsync("https://github.com/polymorphik");
		}

		void Button_Clicked_Roberto(System.Object sender, System.EventArgs e) {
			Xamarin.Essentials.Browser.OpenAsync("https://github.com/rber14");
		}

		void Button_Clicked_Austin(System.Object sender, System.EventArgs e) {
			Xamarin.Essentials.Browser.OpenAsync("https://github.com/austinwilson1224");
		}
	}
}
