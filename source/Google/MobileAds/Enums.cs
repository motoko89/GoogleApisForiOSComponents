using System;
using System.Runtime.InteropServices;

using CoreGraphics;
using Foundation;
using ObjCRuntime;

namespace Google.MobileAds
{
	public enum AdLoaderAdType
	{
		// extern NSString *const kGADAdLoaderAdTypeNativeAppInstall;
		[Field ("kGADAdLoaderAdTypeNativeAppInstall", "__Internal")]
		NativeAppInstall,

		// extern NSString *const kGADAdLoaderAdTypeNativeContent;
		[Field ("kGADAdLoaderAdTypeNativeContent", "__Internal")]
		NativeContent,

		// extern NSString *const kGADAdLoaderAdTypeNativeCustomTemplate;
		[Field ("kGADAdLoaderAdTypeNativeCustomTemplate", "__Internal")]
		NativeCustomTemplate,

		// extern NSString *const kGADAdLoaderAdTypeDFPBanner;
		[Field ("kGADAdLoaderAdTypeDFPBanner", "__Internal")]
		DfpBanner,

		// AD_EXTERN GADAdLoaderAdType const kGADAdLoaderAdTypeUnifiedNative;
		[Field ("kGADAdLoaderAdTypeUnifiedNative", "__Internal")]
		UnifiedNative
	}

	[Native]
	public enum AdValuePrecision : long {
		Unknown = 0,
		Estimated = 1,
		PublisherProvided = 2,
		Precise = 3
	}

	[Native]
	public enum AdapterInitializationState : long {
		NotReady = 0,
		Ready = 1
	}
}
