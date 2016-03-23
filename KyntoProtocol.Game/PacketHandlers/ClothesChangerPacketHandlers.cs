using System;
using System.Text;

using KyntoLib.Interfaces.Events;
using KyntoLib.Interfaces.Instances.Rooms.Avatars;
using KyntoLib.Interfaces.Managers;
using KyntoLib.Data;

namespace KyntoProtocol.Game.PacketHandlers
{
    /// <summary>
    /// Handles clothes changer related packets.
    /// </summary>
    public class ClothesChangerPacketHandlers
    {
        /// <summary>
        /// Stores the server manager.
        /// </summary>
        private IServerManager _ServerManager;

        /// <summary>
        /// Initialises this set of packet handlers.
        /// </summary>
        /// <param name="ServerInstance">The server instance.</param>
        public ClothesChangerPacketHandlers(IServerManager ServerInstance)
        {
            // Store the server instance.
            this._ServerManager = ServerInstance;
        }

        /// <summary>
        /// Handles a avatar changer request packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void AvatarChangerRequestDataPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Create the response.
                AvatarChangerDetailsPacket AvatarChangerDetailsPacket = new AvatarChangerDetailsPacket();
                AvatarChangerDetailsPacket.A = AvatarChangerAvaliableClothes.GetClothesByGender(AvatarChangerAvaliableClothes.FromClothesArray(Params.UserInstance.UserData.UsersClothes), Params.UserInstance.Avatar.Sex);
                AvatarChangerDetailsPacket.C = Params.UserInstance.Avatar.Clothes;
                // Convert to string.
                string AvatarChangerDetailsPacketString = JSON.Serializer<AvatarChangerDetailsPacket>(AvatarChangerDetailsPacket);
                // Send data.
                Params.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.AvatarChangerDetails + AvatarChangerDetailsPacketString + ProtocolInfo.Terminator, true);
            }
        }

        /// <summary>
        /// Handles a avatar changer save packet.
        /// </summary>
        /// <param name="Sender">The object that sent this event.</param>
        /// <param name="Args">The event arguments.</param>
        public void AvatarChangerSaveDataPacketHandler(object Sender, EventArgs Args)
        {
            // Convert the arguments.
            GameSocketsPacketReceivedEventArguments Params = (GameSocketsPacketReceivedEventArguments)Args;

            // Check the user is already logged in.
            if (Params.UserInstance.UserData.IsLoggedIn && !Params.UserInstance.UserData.IsGuest)
            {
                // Get the packet data.
                AvatarDataPacket PacketModel = JSON.DeSerialize<AvatarDataPacket>(Params.PacketBody);

                // Make sure the packet model is ok.
                if (PacketModel != null)
                {
                    // Store if we own each part.
                    bool OwnsBase = false;
                    bool OwnsHat = true;
                    bool OwnsHair = false;
                    bool OwnsFace = false;
                    bool OwnsShirt = false;
                    bool OwnsPants = false;
                    bool OwnsAccessories = true;
                    bool OwnsShoes = true;

                    // Look through all the users owned clothes, searching for the requested parts.
                    for (int i = 0; i < Params.UserInstance.UserData.UsersClothes.Count; i++)
                    {
                        // Check if they are any of the specified parts.
                        switch (Params.UserInstance.UserData.UsersClothes[i].Type)
                        {
                            case "base":
                                // The base part.
                                if (Params.UserInstance.UserData.UsersClothes[i].ItemId == PacketModel.B)
                                    OwnsBase = true;

                                // Finish.
                                break;

                            case "hair":
                                // The hair part.
                                if (Params.UserInstance.UserData.UsersClothes[i].ItemId == PacketModel.H)
                                    OwnsHair = true;

                                // Finish.
                                break;

                            case "face":
                                // The face part.
                                if (Params.UserInstance.UserData.UsersClothes[i].ItemId == PacketModel.F)
                                    OwnsFace = true;

                                // Finish.
                                break;

                            case "top":
                                // The top part.
                                if (Params.UserInstance.UserData.UsersClothes[i].ItemId == PacketModel.T)
                                    OwnsShirt = true;

                                // Finish.
                                break;

                            case "pants":
                                // The pants part.
                                if (Params.UserInstance.UserData.UsersClothes[i].ItemId == PacketModel.P)
                                    OwnsPants = true;

                                // Finish.
                                break;
                        }
                    }

                    // Finally make sure we own all the clothes.
                    if (OwnsBase && OwnsHat && OwnsHair && OwnsFace && OwnsShirt && OwnsPants && OwnsAccessories && OwnsShoes)
                    {
                        // The user owns those clothes.   
                        AvatarClothes AvatarClothesStructure = new AvatarClothes();
                        AvatarClothesStructure.body = PacketModel.B;
                        AvatarClothesStructure.hair = PacketModel.H;
                        AvatarClothesStructure.hat = PacketModel.Ha;
                        AvatarClothesStructure.face = PacketModel.F;
                        AvatarClothesStructure.top = PacketModel.T;
                        AvatarClothesStructure.pants = PacketModel.P;
                        AvatarClothesStructure.accessories = PacketModel.A;
                        AvatarClothesStructure.shoes = PacketModel.S;
                        // Convert to string.
                        string AvatarClothesModelString = JSON.Serializer<AvatarClothes>(AvatarClothesStructure);
                        // Store
                        Params.UserInstance.Avatar.Clothes = AvatarClothesStructure;
                        Params.UserInstance.UserData.UserInfo.Clothes = AvatarClothesModelString;

                        // Fire the user data updated event.
                        this._ServerManager.EventsManager.CallEvent(EventType.UserDataUpdated, this, new GameSocketsEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance });

                        // If the user is in a room, update the room.
                        if (Params.UserInstance.Avatar.CurrentRoom != null)
                        {
                            // Fire the avatar updated event.
                            this._ServerManager.EventsManager.CallEvent(EventType.AvatarUpdated, this, new GameSocketsEventArguments() { GameSocketConnection = Params.GameSocketConnection, UserInstance = Params.UserInstance });
                        }

                        // Save.
                        Params.UserInstance.UserData.UserInfo.Update().Execute();
                    }
                    else
                    {
                        // The clothes are not owned by this user.
                        // Create the notice packet.
                        MessageBoxPacket NoticePacket = new MessageBoxPacket();
                        NoticePacket.T = "Avatar changing.";
                        NoticePacket.M = "There was an unexpected error while changing your avatar, you new clothes were not saved.";
                        NoticePacket.I = "alert";
                        // Convert to string.
                        string NoticePacketString = JSON.Serializer<MessageBoxPacket>(NoticePacket);
                        // Send data.
                        Params.GameSocketConnection.SendData(ProtocolInfo.ServerPackets.MessageBox + NoticePacketString + ProtocolInfo.Terminator, true);
                    }
                }
            }
        }
    }
}
