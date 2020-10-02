namespace ProdUI
{
    /// <summary>
    /// Defines the <see cref="Notes" />.
    /// </summary>
    internal class Notes
    {
        /*
           //TODO Notes Section:

            1. StyleCop
            2. Event aggregator - replace with services
            3. all csv files write to JSON instead, give the object to JSON
            4. subview - zones like a security camera
            5. changing the UI (outside of #4) at all is last on the list

            look at dashboard page service
            dashboard tile service

            + Interface all of the concrete classes and resolve within Unity container / factories in appropriate projects
            + Add more Interfaces to universalize properties within classes and Interfaces
            + Eliminate the EventAggregator and all PubSubEvent inheriting classes and replace with services
            + Add ability to spawn multiple process regions


        Steps:

        Fix or revamp Enumerated StepStatus to be simpler, possibly just a string.

        PlaybackService
        - Pause / Play / Halt / Abort / Raise Error / End (notify others)
        - ICard, ICardFactory : needs creation

        ModalService : create to handle Modal transactions and control the ModalViewModel

        Move Concretes to ProdData, move Services to appropriate locations

        Create FileService in Test, use to load programs and filenames, and save programdata to JSON
        -> use some simple overloaded methods like LogData(T[] members) to save all relevant data...

        */
    }
}
