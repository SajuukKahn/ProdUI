namespace ProdData.Factories
{
    using ProdData.Models;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
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

        /// <inheritdoc/>
        public ICard Create()
        {
            return new Card(_chronometerFactory);
        }
    }
}
