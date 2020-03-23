namespace Classification {
	using Plugin.Media.Abstractions;

	public class CameraModel : DataNPC {
		public CameraModel() {
			this.Optimizers = new string[] {
				"sigmoid",
				"tanh"
			};

			this.Activators = new string[] {
				"adam"
			};

			this.MediaFile = null;
		}

		public MediaFile MediaFile {
			get {
				return this.GetProperty<MediaFile>();
			} set {
				this.SetProperty(value);
			}
		}

		public string[] Optimizers {
			get {
				return this.GetProperty<string[]>();
			} set {
				this.SetProperty(value);
			}
		}

		public string[] Activators {
			get {
				return this.GetProperty<string[]>();
			} set {
				this.SetProperty(value);
			}
		}
	}
}
