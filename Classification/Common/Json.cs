namespace Common.Network {
	using System.Collections.Generic;
	using System.Text.Json;

	[System.Serializable]
	public class Json : System.Object {
		public static Builder Create() {
			return new Builder();
		}

		[System.Serializable]
		public sealed class Builder : System.Object {
			private Dictionary<string, object> body = new Dictionary<string, object>();

			public Builder Add(string key, object value) {
				this.body.Add(key, value);
				return this;
			}

			public string Build() {
				return JsonSerializer.Serialize(this.body);
			}
		}
	}
}
