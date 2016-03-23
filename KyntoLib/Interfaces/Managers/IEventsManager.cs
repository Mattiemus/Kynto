using System;

using KyntoLib.Interfaces.Events;

namespace KyntoLib.Interfaces.Managers
{
    /// <summary>
    /// Handles all of this servers events.
    /// </summary>
    public interface IEventsManager
    {
        /// <summary>
        /// Initialises this events manager and loads all available events ready for calling.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        void Initialise(IServerManager ServerInstance);

        /// <summary>
        /// Exits the events manager, stopping all events.
        /// </summary>
        void Exit();

        /// <summary>
        /// Hooks an event handler to the specified event name.
        /// </summary>
        /// <param name="EventId">The event id to hook onto.</param>
        /// <param name="EventHandler">The event handler to handle the specified event.</param>
        void HookEvent(EventType EventId, EventHandler EventHandler);

        /// <summary>
        /// Calls the specified event.
        /// </summary>
        /// <param name="EventName">The event to call.</param>
        /// <param name="Sender">The object that called the event.</param>
        /// <param name="Params">The params to pass too the event.</param>
        void CallEvent(EventType EventId, object Sender, EventArgs Params);
    }
}
