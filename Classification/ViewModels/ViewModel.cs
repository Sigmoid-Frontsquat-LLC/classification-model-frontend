namespace Classification {
	public class ViewModel <T> : DataNPC where T : DataNPC, new() {
		public ViewModel() {
			this.Model = new T();
		}

		public T Model {
			get {
				return this.GetProperty<T>();
			} set {
				this.SetProperty(value);
			}
		}
	}
}
