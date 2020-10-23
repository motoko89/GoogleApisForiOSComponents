using System;
namespace KenZenSoft.NativePlaybackService {
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
			KenZenSoft.NativePlaybackService.Loader.ForceLoad ();
		}
	}
}
