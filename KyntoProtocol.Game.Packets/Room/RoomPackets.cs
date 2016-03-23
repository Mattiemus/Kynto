using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// A room change request packet.
    /// </summary>
    public class RoomChangePacket
    {
        /// <summary>
        /// The new room id.
        /// </summary>
        public int RID;

        /// <summary>
        /// The room type.
        /// </summary>
        public int S;

        /// <summary>
        /// The x cord to be placed in (for public rooms).
        /// </summary>
        public int X;

        /// <summary>
        /// The y cord to be placed in (for public rooms).
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// A private room data packet.
    /// </summary>
    public class PrivateRoomLoadDataPacket
    {
        /// <summary>
        /// The room id.
        /// </summary>
        public int I;

        /// <summary>
        /// The room type.
        /// </summary>
        public int T;

        /// <summary>
        /// The number of room views.
        /// </summary>
        public int V;

        /// <summary>
        /// The rooms name.
        /// </summary>
        public string N;

        /// <summary>
        /// The rooms description.
        /// </summary>
        public string D;

        /// <summary>
        /// The rooms owner id.
        /// </summary>
        public int O;

        /// <summary>
        /// The rooms floor id.
        /// </summary>
        public int Fl;

        /// <summary>
        /// The rooms wall id.
        /// </summary>
        public int Wl;

        /// <summary>
        /// The rooms length.
        /// </summary>
        public int Y;

        /// <summary>
        /// The room breadth.
        /// </summary>
        public int X;

        /// <summary>
        /// The rooms mode.
        /// </summary>
        public string M;

        /// <summary>
        /// The rooms background.
        /// </summary>
        public int Bg;

        /// <summary>
        /// The number of users in the room.
        /// </summary>
        public int U;

        /// <summary>
        /// If the user has rights in the room.
        /// </summary>
        public RoomRightsPacket R;

        /// <summary>
        /// The rooms holes mapping.
        /// </summary>
        public string[] H;

        /// <summary>
        /// The array of furniture in the room.
        /// </summary>
        public FurnictureArrayPacket[] F;
    }

    /// <summary>
    /// A public room data packet.
    /// </summary>
    public class PublicRoomLoadDataPacket
    {
        /// <summary>
        /// The rooms id.
        /// </summary>
        public int I;

        /// <summary>
        /// The room type.
        /// </summary>
        public int T;

        /// <summary>
        /// The rooms name.
        /// </summary>
        public string N;

        /// <summary>
        /// The rooms description.
        /// </summary>
        public string D;

        /// <summary>
        /// The rooms data.
        /// </summary>
        public string Da;

        /// <summary>
        /// The rooms height mapping layout.
        /// </summary>
        public string H;

        /// <summary>
        /// The rooms rows.
        /// </summary>
        public int X;

        /// <summary>
        /// The rooms cols.
        /// </summary>
        public int Y;

        /// <summary>
        /// The rooms offset x position.
        /// </summary>
        public int OX;

        /// <summary>
        /// The rooms offset y position.
        /// </summary>
        public int OY;

        /// <summary>
        /// The rooms background layer.
        /// </summary>
        public int BL;

        /// <summary>
        /// The rooms background image.
        /// </summary>
        public int BI;

        /// <summary>
        /// The rooms entry tile.
        /// </summary>
        public string E;

        /// <summary>
        /// The array of furniture in the room.
        /// </summary>
        public FurnictureArrayPacket[] F;
    }

    /// <summary>
    /// Represents room rights.
    /// </summary>
    public class RoomRightsPacket
    {
        /// <summary>
        /// The user can move furni.
        /// </summary>
        public bool MF;

        /// <summary>
        /// The user can pickup furni.
        /// </summary>
        public bool PF;

        /// <summary>
        /// The user can return furni to its owner.
        /// </summary>
        public bool RF;

        /// <summary>
        /// The user can drop furni.
        /// </summary>
        public bool DF;

        /// <summary>
        /// The user can activate furni.
        /// </summary>
        public bool AF;

        /// <summary>
        /// The user can kick other users.
        /// </summary>
        public bool K;

        /// <summary>
        /// The user can ban other users.
        /// </summary>
        public bool B;
    }
}
