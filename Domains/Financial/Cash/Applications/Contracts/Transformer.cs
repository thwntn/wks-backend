namespace Wks.DataTransformers;

public class CashDescriptionDataTransformer
{
    public class Create
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Quantity { get; set; }

        [Required]
        public CashNames CashNames { get; set; }
    }
}

public class CashDataTransformer
{
    public class Create
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long Quantity { get; set; }
    }
}
