namespace SampleRestApi.Domain.Entities
{
    public class Dog : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Color { get; set; } = null!;

        public int TailLength { get; set; }

        public int Weight { get; set; }
    }
}
