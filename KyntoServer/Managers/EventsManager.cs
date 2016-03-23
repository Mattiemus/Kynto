using System;
using System.Collections.Generic;
using System.Reflection;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Services;

namespace KyntoServer.Managers
{
    /// <summary>
    /// Handles all of this servers events.
    /// </summary>
    public class EventsManager : IEventsManager
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Store the event handlers.
        /// </summary>
        private Dictionary<EventType, List<EventHandler>> _EventHandlers = new Dictionary<EventType, List<EventHandler>>();

        /// <summary>
        /// Initialises this events manager and loads all available events ready for calling.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public void Initialise(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;

            // Write that we have initialised.
            this._ServerManager.LoggingService.Write(LogImportance.Initialise, " Events manager initialised.", null);
        }

        /// <summary>
        /// Exits the events manager, stopping all events.
        /// </summary>
        public void Exit()
        {
        }

        /// <summary>
        /// Hooks an event handler to the specified event name.
        /// </summary>
        /// <param name="EventName">The event id to hook onto.</param>
        /// <param name="EventHandler">The event handler to handle the specified event.</param>
        public void HookEvent(EventType EventId, EventHandler EventHandler)
        {
            // Find out if we already have event handlers for the specified event.
            if (this._EventHandlers.ContainsKey(EventId))
            {
                // We do, simply add it
                if (this._EventHandlers[EventId] != null)
                {
                    this._EventHandlers[EventId].Add(EventHandler);
                }
                else
                {
                    // First create a new list then add it.
                    this._EventHandlers[EventId] = new List<EventHandler>();
                    this._EventHandlers[EventId].Add(EventHandler);
                }
            }
            else
            {
                // Create the event handler.
                this._EventHandlers.Add(EventId, new List<EventHandler>());
                // Store the event.
                this._EventHandlers[EventId].Add(EventHandler);
            }
        }

        /// <summary>
        /// Calls the specified event.
        /// </summary>
        /// <param name="EventName">The event to call.</param>
        /// <param name="Sender">The object that called the event.</param>
        /// <param name="Params">The params to pass too the event.</param>
        public void CallEvent(EventType EventId, object Sender, EventArgs Params)
        {
            // If we contain this event, call it.
            bool CalledAnEventHandler = false;
            if (this._EventHandlers.ContainsKey(EventId))
            {
                // Call it.
                if (this._EventHandlers[EventId] != null)
                {
                    // Loop through each handler.
                    for (int i = 0; i < this._EventHandlers[EventId].Count; i++)
                    {
                        this._EventHandlers[EventId][i](Sender, Params);
                        CalledAnEventHandler = true;
                    }
                }
            }

            // Make sure we called an event handler.
            if (!CalledAnEventHandler)
            {
                this._ServerManager.LoggingService.Write(LogImportance.Error, "Failed to find event handler for \"" + Enum.GetName(typeof(EventType), EventId) + "\".", null);
            }
        }
    }
}
