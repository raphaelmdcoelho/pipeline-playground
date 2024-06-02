namespace PipelinePlayground.Entities
{
    public sealed class Category
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
