using System;
using System.Collections.Generic;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using StoreKit;
using UIKit;

namespace Google.MobileAds {
	#region CustomLib
	// This is a custom class created by me and is not part of Google Admob lib
	// But it is necesary for this binding to work
	[Static]
	interface AdSizeCons {
		[Internal]
		[Field ("kGADAdSizeBanner", "__Internal")]
		IntPtr _Banner { get; }

		[Internal]
		[Field ("kGADAdSizeLargeBanner", "__Internal")]
		IntPtr _LargeBanner { get; }

		[Internal]
		[Field ("kGADAdSizeMediumRectangle", "__Internal")]
		IntPtr _MediumRectangle { get; }

		[Internal]
		[Field ("kGADAdSizeFullBanner", "__Internal")]
		IntPtr _FullBanner { get; }

		[Internal]
		[Field ("kGADAdSizeLeaderboard", "__Internal")]
		IntPtr _Leaderboard { get; }

		[Internal]
		[Field ("kGADAdSizeSkyscraper", "__Internal")]
		IntPtr _Skyscraper { get; }

		[Internal]
		[Field ("kGADAdSizeSmartBannerPortrait", "__Internal")]
		IntPtr _SmartBannerPortrait { get; }

		[Internal]
		[Field ("kGADAdSizeSmartBannerLandscape", "__Internal")]
		IntPtr _SmartBannerLandscape { get; }

		[Internal]
		[Field ("kGADAdSizeFluid", "__Internal")]
		IntPtr _Fluid { get; }

		[Internal]
		[Field ("kGADAdSizeInvalid", "__Internal")]
		IntPtr _Invalid { get; }
	}
	#endregion

	interface IRewardedAdDelegate { }

	// @protocol GADRewardedAdDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "GADRewardedAdDelegate")]
	interface RewardedAdDelegate {
		// @required -(void)rewardedAd:(GADRewardedAd * _Nonnull)rewardedAd userDidEarnReward:(GADAdReward * _Nonnull)reward;
		[Abstract]
		[EventArgs ("RewardedAdReward")]
		[EventName ("RewardEarned")]
		[Export ("rewardedAd:userDidEarnReward:")]
		void UserDidEarnReward (RewardedAd rewardedAd, AdReward reward);

		// @optional -(void)rewardedAd:(GADRewardedAd * _Nonnull)rewardedAd didFailToPresentWithError:(NSError * _Nonnull)error;
		[EventArgs ("RewardedAdError")]
		[EventName ("FailedToPresent")]
		[Export ("rewardedAd:didFailToPresentWithError:")]
		void DidFailToPresent (RewardedAd rewardedAd, NSError error);

		// @optional -(void)rewardedAdDidPresent:(GADRewardedAd * _Nonnull)rewardedAd;
		[EventArgs ("RewardedAd")]
		[EventName ("Presented")]
		[Export ("rewardedAdDidPresent:")]
		void DidPresent (RewardedAd rewardedAd);

		// @optional -(void)rewardedAdDidDismiss:(GADRewardedAd * _Nonnull)rewardedAd;
		[EventArgs ("RewardedAd")]
		[EventName ("Dismissed")]
		[Export ("rewardedAdDidDismiss:")]
		void DidDismiss (RewardedAd rewardedAd);
	}

	// @interface GADRewardedAd : NSObject
	[BaseType (typeof (NSObject),
		Name = "GADRewardedAd",
		Delegates = new [] { "AdMetadataDelegate" },
		Events = new [] { typeof (RewardedAdMetadataDelegate) })]
	interface RewardedAd {
		// -(instancetype _Nonnull)initWithAdUnitID:(NSString * _Nonnull)adUnitID;
		[Export ("initWithAdUnitID:")]
		IntPtr Constructor (string adUnitID);

		// -(void)loadRequest:(GADRequest * _Nullable)request completionHandler:(GADRewardedAdLoadCompletionHandler _Nullable)completionHandler;
		[Export ("loadRequest:completionHandler:")]
		void LoadRequest ([NullAllowed] Request request, [NullAllowed] RewardedAdLoadCompletionHandler completionHandler);

		// @property (readonly, nonatomic) NSString * _Nonnull adUnitID;
		[Export ("adUnitID")]
		string AdUnitID { get; }

		// @property (readonly, getter = isReady, nonatomic) BOOL ready;
		[Export ("ready")]
		bool IsReady { [Bind ("isReady")] get; }

		// @property (readonly, nonatomic) GADResponseInfo * _Nullable responseInfo;
		[NullAllowed, Export ("responseInfo")]
		ResponseInfo ResponseInfo { get; }

		// @property (readonly, nonatomic) GADAdReward * _Nullable reward;
		[NullAllowed, Export ("reward")]
		AdReward Reward { get; }

		// @property (nonatomic) GADServerSideVerificationOptions * _Nullable serverSideVerificationOptions;
		[NullAllowed, Export ("serverSideVerificationOptions", ArgumentSemantic.Assign)]
		ServerSideVerificationOptions ServerSideVerificationOptions { get; set; }

		// @property (readonly, nonatomic) NSDictionary<GADAdMetadataKey,id> * _Nullable adMetadata;
		[NullAllowed, Export ("adMetadata")]
		NSDictionary<NSString, NSObject> AdMetadata { get; }

		// @property (nonatomic, weak) id<GADRewardedAdMetadataDelegate> _Nullable adMetadataDelegate;
		[NullAllowed, Export ("adMetadataDelegate", ArgumentSemantic.Weak)]
		IRewardedAdMetadataDelegate AdMetadataDelegate { get; set; }

		// @property (copy, nonatomic) GADPaidEventHandler _Nullable paidEventHandler;
		[NullAllowed, Export ("paidEventHandler", ArgumentSemantic.Copy)]
		PaidEventHandler PaidEventHandler { get; set; }

		// -(BOOL)canPresentFromRootViewController:(UIViewController * _Nonnull)rootViewController error:(NSError * _Nullable * _Nullable)error;
		[Export ("canPresentFromRootViewController:error:")]
		bool CanPresent (UIViewController rootViewController, [NullAllowed] out NSError error);

		// -(void)presentFromRootViewController:(UIViewController * _Nonnull)viewController delegate:(id<GADRewardedAdDelegate> _Nonnull)delegate;
		[Export ("presentFromRootViewController:delegate:")]
		void Present (UIViewController viewController, IRewardedAdDelegate @delegate);
	}

	// @interface GADAdReward : NSObject
	[BaseType (typeof (NSObject), Name = "GADAdReward")]
	interface AdReward {
		// @property (readonly, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, nonatomic) NSDecimalNumber * _Nonnull amount;
		[Export ("amount")]
		NSDecimalNumber Amount { get; }

		// -(instancetype _Nonnull)initWithRewardType:(NSString * _Nonnull)rewardType rewardAmount:(NSDecimalNumber * _Nonnull)rewardAmount __attribute__((objc_designated_initializer));
		[Export ("initWithRewardType:rewardAmount:")]
		[DesignatedInitializer]
		IntPtr Constructor (string rewardType, NSDecimalNumber rewardAmount);
	}

	// @interface GADRequest : NSObject <NSCopying>
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "GADRequest")]
	interface Request : INSCopying {
		// +(instancetype _Nonnull)request;
		[Static]
		[Export ("request")]
		Request GetDefaultRequest ();

		// -(void)registerAdNetworkExtras:(id<GADAdNetworkExtras> _Nonnull)extras;
		[Export ("registerAdNetworkExtras:")]
		void RegisterAdNetworkExtras (IAdNetworkExtras extras);

		// -(id<GADAdNetworkExtras> _Nullable)adNetworkExtrasFor:(Class<GADAdNetworkExtras> _Nonnull)aClass;
		[Export ("adNetworkExtrasFor:")]
		[return: NullAllowed]
		IAdNetworkExtras AdNetworkExtrasFor ([NullAllowed] Class aClass);

		// -(void)removeAdNetworkExtrasFor:(Class<GADAdNetworkExtras> _Nonnull)aClass;
		[Export ("removeAdNetworkExtrasFor:")]
		void RemoveAdNetworkExtrasFor (Class aClass);

		// @property (nonatomic, weak) UIWindowScene * _Nullable scene __attribute__((availability(ios, introduced=13.0)));
		[Introduced (PlatformName.iOS, 13, 0)]
		[NullAllowed, Export ("scene", ArgumentSemantic.Weak)]
		UIWindowScene Scene { get; set; }

		// -(void)setLocationWithLatitude:(CGFloat)latitude longitude:(CGFloat)longitude accuracy:(CGFloat)accuracyInMeters;
		[Export ("setLocationWithLatitude:longitude:accuracy:")]
		void SetLocation (nfloat latitude, nfloat longitude, nfloat accuracyInMeters);

		// @property (copy, nonatomic) NSArray * _Nullable keywords;
		[NullAllowed, Export ("keywords", ArgumentSemantic.Copy)]
		string [] Keywords { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable contentURL;
		[NullAllowed, Export ("contentURL")]
		string ContentUrl { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable requestAgent;
		[NullAllowed, Export ("requestAgent")]
		string RequestAgent { get; set; }
	}

	// typedef void (^GADRewardedAdLoadCompletionHandler)(GADRequestError * _Nullable);
	delegate void RewardedAdLoadCompletionHandler ([NullAllowed] RequestError arg0);

	// @interface GADResponseInfo : NSObject
	[BaseType (typeof (NSObject), Name = "GADResponseInfo")]
	interface ResponseInfo {
		// @property (readonly, nonatomic) NSString * _Nullable responseIdentifier;
		[NullAllowed, Export ("responseIdentifier")]
		string ResponseIdentifier { get; }

		// @property (readonly, nonatomic) NSString * _Nullable adNetworkClassName;
		[NullAllowed, Export ("adNetworkClassName")]
		string AdNetworkClassName { get; }

		// @property (readonly, nonatomic) NSArray<GADAdNetworkResponseInfo *> * _Nonnull adNetworkInfoArray;
		[Export ("adNetworkInfoArray")]
		AdNetworkResponseInfo [] AdNetworkInfoArray { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,id> * _Nonnull dictionaryRepresentation;
		[Export ("dictionaryRepresentation")]
		NSDictionary<NSString, NSObject> DictionaryRepresentation { get; }
	}

	// @interface GADServerSideVerificationOptions : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "GADServerSideVerificationOptions")]
	interface ServerSideVerificationOptions : INSCopying {
		// @property (copy, nonatomic) NSString * _Nullable userIdentifier;
		[NullAllowed, Export ("userIdentifier")]
		string UserIdentifier { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable customRewardString;
		[NullAllowed, Export ("customRewardString")]
		string CustomRewardString { get; set; }
	}

	interface IRewardedAdMetadataDelegate { }

	// @protocol GADRewardedAdMetadataDelegate <NSObject>
	[Model (AutoGeneratedName = true)]
	[Protocol]
	[BaseType (typeof (NSObject), Name = "GADRewardedAdMetadataDelegate")]
	interface RewardedAdMetadataDelegate {
		// @optional -(void)rewardedAdMetadataDidChange:(GADRewardedAd * _Nonnull)rewardedAd;
		[EventArgs ("RewardedAdMetadataChanged")]
		[EventName ("Changed")]
		[Export ("rewardedAdMetadataDidChange:")]
		void DidChange (RewardedAd rewardedAd);
	}

	// typedef void (^GADPaidEventHandler)(GADAdValue * _Nonnull);
	delegate void PaidEventHandler (AdValue arg0);

	// @interface GADAdValue : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "GADAdValue")]
	interface AdValue : INSCopying {
		// @property (readonly, nonatomic) GADAdValuePrecision precision;
		[Export ("precision")]
		AdValuePrecision Precision { get; }

		// @property (readonly, nonatomic) NSDecimalNumber * _Nonnull value;
		[Export ("value")]
		NSDecimalNumber Value { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull currencyCode;
		[Export ("currencyCode")]
		string CurrencyCode { get; }
	}

	interface IAdNetworkExtras { }

	// @protocol GADAdNetworkExtras <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "GADAdNetworkExtras")]
	interface AdNetworkExtras {
	}

	[BaseType (typeof (NSError), Name = "GADRequestError")]
	interface RequestError {
	}

	// @interface GADAdNetworkResponseInfo : NSObject
	[BaseType (typeof (NSObject), Name = "GADAdNetworkResponseInfo")]
	interface AdNetworkResponseInfo {
		// @property (readonly, nonatomic) NSString * _Nonnull adNetworkClassName;
		[Export ("adNetworkClassName")]
		string AdNetworkClassName { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,id> * _Nonnull credentials;
		[Export ("credentials")]
		NSDictionary<NSString, NSObject> Credentials { get; }

		// @property (readonly, nonatomic) NSError * _Nullable error;
		[NullAllowed, Export ("error")]
		NSError Error { get; }

		// @property (readonly, nonatomic) NSTimeInterval latency;
		[Export ("latency")]
		double Latency { get; }

		// @property (readonly, nonatomic) NSDictionary<NSString *,id> * _Nonnull dictionaryRepresentation;
		[Export ("dictionaryRepresentation")]
		NSDictionary<NSString, NSObject> DictionaryRepresentation { get; }
	}

	// @interface GADInterstitial : NSObject
	[BaseType (typeof (NSObject),
		Name = "GADInterstitial",
		Delegates = new string [] { "Delegate" },
		Events = new Type [] { typeof (InterstitialDelegate) })]
	interface Interstitial {
		// -(instancetype _Nonnull)initWithAdUnitID:(NSString * _Nonnull)adUnitID __attribute__((objc_designated_initializer));
		[Export ("initWithAdUnitID:")]
		[DesignatedInitializer]
		IntPtr Constructor (string adUnitID);

		// @property (readonly, nonatomic) NSString * _Nullable adUnitID;
		[NullAllowed, Export ("adUnitID")]
		string AdUnitId { get; }

		// @property (nonatomic, weak) id<GADInterstitialDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IInterstitialDelegate Delegate { get; set; }

		// -(void)loadRequest:(GADRequest * _Nullable)request;
		[Export ("loadRequest:")]
		void LoadRequest ([NullAllowed] Request request);

		// @property (readonly, nonatomic) BOOL isReady;
		[Export ("isReady")]
		bool IsReady { get; }

		// @property (readonly, nonatomic) BOOL hasBeenUsed;
		[Export ("hasBeenUsed")]
		bool HasBeenUsed { get; }

		// @property (readonly, nonatomic) GADResponseInfo * _Nullable responseInfo;
		[NullAllowed, Export ("responseInfo")]
		ResponseInfo ResponseInfo { get; }

		// @property (copy, nonatomic) GADPaidEventHandler _Nullable paidEventHandler;
		[NullAllowed, Export ("paidEventHandler", ArgumentSemantic.Copy)]
		PaidEventHandler PaidEventHandler { get; set; }

		// -(void)presentFromRootViewController:(UIViewController * _Nonnull)rootViewController;
		[Export ("presentFromRootViewController:")]
		void Present (UIViewController rootViewController);

		// -(BOOL)canPresentFromRootViewController:(UIViewController * _Nonnull)rootViewController error:(NSError * _Nullable * _Nullable)error;
		[Export ("canPresentFromRootViewController:error:")]
		bool CanPresent (UIViewController rootViewController, [NullAllowed] out NSError error);
	}

	interface IInterstitialDelegate { }

	// @protocol GADInterstitialDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "GADInterstitialDelegate")]
	interface InterstitialDelegate {
		[EventArgs ("InterstitialE")]
		[EventName ("AdReceived")]
		// @optional -(void)interstitialDidReceiveAd:(GADInterstitial * _Nonnull)ad;
		[Export ("interstitialDidReceiveAd:")]
		void DidReceiveAd (Interstitial ad);

		[EventArgs ("InterstitialDidFailToReceiveAdWithError")]
		[EventName ("ReceiveAdFailed")]
		// @optional -(void)interstitial:(GADInterstitial * _Nonnull)ad didFailToReceiveAdWithError:(GADRequestError * _Nonnull)error;
		[Export ("interstitial:didFailToReceiveAdWithError:")]
		void DidFailToReceiveAd (Interstitial ad, RequestError error);

		[EventArgs ("InterstitialE")]
		// @optional -(void)interstitialWillPresentScreen:(GADInterstitial * _Nonnull)ad;
		[Export ("interstitialWillPresentScreen:")]
		void WillPresentScreen (Interstitial ad);

		[EventArgs ("InterstitialE")]
		[EventName ("FailedToPresentScreen")]
		// @optional -(void)interstitialDidFailToPresentScreen:(GADInterstitial * _Nonnull)ad;
		[Export ("interstitialDidFailToPresentScreen:")]
		void DidFailToPresentScreen (Interstitial ad);

		[EventArgs ("InterstitialE")]
		// @optional -(void)interstitialWillDismissScreen:(GADInterstitial * _Nonnull)ad;
		[Export ("interstitialWillDismissScreen:")]
		void WillDismissScreen (Interstitial ad);

		[EventArgs ("InterstitialE")]
		[EventName ("ScreenDismissed")]
		// @optional -(void)interstitialDidDismissScreen:(GADInterstitial * _Nonnull)ad;
		[Export ("interstitialDidDismissScreen:")]
		void DidDismissScreen (Interstitial ad);

		[EventArgs ("InterstitialE")]
		// @optional -(void)interstitialWillLeaveApplication:(GADInterstitial * _Nonnull)ad;
		[Export ("interstitialWillLeaveApplication:")]
		void WillLeaveApplication (Interstitial ad);
	}

	// @interface GADExtras : NSObject <GADAdNetworkExtras>
	[BaseType (typeof (NSObject), Name = "GADExtras")]
	interface Extras : AdNetworkExtras {
		// @property (copy, nonatomic) NSDictionary * _Nullable additionalParameters;
		[NullAllowed, Export ("additionalParameters", ArgumentSemantic.Copy)]
		NSDictionary AdditionalParameters { get; set; }
	}

	// @interface GADBannerView : UIView
	[BaseType (typeof (UIView),
		Name = "GADBannerView",
		Delegates = new string [] { "Delegate", "AdSizeDelegate" },
		Events = new Type [] { typeof (BannerViewDelegate), typeof (AdSizeDelegate) })]
	interface BannerView {
		// -(instancetype _Nonnull)initWithAdSize:(GADAdSize)adSize origin:(CGPoint)origin;
		[Export ("initWithAdSize:origin:")]
		IntPtr Constructor (AdSize adSize, CGPoint origin);

		// -(instancetype _Nonnull)initWithAdSize:(GADAdSize)adSize;
		[Export ("initWithAdSize:")]
		IntPtr Constructor (AdSize adSize);

		// @property (copy, nonatomic) NSString * _Nullable adUnitID;
		[NullAllowed, Export ("adUnitID")]
		string AdUnitId { get; set; }

		// @property (nonatomic, weak) UIViewController * _Nullable rootViewController __attribute__((iboutlet));
		[NullAllowed, Export ("rootViewController", ArgumentSemantic.Weak)]
		UIViewController RootViewController { get; set; }

		// @property (assign, nonatomic) GADAdSize adSize;
		[Export ("adSize", ArgumentSemantic.Assign)]
		AdSize AdSize { get; set; }

		// @property (nonatomic, weak) id<GADBannerViewDelegate> _Nullable delegate __attribute__((iboutlet));
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IBannerViewDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<GADAdSizeDelegate> _Nullable adSizeDelegate __attribute__((iboutlet));
		[NullAllowed, Export ("adSizeDelegate", ArgumentSemantic.Weak)]
		IAdSizeDelegate AdSizeDelegate { get; set; }

		// -(void)loadRequest:(GADRequest * _Nullable)request;
		[Export ("loadRequest:")]
		void LoadRequest ([NullAllowed] Request request);

		// @property (getter = isAutoloadEnabled, assign, nonatomic) BOOL autoloadEnabled;
		[Export ("autoloadEnabled")]
		bool AutoloadEnabled { [Bind ("isAutoloadEnabled")] get; set; }

		// @property (readonly, nonatomic) GADResponseInfo * _Nullable responseInfo;
		[NullAllowed, Export ("responseInfo")]
		ResponseInfo ResponseInfo { get; }

		// @property (copy, nonatomic) GADPaidEventHandler _Nullable paidEventHandler;
		[NullAllowed, Export ("paidEventHandler", ArgumentSemantic.Copy)]
		PaidEventHandler PaidEventHandler { get; set; }
	}

	interface IBannerViewDelegate { }

	// @protocol GADBannerViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "GADBannerViewDelegate")]
	interface BannerViewDelegate {
		[EventArgs ("BannerViewE")]
		[EventName ("AdReceived")]
		// @optional -(void)adViewDidReceiveAd:(GADBannerView * _Nonnull)bannerView;
		[Export ("adViewDidReceiveAd:")]
		void DidReceiveAd (BannerView bannerView);

		[EventArgs ("BannerViewError")]
		[EventName ("ReceiveAdFailed")]
		// @optional -(void)adView:(GADBannerView * _Nonnull)bannerView didFailToReceiveAdWithError:(GADRequestError * _Nonnull)error;
		[Export ("adView:didFailToReceiveAdWithError:")]
		void DidFailToReceiveAd (BannerView bannerView, RequestError error);

		[EventArgs ("BannerViewE")]
		// @optional -(void)adViewWillPresentScreen:(GADBannerView * _Nonnull)bannerView;
		[Export ("adViewWillPresentScreen:")]
		void WillPresentScreen (BannerView bannerView);

		[EventArgs ("BannerViewE")]
		// @optional -(void)adViewWillDismissScreen:(GADBannerView * _Nonnull)bannerView;
		[Export ("adViewWillDismissScreen:")]
		void WillDismissScreen (BannerView bannerView);

		[EventArgs ("BannerViewE")]
		[EventName ("ScreenDismissed")]
		// @optional -(void)adViewDidDismissScreen:(GADBannerView * _Nonnull)bannerView;
		[Export ("adViewDidDismissScreen:")]
		void DidDismissScreen (BannerView bannerView);

		[EventArgs ("BannerViewE")]
		// @optional -(void)adViewWillLeaveApplication:(GADBannerView * _Nonnull)bannerView;
		[Export ("adViewWillLeaveApplication:")]
		void WillLeaveApplication (BannerView bannerView);
	}

	interface IAdSizeDelegate { }

	// @protocol GADAdSizeDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "GADAdSizeDelegate")]
	interface AdSizeDelegate {
		// @required -(void)adView:(GADBannerView * _Nonnull)bannerView willChangeAdSizeTo:(GADAdSize)size;
		[Abstract]
		[EventArgs ("AdSizeDelegateSize")]
		[Export ("adView:willChangeAdSizeTo:")]
		void WillChangeAdSizeTo (BannerView bannerView, AdSize size);
	}

	// @interface GADRequestConfiguration : NSObject
	[BaseType (typeof (NSObject), Name = "GADRequestConfiguration")]
	interface RequestConfiguration {
		// @property (copy, nonatomic) GADMaxAdContentRating _Nullable maxAdContentRating;
		[NullAllowed, Export ("maxAdContentRating")]
		string MaxAdContentRating { get; set; }

		// @property (copy, nonatomic) NSArray<NSString *> * _Nullable testDeviceIdentifiers;
		[NullAllowed, Export ("testDeviceIdentifiers", ArgumentSemantic.Copy)]
		string [] TestDeviceIdentifiers { get; set; }

		// -(void)tagForUnderAgeOfConsent:(BOOL)underAgeOfConsent;
		[Export ("tagForUnderAgeOfConsent:")]
		void TagForUnderAgeOfConsent (bool underAgeOfConsent);

		// -(void)tagForChildDirectedTreatment:(BOOL)childDirectedTreatment;
		[Export ("tagForChildDirectedTreatment:")]
		void TagForChildDirectedTreatment (bool childDirectedTreatment);
	}

	// @interface GADMobileAds : NSObject
	[BaseType (typeof (NSObject), Name = "GADMobileAds")]
	interface MobileAds {
		// +(GADMobileAds * _Nonnull)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		MobileAds SharedInstance { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull sdkVersion;
		[Export ("sdkVersion")]
		string SdkVersion { get; }

		// @property (assign, nonatomic) float applicationVolume;
		[Export ("applicationVolume")]
		float ApplicationVolume { get; set; }

		// @property (assign, nonatomic) BOOL applicationMuted;
		[Export ("applicationMuted")]
		bool ApplicationMuted { get; set; }

		// @property (readonly, nonatomic, strong) GADAudioVideoManager * _Nonnull audioVideoManager;
		[Export ("audioVideoManager", ArgumentSemantic.Strong)]
		AudioVideoManager AudioVideoManager { get; }

		// @property (readonly, nonatomic, strong) GADRequestConfiguration * _Nonnull requestConfiguration;
		[Export ("requestConfiguration", ArgumentSemantic.Strong)]
		RequestConfiguration RequestConfiguration { get; }

		// @property (readonly, nonatomic) GADInitializationStatus * _Nonnull initializationStatus;
		[Export ("initializationStatus")]
		InitializationStatus InitializationStatus { get; }

		// -(BOOL)isSDKVersionAtLeastMajor:(NSInteger)major minor:(NSInteger)minor patch:(NSInteger)patch;
		[Export ("isSDKVersionAtLeastMajor:minor:patch:")]
		bool IsSDKVersionAtLeast (nint major, nint minor, nint patch);

		// -(void)startWithCompletionHandler:(GADInitializationCompletionHandler _Nullable)completionHandler;
		[Export ("startWithCompletionHandler:")]
		void Start ([NullAllowed] InitializationCompletionHandler completionHandler);

		// -(void)disableAutomatedInAppPurchaseReporting;
		[Export ("disableAutomatedInAppPurchaseReporting")]
		void DisableAutomatedInAppPurchaseReporting ();

		// -(void)enableAutomatedInAppPurchaseReporting;
		[Export ("enableAutomatedInAppPurchaseReporting")]
		void EnableAutomatedInAppPurchaseReporting ();

		// -(void)disableSDKCrashReporting;
		[Export ("disableSDKCrashReporting")]
		void DisableSDKCrashReporting ();

		// -(void)disableMediationInitialization;
		[Export ("disableMediationInitialization")]
		void DisableMediationInitialization ();
	}

	// @interface GADAudioVideoManager : NSObject
	[BaseType (typeof (NSObject),
		Name = "GADAudioVideoManager",
		Delegates = new string [] { "Delegate" },
		Events = new Type [] { typeof (AudioVideoManagerDelegate) })]
	interface AudioVideoManager {
		// @property (nonatomic, weak) id<GADAudioVideoManagerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IAudioVideoManagerDelegate Delegate { get; set; }

		// @property (assign, nonatomic) BOOL audioSessionIsApplicationManaged;
		[Export ("audioSessionIsApplicationManaged")]
		bool AudioSessionIsApplicationManaged { get; set; }
	}

	interface IAudioVideoManagerDelegate { }

	// @protocol GADAudioVideoManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject), Name = "GADAudioVideoManagerDelegate")]
	interface AudioVideoManagerDelegate {
		[EventArgs ("AudioVideoManagerWillPlayVideo")]
		// @optional -(void)audioVideoManagerWillPlayVideo:(GADAudioVideoManager * _Nonnull)audioVideoManager;
		[Export ("audioVideoManagerWillPlayVideo:")]
		void WillPlayVideo (AudioVideoManager audioVideoManager);

		[EventArgs ("AudioVideoManagerAllVideoPaused")]
		[EventName ("AllVideoPaused")]
		// @optional -(void)audioVideoManagerDidPauseAllVideo:(GADAudioVideoManager * _Nonnull)audioVideoManager;
		[Export ("audioVideoManagerDidPauseAllVideo:")]
		void DidPauseAllVideo (AudioVideoManager audioVideoManager);

		[EventArgs ("AudioVideoManagerWillPlayAudio")]
		// @optional -(void)audioVideoManagerWillPlayAudio:(GADAudioVideoManager * _Nonnull)audioVideoManager;
		[Export ("audioVideoManagerWillPlayAudio:")]
		void WillPlayAudio (AudioVideoManager audioVideoManager);

		[EventArgs ("AudioVideoManagerPlayingAudioStopped")]
		[EventName ("PlayingAudioStopped")]
		// @optional -(void)audioVideoManagerDidStopPlayingAudio:(GADAudioVideoManager * _Nonnull)audioVideoManager;
		[Export ("audioVideoManagerDidStopPlayingAudio:")]
		void DidStopPlayingAudio (AudioVideoManager audioVideoManager);
	}

	// @interface GADInitializationStatus : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "GADInitializationStatus")]
	interface InitializationStatus : INSCopying {
		// @property (readonly, nonatomic) NSDictionary<NSString *,GADAdapterStatus *> * _Nonnull adapterStatusesByClassName;
		[Export ("adapterStatusesByClassName")]
		NSDictionary<NSString, AdapterStatus> AdapterStatusesByClassName { get; }
	}

	// @interface GADAdapterStatus : NSObject <NSCopying>
	[BaseType (typeof (NSObject), Name = "GADAdapterStatus")]
	interface AdapterStatus : INSCopying {
		// @property (readonly, nonatomic) GADAdapterInitializationState state;
		[Export ("state")]
		AdapterInitializationState State { get; }

		// @property (readonly, nonatomic) NSString * _Nonnull description;
		[Export ("description")]
		string Description { get; }

		// @property (readonly, nonatomic) NSTimeInterval latency;
		[Export ("latency")]
		double Latency { get; }
	}

	// typedef void (^GADInitializationCompletionHandler)(GADInitializationStatus * _Nonnull);
	delegate void InitializationCompletionHandler (InitializationStatus arg0);

	// @interface GADAdLoaderOptions : NSObject
	[BaseType (typeof (NSObject), Name = "GADAdLoaderOptions")]
	interface AdLoaderOptions {
	}
}