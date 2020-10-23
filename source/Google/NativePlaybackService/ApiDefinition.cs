using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace KenZenSoft.NativePlaybackService {

	// @interface NativePlaybackService : NSObject
	[BaseType (typeof (NSObject))]
	interface NativePlaybackService {
		// @property (copy, nonatomic) void (^ _Nullable)(NSString * _Nonnull, NSDictionary * _Nullable) logFunction;
		[NullAllowed, Export ("logFunction", ArgumentSemantic.Copy)]
		Action<NSString, NSDictionary> LogFunction { get; set; }

		// @property (copy, nonatomic) void (^ _Nullable)(void) handlePlayReadyFunction;
		[NullAllowed, Export ("handlePlayReadyFunction", ArgumentSemantic.Copy)]
		Action HandlePlayReadyFunction { get; set; }

		// @property (copy, nonatomic) void (^ _Nullable)(void) handlePlayEndFunction;
		[NullAllowed, Export ("handlePlayEndFunction", ArgumentSemantic.Copy)]
		Action HandlePlayEndFunction { get; set; }

		// @property (copy, nonatomic) void (^ _Nullable)(NSString * _Nonnull) handlePlayUpdateFunction;
		[NullAllowed, Export ("handlePlayUpdateFunction", ArgumentSemantic.Copy)]
		Action<NSString> HandlePlayUpdateFunction { get; set; }

		// -(double)getLengthSecond __attribute__((warn_unused_result));
		[Export ("getLengthSecond")]
		double LengthSecond ();

		// -(void)initUrl:(NSString * _Nonnull)url __attribute__((objc_method_family("none")));
		[Export ("initUrl:")]
		void InitUrl (string url);

		// -(BOOL)pauseAndReturnError:(NSError * _Nullable * _Nullable)error;
		[Export ("pauseAndReturnError:")]
		bool Pause([NullAllowed] out NSError error);

		// -(BOOL)toPauseSessionStateAndReturnError:(NSError * _Nullable * _Nullable)error;
		[Export ("toPauseSessionStateAndReturnError:")]
		bool ToPauseSessionState([NullAllowed] out NSError error);

		// -(BOOL)toActiveSessionStateAndReturnError:(NSError * _Nullable * _Nullable)error;
		[Export ("toActiveSessionStateAndReturnError:")]
		bool ToActiveSessionState([NullAllowed] out NSError error);

		// -(BOOL)playAndReturnError:(NSError * _Nullable * _Nullable)error;
		[Export ("playAndReturnError:")]
		bool Play([NullAllowed] out NSError error);

		// -(void)seek:(double)value;
		[Export ("seek:")]
		void Seek(double value);

		// -(void)Dispose;
		[Export ("Dispose")]
		void Dispose ();
	}
}

