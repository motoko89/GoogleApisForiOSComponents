using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Google.UserMessagingPlatform
{
	public partial class ConsentInformation
	{
		public  Task RequestConsentInfoUpdate(RequestParameters parameters)
		{
			var t = new TaskCompletionSource<bool>();
			ConsentInformationUpdateCompletionHandler handler = (error) => {
				if (error != null) {
					t.TrySetException (new NSErrorException(error));
				} else {
					t.TrySetResult (true);
				}
			};
			RequestConsentInfoUpdate (parameters, handler);
			return t.Task;
		}
	}

	public partial class ConsentForm
	{
		public static Task<ConsentForm> Load()
		{
			var t = new TaskCompletionSource<ConsentForm> ();
			ConsentFormLoadCompletionHandler handler = (form, error) => {
				if (error != null) {
					t.TrySetException (new NSErrorException (error));
				} else {
					t.TrySetResult (form);
				}
			};
			Load(handler);
			return t.Task;
		}

		public Task Present(UIViewController viewController)
		{
			var t = new TaskCompletionSource<bool> ();
			ConsentFormPresentCompletionHandler handler = (error) => {
				if (error != null) {
					t.TrySetException (new NSErrorException (error));
				} else {
					t.TrySetResult (true);
				}
			};
			Present(viewController, handler);
			return t.Task;
		}
	}
}
