namespace ProdData.Factories
{
    using global::ProdData.Models;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ChonometerFactory" />.
    /// </summary>
    public class ChonometerFactory : IChronometerFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChonometerFactory"/> class.
        /// </summary>
        public ChonometerFactory()
        {
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="IChronometer"/>.</returns>
        public IChronometer Create()
        {
            return new Chronometer();
        }
    }
}
