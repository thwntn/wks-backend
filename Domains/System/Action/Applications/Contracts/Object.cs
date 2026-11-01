namespace Wks.Objects;

//
// Summary:
//     Contains data transfer objects for activity operations.
//     Provides structured objects for creating activities and returning activity responses.
//
public class ActionObject
{
    //
    // Summary:
    //     Represents the data structure for creating a new activity.
    //     Contains the activity type and associated JSON data.
    //
    // Parameters:
    //   type:
    //     The type/category of the activity.
    //   jsonData:
    //     The JSON data associated with the activity.
    //
    public class Create(string type, string jsonData)
    {
        //
        // Summary:
        //     Gets or sets the type/category of the activity.
        //
        public string Type { get; set; } = type;

        //
        // Summary:
        //     Gets or sets the JSON data associated with the activity.
        //
        public string JsonData { get; set; } = jsonData;
    }

    //
    // Summary:
    //     Represents the response structure for an activity.
    //     Contains profile information, activity data, and metadata.
    //
    public class ActionResponse
    {
        //
        // Summary:
        //     Gets or sets the profile information associated with the activity.
        //     Marked as NotMapped to prevent Entity Framework mapping.
        //
        [NotMapped]
        public AuthObject.ProfileResponse Profile { get; set; }

        //
        // Summary:
        //     Gets or sets the JSON data of the activity.
        //     Marked as NotMapped to prevent Entity Framework mapping.
        //
        [NotMapped]
        public string JsonData { get; set; }

        //
        // Summary:
        //     Gets or sets the creation date/time of the activity.
        //     Marked as NotMapped to prevent Entity Framework mapping.
        //
        [NotMapped]
        public DateTime CreatedAt { get; set; }

        //p[o;lkijnhi0i\r\]
        // Summary:
        //     Gets or sets the type/category of the activity.
        //     Marked as NotMapped to prevent Entity Framework mapping.
        //
        [NotMapped]
        public string Type { get; set; }
    }
}
