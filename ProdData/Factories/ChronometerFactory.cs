namespace ProdData.Factories
{
    using ProdData.Models;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
    public class ChronometerFactory : IChronometerFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChronometerFactory"/> class.
        /// </summary>
        public ChronometerFactory()
        {
        }

        /// <inheritdoc/>
        public IChronometer Create()
        {
            return new Chronometer();
        }
    }
}
