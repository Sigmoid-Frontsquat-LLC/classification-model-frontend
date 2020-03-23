namespace Classification {
	using System.Threading.Tasks;
	using Xamarin.Forms;
	using Plugin.Media;
	using Plugin.Media.Abstractions;
	using System.Diagnostics;
	using Common.Network;
	using System.IO;
	using System.Net;
	using System.Net.Http;

	public class CameraViewModel : ViewModel<CameraModel> {
		public CameraViewModel() {
			this.HasImage = false;

			this.CmdTakePhoto = new Command(
				async () => { await this.TakePhoto(); });
			this.CmdSendPhoto = new Command(
				async () => { await this.SendPhoto(); },
				() => this.HasImage);

			this.Model.PropertyChanged += OnCamerModelPropertyChanged;
		}

		private void OnCamerModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			var model = (CameraModel)sender;

			if(string.Compare(e.PropertyName, nameof(model.MediaFile)) == 0) {
				this.HasImage = model.MediaFile != null;
			}
		}

		public bool HasImage {
			get {
				return this.GetProperty<bool>();
			} set {
				this.SetProperty(value);

				this.CmdSendPhoto?.ChangeCanExecute();
			}
		}

		public bool IsBusy {
			get {
				return this.GetProperty<bool>();
			} set {
				this.SetProperty(value);
			}
		}

		public Command CmdTakePhoto { get; }

		public Command CmdSendPhoto { get; }

		async Task TakePhoto() {
			var options = new StoreCameraMediaOptions();
			options.DefaultCamera = CameraDevice.Rear;
			Debug.WriteLine(options.SaveToAlbum);

			var result = await CrossMedia.Current.TakePhotoAsync(options);

			if(result == null || result.GetStream() == null) {
				return;
			}

			this.Model.MediaFile?.Dispose();

			this.Model.MediaFile = null;

			this.Model.MediaFile = result;

			Debug.WriteLine(this.Model.MediaFile?.Path);

		}

		async Task SendPhoto() {
			if(this.Model.MediaFile == null || this.Model.MediaFile.GetStream() == null) return;

			this.IsBusy = true;

			var builder = Json.Create();

			builder
				.Add("optimizer", "sigmoid")
				.Add("activation", "adam");

			using(var ms = new MemoryStream()) {
				var stream = this.Model.MediaFile.GetStream();

				await stream.CopyToAsync(ms);

				var bytes = ms.ToArray();

				string image = await Task.Run(() => { return System.Convert.ToBase64String(bytes); });

				builder.Add("image", image);
			}

			var json = builder.Build();

			System.Uri uri = new System.Uri("http://192.168.2.12:9000/classify");

			var request = WebRequest.Create(uri);
			request.Method = HttpMethod.Post.Method;
			request.ContentType = "application/json";

			var data = await Task.Run(() => { return System.Text.Encoding.UTF8.GetBytes(json); });

			request.ContentLength = data.Length;

			try {
				using(var stream = request.GetRequestStream()) {
					await stream.WriteAsync(data, 0, data.Length);
				}

				using(var response = await request.GetResponseAsync()) {
					using(var reader = new StreamReader(response.GetResponseStream())) {
						var result = await reader.ReadToEndAsync();

						Debug.WriteLine(result);
					}
				}
			} catch(WebException e) {
				Debug.WriteLine(e.GetType());
				Debug.WriteLine(e.Message);

				using(var reader = new StreamReader(e.Response.GetResponseStream())) {
					var output = await reader.ReadToEndAsync();

					Debug.WriteLine(output);
				}

				Debug.WriteLine(e.Source);
			}

			this.IsBusy = false;
		}
	}
}
