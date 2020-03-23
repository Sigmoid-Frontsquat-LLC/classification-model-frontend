namespace Classification {
	using Xamarin.Forms;

	public partial class CameraView : ContentPage {
		private ActivityIndicator indicator = new ActivityIndicator();
		private View root = null;

		public CameraView() {
			InitializeComponent();

			this.BindingContext = new CameraViewModel();

			this.root = this.Content;

			this.indicator.IsRunning = true;
			this.indicator.VerticalOptions = LayoutOptions.Center;

			var context = (CameraViewModel)this.BindingContext;

			context.PropertyChanged += OnContextPropertyChanged;
		}

		private void OnContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			var context = (CameraViewModel)sender;

			if(string.Compare(e.PropertyName, nameof(context.HasImage)) == 0) {
				if(context.HasImage && context.Model.MediaFile != null) {
					this.photo.Source = ImageSource.FromStream(() => {
						return context.Model.MediaFile.GetStream();
					});
				}
			}

			if(string.Compare(e.PropertyName, nameof(context.IsBusy)) == 0) {
				if(context.IsBusy) {
					this.Content = this.indicator;
				} else {
					this.Content = root;
				}
			}
		}
	}
}
