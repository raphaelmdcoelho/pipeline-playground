namespace PipelinePlayground.Dtos
{
    public sealed record AppUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
