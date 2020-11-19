namespace ProdData.Factories
{
    using ProdData.Models;
    using ProdCore.Interfaces;

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
