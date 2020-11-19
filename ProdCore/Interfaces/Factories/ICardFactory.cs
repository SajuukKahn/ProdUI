namespace ProdCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ICardFactory" />.
    /// </summary>
    public interface ICardFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="ICard"/>.</returns>
        ICard Create();
    }
}
