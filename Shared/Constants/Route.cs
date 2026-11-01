namespace Wks.Mains;

///
/// Summary:
///     Defines route templates for the retained WKS platform APIs.
///     Controllers combine base routes and action templates; runtime API prefixing is configured separately.
///
public class RouteConstant
{
    ///
    /// Summary:
    ///     Route parameter identifying a resource by its ID.
    ///
    public const string ID = "{id}";
    ///
    /// Summary:
    ///     Base route for feedback and support conversations.
    ///
    public const string FEEDBACK = "system/feedbacks";
    ///
    /// Summary:
    ///     Replies to the feedback identified by {id}.
    ///
    public const string FEEDBACK_REPLY = "{id}/reply";
    ///
    /// Summary:
    ///     Lists supported feedback status values.
    ///
    public const string FEEDBACK_STATUS = "statuses";
    ///
    /// Summary:
    ///     Pins the feedback identified by {id}.
    ///
    public const string FEEDBACK_PIN = "{id}/pin";
    ///
    /// Summary:
    ///     Unpins the feedback identified by {id}.
    ///
    public const string FEEDBACK_UNPIN = "{id}/unpin";
    ///
    /// Summary:
    ///     Base route for notifications belonging to a profile.
    ///
    public const string NOTIFICATIONS = "system/notifications";
    ///
    /// Summary:
    ///     Marks a notification as read; the controller appends its ID.
    ///
    public const string NOTIFICATION_READ = "read";
    ///
    /// Summary:
    ///     Handles a notification action; the controller appends its ID.
    ///
    public const string NOTIFICATION_HANDLE = "handle";
    ///
    /// Summary:
    ///     Base route for managing profile icons.
    ///
    public const string ICONS = "system/icons";
    ///
    /// Summary:
    ///     Health endpoint used to check whether the API is running.
    ///
    public const string HEALTH = "system/health";
    ///
    /// Summary:
    ///     Base route for server time and timezone information.
    ///
    public const string TIME = "system/time";
    ///
    /// Summary:
    ///     Returns the configured timezone information.
    ///
    public const string TIME_TIMEZONE = "timezone";
    ///
    /// Summary:
    ///     Returns the current server time.
    ///
    public const string TIME_NOW = "now";
    ///
    /// Summary:
    ///     Base route for artifact registration and file access.
    ///
    public const string ARTIFACT = "system/artifacts";
    ///
    /// Summary:
    ///     Downloads an artifact by token, with an optional catch-all file name.
    ///
    public const string ARTIFACT_TOKEN = "token/{token}/{**fileName}";
    ///
    /// Summary:
    ///     Creates a presigned object-storage upload session.
    ///
    public const string ARTIFACT_SESSION = "session";
    ///
    /// Summary:
    ///     Returns a bundled static file identified by {fileName}.
    ///
    public const string ARTIFACT_STATIC = "static/{fileName}";
    ///
    /// Summary:
    ///     Base route for platform administration protected by ADMIN_SETTING.
    ///
    public const string ADMIN = "system/admin";
    ///
    /// Summary:
    ///     Creates a domain for the account identified by {accountId}.
    ///
    public const string ADMIN_DOMAIN_SETUP = "accounts/{accountId}/domains";
    ///
    /// Summary:
    ///     Lists domains for the profile identified by {profileId}.
    ///
    public const string ADMIN_PROFILE_DOMAINS = "profiles/{profileId}/domains";
    ///
    /// Summary:
    ///     Lists profiles through platform administration.
    ///
    public const string ADMIN_PROFILE_LIST = "profiles";
    ///
    /// Summary:
    ///     Gets the profile identified by {id} through platform administration.
    ///
    public const string ADMIN_PROFILE_GET = "profiles/{id}";
    ///
    /// Summary:
    ///     Creates an account role through platform administration.
    ///
    public const string ADMIN_ROLE_CREATE = "roles";
    ///
    /// Summary:
    ///     Lists roles belonging to the account identified by {accountId}.
    ///
    public const string ADMIN_ROLE_LIST = "roles/accounts/{accountId}";
    ///
    /// Summary:
    ///     Gets the role identified by {id}.
    ///
    public const string ADMIN_ROLE_GET = "roles/{id}";
    ///
    /// Summary:
    ///     Updates the role identified by {id}.
    ///
    public const string ADMIN_ROLE_UPDATE = "roles/{id}";
    ///
    /// Summary:
    ///     Removes roles selected by the administrative request.
    ///
    public const string ADMIN_ROLE_REMOVE = "roles";
    ///
    /// Summary:
    ///     Assigns the role identified by {id}.
    ///
    public const string ADMIN_ROLE_ASSIGN = "roles/{id}/assign";
    ///
    /// Summary:
    ///     Unassigns the role identified by {id}.
    ///
    public const string ADMIN_ROLE_UNASSIGN = "roles/{id}/unassign";
    ///
    /// Summary:
    ///     Lists the platform permission definitions available to roles.
    ///
    public const string ADMIN_ROLE_PERMISSIONS = "roles/permissions";
    ///
    /// Summary:
    ///     Creates an application version entry.
    ///
    public const string ADMIN_VERSION_CREATE = "versions";
    ///
    /// Summary:
    ///     Updates the application version identified by {id}.
    ///
    public const string ADMIN_VERSION_UPDATE = "versions/{id}";
    ///
    /// Summary:
    ///     Removes the application version identified by {id}.
    ///
    public const string ADMIN_VERSION_REMOVE = "versions/{id}";
    ///
    /// Summary:
    ///     Adds a changelog feature to the version identified by {id}.
    ///
    public const string ADMIN_VERSION_CREATE_FEATURE = "versions/{id}/features";
    ///
    /// Summary:
    ///     Removes the changelog feature identified by {featureId}.
    ///
    public const string ADMIN_VERSION_REMOVE_FEATURE = "versions/features/{featureId}";
    ///
    /// Summary:
    ///     Lists supported system setting names.
    ///
    public const string ADMIN_SETTING_NAME = "settings/names";
    ///
    /// Summary:
    ///     Lists system settings through platform administration.
    ///
    public const string ADMIN_SETTING_LIST = "settings";
    ///
    /// Summary:
    ///     Creates a system setting through platform administration.
    ///
    public const string ADMIN_SETTING_CREATE = "settings";
    ///
    /// Summary:
    ///     Updates a system setting through platform administration.
    ///
    public const string ADMIN_SETTING_UPDATE = "settings";
    ///
    /// Summary:
    ///     Removes a system setting through platform administration.
    ///
    public const string ADMIN_SETTING_REMOVE = "settings";
    ///
    /// Summary:
    ///     Lists supported domain types through platform administration.
    ///
    public const string ADMIN_DOMAIN_TYPE = "domains/types";
    ///
    /// Summary:
    ///     Updates a domain belonging to the profile identified by {profileId}.
    ///
    public const string ADMIN_DOMAIN_UPDATE = "profiles/{profileId}/domains";
    ///
    /// Summary:
    ///     Removes a domain belonging to the profile identified by {profileId}.
    ///
    public const string ADMIN_DOMAIN_REMOVE = "profiles/{profileId}/domains";
    ///
    /// Summary:
    ///     Clears cached platform data through platform administration.
    ///
    public const string ADMIN_CACHE = "cache";
    ///
    /// Summary:
    ///     Synchronizes feature definitions and associated permissions.
    ///
    public const string ADMIN_SYNC_FEATURES = "features/sync";
    ///
    /// Summary:
    ///     Base route for platform feature definitions.
    ///
    public const string FEATURE = "system/features";
    ///
    /// Summary:
    ///     Base route for reading system settings.
    ///
    public const string SETTINGS = "system/settings";
    ///
    /// Summary:
    ///     Base route for system role operations.
    ///
    public const string ROLE = "system/roles";
    ///
    /// Summary:
    ///     Grants platform administration permissions after validating the configured admin pass.
    ///
    public const string ROLE_MAKE_ADMIN = "make-admin";
    ///
    /// Summary:
    ///     Base route for application versions and changelogs.
    ///
    public const string VERSION = "system/versions";
    ///
    /// Summary:
    ///     Returns the current application version.
    ///
    public const string VERSION_CURRENT = "current";
    ///
    /// Summary:
    ///     Base route for authenticated account audit actions.
    ///
    public const string ACTION = "secure/actions";
    ///
    /// Summary:
    ///     Base route for account authentication and verification.
    ///
    public const string AUTH = "secure/auth";
    ///
    /// Summary:
    ///     Registers a new account.
    ///
    public const string AUTH_SIGNUP = "signup";
    ///
    /// Summary:
    ///     Starts account email verification.
    ///
    public const string AUTH_VERIFY_EMAIL = "verify-email";
    ///
    /// Summary:
    ///     Authenticates an account with its password.
    ///
    public const string AUTH_USE_PASSWORD = "use-password";
    ///
    /// Summary:
    ///     Confirms an account verification code.
    ///
    public const string AUTH_CONFIRM_CODE = "confirm-code";
    ///
    /// Summary:
    ///     Starts the account password reset flow.
    ///
    public const string AUTH_RESET_PASSWORD = "reset-password";
    ///
    /// Summary:
    ///     Changes an account password using the authentication flow.
    ///
    public const string AUTH_CHANGE_PASSWORD = "change-password";
    ///
    /// Summary:
    ///     Authenticates an account with Google.
    ///
    public const string AUTH_GOOGLE = "google";
    ///
    /// Summary:
    ///     Gets the Google OAuth authorization URL.
    ///
    public const string AUTH_GOOGLE_PRESIGNED = "google/presigned";
    ///
    /// Summary:
    ///     Returns the Google account setup status.
    ///
    public const string AUTH_GOOGLE_STATUS = "google/status";
    ///
    /// Summary:
    ///     Base route for profile information and updates.
    ///
    public const string PROFILE = "secure/profiles";
    ///
    /// Summary:
    ///     Returns the current authenticated profile.
    ///
    public const string PROFILE_ME = "me";
    ///
    /// Summary:
    ///     Updates the current profile avatar.
    ///
    public const string PROFILE_UPDATE_AVATAR = "update-avatar";
    ///
    /// Summary:
    ///     Updates the current profile cover picture.
    ///
    public const string PROFILE_UPDATE_COVER_PICTURE = "update-cover-picture";
    ///
    /// Summary:
    ///     Base route for authentication token operations.
    ///
    public const string TOKEN = "secure/token";
    ///
    /// Summary:
    ///     Refreshes authentication tokens using {refreshToken}.
    ///
    public const string TOKEN_GENERATE = "refresh/{refreshToken}";
    ///
    /// Summary:
    ///     Base route for authenticated profile domain management.
    ///
    public const string DOMAIN = "secure/domains";
    ///
    /// Summary:
    ///     Base route for managing actor accounts and their permissions.
    ///
    public const string ACTOR = "actors";
    ///
    /// Summary:
    ///     Updates the status of the actor account identified by {id}.
    ///
    public const string ACTOR_STATUS = "{id}/status";
    ///
    /// Summary:
    ///     Lists roles for the actor account identified by {id}.
    ///
    public const string ACTOR_ROLE_LIST = "{id}/roles";
    ///
    /// Summary:
    ///     Creates a role for the actor account identified by {id}.
    ///
    public const string ACTOR_ROLE_CREATE = "{id}/roles";
    ///
    /// Summary:
    ///     Updates the actor role identified by {roleId}.
    ///
    public const string ACTOR_ROLE_UPDATE = "roles/{roleId}";
    ///
    /// Summary:
    ///     Removes the actor role identified by {roleId}.
    ///
    public const string ACTOR_ROLE_REMOVE = "roles/{roleId}";
    ///
    /// Summary:
    ///     Lists permission definitions available for actor roles.
    ///
    public const string ACTOR_ROLE_PERMISSIONS = "permissions";
}
