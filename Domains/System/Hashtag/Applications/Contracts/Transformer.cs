namespace Wks.DataTransformers;

public class HashTagDataTransformers
{
    public class Create
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Color { get; set; }

        [JsonRequired]
        public int Filter { get; set; }
    }

    public class Update : Hashtag { }
}
