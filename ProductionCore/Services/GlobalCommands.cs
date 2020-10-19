namespace ProductionCore.Services
{
    using Prism.Commands;

    /// <summary>
    /// Defines the <see cref="GlobalCommands" />.
    /// </summary>
    public static class GlobalCommands
    {
        /// <summary>
        /// Gets or sets defines the RequestProgram.
        /// </summary>
        public static CompositeCommand RequestProgram { get; set; } = new CompositeCommand();
    }
}
