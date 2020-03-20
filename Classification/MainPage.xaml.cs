namespace Classification {
	using System.ComponentModel;
	using Xamarin.Forms;
	using Plugin.Media;
	using System.Diagnostics;

	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();

			new System.Action(async () => {
				var result = await CrossMedia.Current.Initialize();

				Debug.WriteLine("Initialized CrossMedia with result: " + (result ? "Success" : "Failed"));
			})();
		}

		async void Button_Clicked(System.Object sender, System.EventArgs e) {
			var options = new Plugin.Media.Abstractions.StoreCameraMediaOptions();

			options.DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Rear;

			this.image.IsVisible = false;

			using(var result = await CrossMedia.Current.TakePhotoAsync(options)) {
				if(result != null && result.GetStream() != null) {
					Debug.WriteLine(result.Path);
					Debug.WriteLine(result.AlbumPath);
					Debug.WriteLine(result.GetStream() == null);

					this.image.Source = ImageSource.FromStream(result.GetStream);

					this.image.IsVisible = true;
				}
			}
		}
	}
}
