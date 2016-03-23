using System;
using System.Text;

using KyntoLib.Helpers;
using KyntoLib.Interfaces.Instances.Rooms.Furni;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoProtocol.Game
{
    public class FurnictureArrayPacket
    {
        /// <summary>
        /// Convert a furni array into a packet array.
        /// </summary>
        /// <param name="FurniArray">The array of items.</param>
        /// <returns>The array of packets.</returns>
        public static FurnictureArrayPacket[] FromArray(IFurni[] FurniArray)
        {
            // Create the buffer.
            FurnictureArrayPacket[] InventoryItemsBuffer = new FurnictureArrayPacket[FurniArray.Length];

            // Add items too the buffer.
            for (int i = 0; i < InventoryItemsBuffer.Length; i++)
            {
                // Add the item.
                InventoryItemsBuffer[i] = new FurnictureArrayPacket();
                InventoryItemsBuffer[i].I = FurniArray[i].ItemData.Id;
                InventoryItemsBuffer[i].RID = FurniArray[i].ItemData.RoomId;
                InventoryItemsBuffer[i].S = FurniArray[i].ItemData.StackNr;
                InventoryItemsBuffer[i].H = FurniArray[i].ItemData.Rotation;
                InventoryItemsBuffer[i].T = FurniArray[i].ItemData.Tile;
                InventoryItemsBuffer[i].F = FurniArray[i].ItemTemplate.Furni;
                // TODO: Alter furni action packet!
                InventoryItemsBuffer[i].A = FurniArray[i].ItemTemplate.Action;


                InventoryItemsBuffer[i].X = Convert.ToBase64String(Encoding.ASCII.GetBytes(FurniArray[i].ItemTemplate.XmlData.ToString()));
                InventoryItemsBuffer[i].J = new SerializableDictionary<string, string>();
                    foreach (IFurniDataDatabaseTable data in FurniArray[i].FurniData)
                    {
                        InventoryItemsBuffer[i].J.Add(data.Key, data.Value);
                    }
                InventoryItemsBuffer[i].N = FurniArray[i].ItemTemplate.Name;
                InventoryItemsBuffer[i].D = FurniArray[i].ItemTemplate.Description;
                InventoryItemsBuffer[i].R = FurniArray[i].ItemTemplate.Rows;
                InventoryItemsBuffer[i].C = FurniArray[i].ItemTemplate.Cols;
                InventoryItemsBuffer[i].Ty = FurniArray[i].ItemTemplate.Type;
                InventoryItemsBuffer[i].Ss = FurniArray[i].ItemTemplate.Stacking;
                InventoryItemsBuffer[i].Cl = FurniArray[i].ItemTemplate.Class;
            }

            // Return the buffer.
            return InventoryItemsBuffer;
        }

        public int I;
        public string X;
        public SerializableDictionary<string, string> J;
        public string T;
        public string N;
        public string D;
        public int S;
        public int H;
        public bool A;
        public string F;
        public int RID;
        public int R;
        public int C;
        public string Ty;
        public bool Ss;
        public string Cl;
    }

    /// <summary>
    /// Defines a furniture's type data, such as its description, name etc.
    /// </summary>
    public class FurnictureInfoPacket
    {
        public string X;
        public string N;
        public string D;
        public bool A;
        public int R;
        public int C;
        public string Ty;
        public bool Ss;
        public string Cl;
    }

    /// <summary>
    /// Move an item packet.
    /// </summary>
    public class MoveFurniPacket
    {
        /// <summary>
        /// The id of the item.
        /// </summary>
        public int I;

        /// <summary>
        /// The tile to move the item too.
        /// </summary>
        public string T;

        /// <summary>
        /// The array of items too move.
        /// </summary>
        public MoveFurniPacketArrayItem[] IA;
    }

    /// <summary>
    /// The move item array item.
    /// </summary>
    public class MoveFurniPacketArrayItem
    {
        /// <summary>
        /// The id of the item.
        /// </summary>
        public int I;

        /// <summary>
        /// The tile to move the item too.
        /// </summary>
        public string T;
    }

    /// <summary>
    /// The packet to remove an item from a room.
    /// </summary>
    public class RemoveItemPacket
    {
        /// <summary>
        /// The item to remove.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// The response to a move furni packet.
    /// </summary>
    public class MoveFurniResponsePacket
    {
        /// <summary>
        /// The item id.
        /// </summary>
        public int I;

        /// <summary>
        /// The items stack number.
        /// </summary>
        public int S;

        /// <summary>
        /// The tile to move too.
        /// </summary>
        public string T;
    }

    /// <summary>
    /// Request to drop an item.
    /// </summary>
    public class DropFurniRequestPacket
    {
        /// <summary>
        /// The item id to drop.
        /// </summary>
        public int I;

        /// <summary>
        /// The tile to place it on.
        /// </summary>
        public string T;
    }

    /// <summary>
    /// Request to pickup an item.
    /// </summary>
    public class PickFurniRequestPacket
    {
        /// <summary>
        /// The item id to pickup.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// Request to return an item.
    /// </summary>
    public class ReturnFurniRequestPacket
    {
        /// <summary>
        /// The item id to return.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// Request to delete an item.
    /// </summary>
    public class DeleteFurniRequestPacket
    {
        /// <summary>
        /// The item id to delete.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// Request to activate an item.
    /// </summary>
    public class ActivateFurniRequestPacket
    {
        /// <summary>
        /// The items id to activate.
        /// </summary>
        public int I;
    }

    /// <summary>
    /// The activate item response packet.
    /// </summary>
    public class ActivateFurniResponsePacket
    {
        /// <summary>
        /// The items id.
        /// </summary>
        public int I;

        /// <summary>
        /// If the room is activated.
        /// </summary>
        public bool A;
    }
}
