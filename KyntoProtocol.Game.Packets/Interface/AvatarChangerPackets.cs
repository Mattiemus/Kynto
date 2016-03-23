using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoProtocol.Game
{
    /// <summary>
    /// The packet sent when the avatar changer is open.
    /// </summary>
    public class AvatarChangerDetailsPacket
    {
        /// <summary>
        /// The available clothes too the user.
        /// </summary>
        public AvatarChangerAvaliableClothes[] A;

        /// <summary>
        /// The current avatar clothes.
        /// </summary>
        public AvatarClothes C;
    }

    /// <summary>
    /// Represents an available clothing item.
    /// </summary>
    public class AvatarChangerAvaliableClothes
    {
        /// <summary>
        /// Gets an array of packets from a database array.
        /// </summary>
        /// <param name="ClothesDatabaseArray">The clothing database array.</param>
        /// <returns>The array of packets.</returns>
        public static AvatarChangerAvaliableClothes[] FromClothesArray(List<IClothesDatabaseTable> ClothesDatabaseArray)
        {
            // Create the buffer array.
            AvatarChangerAvaliableClothes[] BufferArray = new AvatarChangerAvaliableClothes[ClothesDatabaseArray.Count];

            // Add too the buffer.
            for (int i = 0; i < BufferArray.Length; i++)
            {
                BufferArray[i] = new AvatarChangerAvaliableClothes();
                BufferArray[i].T = ClothesDatabaseArray[i].Type;
                BufferArray[i].I = ClothesDatabaseArray[i].ItemId;
                BufferArray[i].S = ClothesDatabaseArray[i].Sex;
            }

            // Return the buffer.
            return BufferArray;
        }

        /// <summary>
        /// Sorts a list of avaliable clothes by gender.
        /// </summary>
        /// <param name="Avaliable">The avaliable clothes.</param>
        /// <param name="GenderWanted">The gender wanted.</param>
        /// <returns>The list of clothes.</returns>
        public static AvatarChangerAvaliableClothes[] GetClothesByGender(AvatarChangerAvaliableClothes[] Avaliable, string GenderWanted)
        {
            // Create the buffer array.
            List<AvatarChangerAvaliableClothes> BufferArray = new List<AvatarChangerAvaliableClothes>();

            // Add too the buffer.
            for (int i = 0; i < Avaliable.Length; i++)
            {
                if (Avaliable[i].S == GenderWanted)
                {
                    BufferArray.Add(Avaliable[i]);
                }
            }

            // Return the buffer.
            return BufferArray.ToArray();
        }

        /// <summary>
        /// The clothing type.
        /// </summary>
        public string T;

        /// <summary>
        /// The clothing id.
        /// </summary>
        public int I;

        /// <summary>
        /// The clothing sex.
        /// </summary>
        public string S;
    }

    /// <summary>
    /// The avatar update packet.
    /// </summary>
    public class AvatarDataPacket
    {
        /// <summary>
        /// The requested body.
        /// </summary>
        public int B;

        /// <summary>
        /// The requested hat.
        /// </summary>
        public int Ha;

        /// <summary>
        /// The requested hair.
        /// </summary>
        public int H;

        /// <summary>
        /// The requested face.
        /// </summary>
        public int F;

        /// <summary>
        /// The requested top.
        /// </summary>
        public int T;

        /// <summary>
        /// The requested pants.
        /// </summary>
        public int P;

        /// <summary>
        /// The requested accessories.
        /// </summary>
        public int A;

        /// <summary>
        /// The requested shoes.
        /// </summary>
        public int S;
    }
}
