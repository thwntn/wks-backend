namespace Wks.DataTransformers;

//
// Summary:
//     Contains data transfer objects for user profile operations.
//     Provides structured objects for handling user profile data in API requests and responses.
//
public class ProfileDataTransformer
{
    //
    // Summary:
    //     Represents the data structure for updating user profile information.
    //     All properties are marked as required to ensure complete profile updates.
    //
    public class Update
    {
        //
        // Summary:
        //     Gets or sets the full name of the user.
        //     Required field for profile updates.
        //
        [JsonRequired]
        public string Name { get; set; }

        //
        // Summary:
        //     Gets or sets the phone number of the user.
        //     Required field for profile updates and contact information.
        //
        [JsonRequired]
        public string Phone { get; set; }

        //
        // Summary:
        //     Gets or sets the address of the user.
        //     Required field for profile updates and location information.
        //
        [JsonRequired]
        public string Address { get; set; }

        //
        // Summary:
        //     Gets or sets the description or bio of the user.
        //     Required field for profile updates and user information.
        //
        [JsonRequired]
        public string Description { get; set; }
    }

    public class UpdateAvatar
    {
        [JsonRequired]
        public string ArtifactToken { get; set; }
    }

    public class UpdateCover
    {
        [JsonRequired]
        public string ArtifactToken { get; set; }
    }
}
