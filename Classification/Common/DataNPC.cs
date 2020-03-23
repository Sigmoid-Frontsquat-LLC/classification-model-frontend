namespace Classification {
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;

	public class DataNPC : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		private Dictionary<string, object> storage = new Dictionary<string, object>();

		protected virtual bool SetProperty<T>(T value, [CallerMemberName] string name = null) {
			if(!this.storage.ContainsKey(name)) {
				this.storage.Add(name, value);

				this.OnPropertyChanged(name);

				return true;
			}

			try {
				var current = (T)this.storage[name];

				if(object.Equals(current, value)) return false;

				this.storage[name] = value;

				this.OnPropertyChanged(name);

				return true;
			} catch {
				return false;
			}
		}

		protected virtual T GetProperty<T>([CallerMemberName] string name = null, T @default = default(T)) {
			if(this.storage.ContainsKey(name)) return (T)this.storage[name];

			return @default;
		}

		protected virtual void OnPropertyChanged(string name) {
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
}
