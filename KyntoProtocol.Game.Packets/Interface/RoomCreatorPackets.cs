using System;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// Requests to create a new room.
    /// </summary>
    public class CreateRoom
    {
        /// <summary>
        /// The new rooms username.
        /// </summary>
        public string N;

        /// <summary>
        /// The new rooms description.
        /// </summary>
        public string D;

        /// <summary>
        /// The lock on the new room.
        /// </summary>
        public int L;

        /// <summary>
        /// The rooms number of x tiles.
        /// </summary>
        public int X;

        /// <summary>
        /// The rooms number of y tiles.
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// The room creation response.
    /// </summary>
    public class CreateRoomFailed
    {
        /// <summary>
        /// The error when creating the room.
        /// </summary>
        public string E;
    }

    /// <summary>
    /// Request to edit a room.
    /// </summary>
    public class RoomEditRequest
    {
        /// <summary>
        /// The room id to edit.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// The room data.
    /// </summary>
    public class RoomEditData
    {
        /// <summary>
        /// The error (if any).
        /// </summary>
        public string E;

        /// <summary>
        /// The rooms id.
        /// </summary>
        public int I;

        /// <summary>
        /// The rooms new name.
        /// </summary>
        public string N;

        /// <summary>
        /// The rooms new description.
        /// </summary>
        public string D;

        /// <summary>
        /// The rooms new x size.
        /// </summary>
        public int X;

        /// <summary>
        /// The rooms new y size.
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// The new room data to save.
    /// </summary>
    public class RoomSaveEditsData
    {
        /// <summary>
        /// The rooms id.
        /// </summary>
        public int I;

        /// <summary>
        /// The new rooms id.
        /// </summary>
        public string N;

        /// <summary>
        /// The new rooms description.
        /// </summary>
        public string D;

        /// <summary>
        /// The new rooms x size.
        /// </summary>
        public int X;

        /// <summary>
        /// The new rooms y size.
        /// </summary>
        public int Y;

        /// <summary>
        /// The new rooms lock.
        /// </summary>
        public int L;
    }
}
