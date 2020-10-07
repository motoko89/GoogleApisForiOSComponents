
using ObjCRuntime;

namespace Google.UserMessagingPlatform {
	[Native]
	public enum DebugGeography : long {
		Disabled = 0,
		Eea = 1,
		NotEEA = 2
	}

	[Native]
	public enum ConsentStatus : long {
		Unknown = 0,
		Required = 1,
		NotRequired = 2,
		Obtained = 3
	}

	[Native]
	public enum ConsentType : long {
		Unknown = 0,
		Personalized = 1,
		NonPersonalized = 2
	}

	[Native]
	public enum FormStatus : long {
		Unknown = 0,
		Available = 1,
		Unavailable = 2
	}

	[Native]
	public enum RequestErrorCode : long {
		Internal = 1,
		InvalidAppID = 2,
		Network = 3,
		Misconfiguration = 4
	}

	[Native]
	public enum FormErrorCode : long {
		Internal = 5,
		AlreadyUsed = 6,
		Unavailable = 7,
		Timeout = 8
	}
}
