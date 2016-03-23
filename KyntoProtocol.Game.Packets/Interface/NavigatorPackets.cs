using System;
using System.Linq;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Interfaces.Instances.Rooms;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The request navigator packet.
    /// </summary>
    public class RequestNavigator
    {
        /// <summary>
        /// The type of request.
        /// </summary>
        public int T;

        /// <summary>
        /// The query in the request.
        /// </summary>
        public string Q;
    }

    /// <summary>
    /// The navigator response packet.
    /// </summary>
    public class NavigatorPacket
    {
        /// <summary>
        /// The type of request that this is replying too.
        /// </summary>
        public int T;

        /// <summary>
        /// The private rooms in this response.
        /// </summary>
        public PrivateRoomDataPacket[] Pr;

        /// <summary>
        /// The public rooms in this response.
        /// </summary>
        public PublicRoomDataPacket[] Pu;
    }

    /// <summary>
    /// Represents a single private room in a navigator request.
    /// </summary>
    public class PrivateRoomDataPacket
    {
        /// <summary>
        /// Gets an array of packets from an array of database items.
        /// </summary>
        /// <param name="PrivateRoomsArray"></param>
        /// <returns></returns>
        public static PrivateRoomDataPacket[] FromArray(IRoomsDatabaseTable[] PrivateRoomsArray, IServerManager ServerManager)
        {
            // Create the data buffer.
            PrivateRoomDataPacket[] DataPacket = new PrivateRoomDataPacket[PrivateRoomsArray.Length];

            // Add all the items.
            for (int i = 0; i < DataPacket.Length; i++)
            {
                // Add the item.
                DataPacket[i] = new PrivateRoomDataPacket();
                DataPacket[i].I = PrivateRoomsArray[i].Id;
                DataPacket[i].N = PrivateRoomsArray[i].Name;
                DataPacket[i].D = PrivateRoomsArray[i].Description;
                DataPacket[i].O = PrivateRoomsArray[i].OwnerId.ToString();

                // Find the number of users.
                DataPacket[i].U = 0;

                // Get number of users.
                KeyValuePair<int, IRoom>[] OnlinePrivates = ServerManager.RoomsManager.PrivateRooms.ToArray();
                for (int i2 = 0; i2 < OnlinePrivates.Length; i2++)
                {
                    if (((IPrivateRoom)OnlinePrivates[i2].Value).DatabaseTable.Id == PrivateRoomsArray[i].Id)
                    {
                        DataPacket[i].U = ((IPrivateRoom)OnlinePrivates[i2].Value).UsersInRoom.Count;
                    }
                }
            }

            // Return the databuffer.
            return DataPacket;
        }

        /// <summary>
        /// The room id.
        /// </summary>
        public int I;

        /// <summary>
        /// The room name.
        /// </summary>
        public string N;

        /// <summary>
        /// The room description.
        /// </summary>
        public string D;

        /// <summary>
        /// The rooms owner username.
        /// </summary>
        public string O;

        /// <summary>
        /// The number of users in the room.
        /// </summary>
        public int U;
    }

    /// <summary>
    /// Represents a single public room in a request.
    /// </summary>
    public class PublicRoomDataPacket
    {
        /// <summary>
        /// Gets an array of packets from an array of database items.
        /// </summary>
        /// <param name="PrivateRoomsArray"></param>
        /// <returns></returns>
        public static PublicRoomDataPacket[] FromArray(IPublicsDatabaseTable[] PublicRoomsArray, IServerManager ServerManager)
        {
            // Create the data buffer.
            PublicRoomDataPacket[] DataPacket = new PublicRoomDataPacket[PublicRoomsArray.Length];
            
            // Add all the items.
            for (int i = 0; i < DataPacket.Length; i++)
            {
                // Add the item.
                DataPacket[i] = new PublicRoomDataPacket();
                DataPacket[i].I = PublicRoomsArray[i].Id;
                DataPacket[i].N = PublicRoomsArray[i].Name;
                DataPacket[i].D = PublicRoomsArray[i].Description;

                // Find the number of users.
                DataPacket[i].U = 0;

                // Get number of users.
                KeyValuePair<int, IRoom>[] OnlinePublics = ServerManager.RoomsManager.PublicRooms.ToArray();
                for (int i2 = 0; i2 < OnlinePublics.Length; i2++)
                {
                    if (((IPublicRoom)OnlinePublics[i2].Value).DatabaseTable.Id == PublicRoomsArray[i].Id)
                    {
                        DataPacket[i].U = ((IPublicRoom)OnlinePublics[i2].Value).UsersInRoom.Count;
                    }
                }
            }

            // Return the databuffer.
            return DataPacket;
        }

        /// <summary>
        /// The room id.
        /// </summary>
        public int I;

        /// <summary>
        /// The room name.
        /// </summary>
        public string N;

        /// <summary>
        /// The room description.
        /// </summary>
        public string D;

        /// <summary>
        /// The number of users in the room.
        /// </summary>
        public int U;
    }
}
