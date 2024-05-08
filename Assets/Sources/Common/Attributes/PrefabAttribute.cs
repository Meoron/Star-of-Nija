using System;

namespace Sources.Common.Attributes {
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PrefabAttribute : Attribute {
		public readonly string Path;
 
		public PrefabAttribute(string path) {
			Path = path;
		}
	}
}