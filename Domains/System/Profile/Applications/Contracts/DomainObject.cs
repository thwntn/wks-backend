namespace Wks.Objects;

public class DomainObject
{
    public class Type
    {
        public int Enum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Update
    {
        public string Host { get; set; }
        public DomainTypeEnum Type { get; set; }
    }
}
