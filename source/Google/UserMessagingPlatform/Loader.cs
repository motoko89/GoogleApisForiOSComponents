using System;
namespace Google.UserMessagingPlatform {
	public class Loader {
		static Loader ()
		{
		}

		public static void ForceLoad () { }
	}
}

namespace ApiDefinition {
	partial class Messaging {
		static Messaging ()
		{
			Google.UserMessagingPlatform.Loader.ForceLoad ();
		}
	}
}
