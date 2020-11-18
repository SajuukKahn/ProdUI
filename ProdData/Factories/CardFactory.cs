namespace ProdData.Factories
{
    using ProdData.Models;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="CardFactory" />.
    /// </summary>
    public class CardFactory : ICardFactory
    {
        /// <summary>
        /// Defines the _chronometerFactory.
        /// </summary>
        private readonly IChronometerFactory _chronometerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardFactory"/> class.
        /// </summary>
        /// <param name="chronometerFactory">The chronometerFactory<see cref="IChronometerFactory"/>.</param>
        public CardFactory(IChronometerFactory chronometerFactory)
        {
            _chronometerFactory = chronometerFactory;
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="ICard"/>.</returns>
        public ICard Create()
        {
            return new Card(_chronometerFactory);
        }
    }
}
