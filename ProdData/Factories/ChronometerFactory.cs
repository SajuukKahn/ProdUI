namespace ProdData.Factories
{
    using ProdData.Models;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ChronometerFactory" />.
    /// </summary>
    public class ChronometerFactory : IChronometerFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChronometerFactory"/> class.
        /// </summary>
        public ChronometerFactory()
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
