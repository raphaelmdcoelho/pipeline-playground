namespace PipelinePlayground.Configurations
{
    public sealed record OdbcConfiguration
    {
        public string ConnectionString { get; set; } = null!;
    }
}
