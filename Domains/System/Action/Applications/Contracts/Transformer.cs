namespace Wks.DataTransformers;

//
// Summary:
//     Contains data transformer objects for activity operations.
//     Provides structured objects for creating activities with validation attributes.
//
public class ActionDataTransformers
{
    //
    // Summary:
    //     Represents the data structure for creating multiple activities.
    //     Contains a collection of activities to be created.
    //
    public class Create
    {
        //
        // Summary:
        //     Gets or sets the collection of activities to be created.
        //     Marked as JsonRequired to ensure this property is present in JSON requests.
        //
        [JsonRequired]
        public IEnumerable<Action> Actions { get; set; }
    }

    //
    // Summary:
    //     Represents the data structure for a single activity.
    //     Contains the activity type and associated JSON data.
    //
    public class Action
    {
        //
        // Summary:
        //     Gets or sets the type/category of the activity.
        //     Marked as JsonRequired to ensure this property is present in JSON requests.
        //
        [JsonRequired]
        public string Type { get; set; }

        //
        // Summary:
        //     Gets or sets the JSON data associated with the activity.
        //     Marked as JsonRequired to ensure this property is present in JSON requests.
        //
        [JsonRequired]
        public string JsonData { get; set; }
    }
}
