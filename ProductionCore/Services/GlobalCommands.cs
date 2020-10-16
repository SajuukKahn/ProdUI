namespace ProductionCore.Services
{
    using Prism.Commands;

    /// <summary>
    /// Defines the <see cref="GlobalCommands" />.
    /// </summary>
    public static class GlobalCommands
    {
        /// <summary>
        /// Defines the RequestProgram.
        /// </summary>
        public static CompositeCommand RequestProgram = new CompositeCommand();
    }
}
