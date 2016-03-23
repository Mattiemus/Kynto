using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;

namespace KyntoDatabase.MySQL.Tables
{

    public class MembersDatabaseTable : IMembersDatabaseTable
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// The id of this member.
        /// </summary>
        private int _Id = -1;

        /// <summary>
        /// The id of this member.
        /// </summary>
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        /// <summary>
        /// The username of this member.
        /// </summary>
        private string _Username;

        /// <summary>
        /// The username of this member.
        /// </summary>
        public string Username
        {
            get
            {
                return this._Username;
            }
            set
            {
                this._Username = value;
            }
        }

        /// <summary>
        /// The password for this member.
        /// </summary>
        private string _Password;

        /// <summary>
        /// The password for this member.
        /// </summary>
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        /// <summary>
        /// The email address for this member.
        /// </summary>
        private string _Email;

        /// <summary>
        /// The email address for this member.
        /// </summary>
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }

        /// <summary>
        /// The timestamp when this member was created.
        /// </summary>
        private int _Created;

        /// <summary>
        /// The timestamp when this member was created.
        /// </summary>
        public int Created
        {
            get
            {
                return this._Created;
            }
            set
            {
                this._Created = value;
            }
        }

        /// <summary>
        /// Weather this member is activated.
        /// </summary>
        private bool _Activated;

        /// <summary>
        /// Weather this member is activated.
        /// </summary>
        public bool Activated
        {
            get
            {
                return this._Activated;
            }
            set
            {
                this._Activated = value;
            }
        }

        /// <summary>
        /// Weather we can email this member.
        /// </summary>
        private bool _Automail;

        /// <summary>
        /// Weather we can email this member.
        /// </summary>
        public bool Automail
        {
            get
            {
                return this._Automail;
            }
            set
            {
                this._Automail = value;
            }
        }

        /// <summary>
        /// The rank id for this member.
        /// </summary>
        private int _RankId;

        /// <summary>
        /// The rank id for this member.
        /// </summary>
        public int RankId
        {
            get
            {
                return this._RankId;
            }
            set
            {
                this._RankId = value;
            }
        }

        /// <summary>
        /// The ip address where this member last logged in at.
        /// </summary>
        private string _Ip;

        /// <summary>
        /// The ip address where this member last logged in at.
        /// </summary>
        public string Ip
        {
            get
            {
                return this._Ip;
            }
            set
            {
                this._Ip = value;
            }
        }

        /// <summary>
        /// The last timestamp when this member was online.
        /// </summary>
        private int _LastOnline;

        /// <summary>
        /// The last timestamp when this member was online.
        /// </summary>
        public int LastOnline
        {
            get
            {
                return this._LastOnline;
            }
            set
            {
                this._LastOnline = value;
            }
        }

        /// <summary>
        /// Weather or not this member is banned.
        /// </summary>
        private bool _Ban;

        /// <summary>
        /// Weather or not this member is banned.
        /// </summary>
        public bool Ban
        {
            get
            {
                return this._Ban;
            }
            set
            {
                this._Ban = value;
            }
        }

        /// <summary>
        /// The ban id that relates to this members ban.
        /// </summary>
        private int _BanId;

        /// <summary>
        /// The ban id that relates to this members ban.
        /// </summary>
        public int BanId
        {
            get
            {
                return this._BanId;
            }
            set
            {
                this._BanId = value;
            }
        }

        /// <summary>
        /// The clothes this member is wearing.
        /// </summary>
        private string _Clothes;

        /// <summary>
        /// The clothes this member is wearing.
        /// </summary>
        public string Clothes
        {
            get
            {
                return this._Clothes;
            }
            set
            {
                this._Clothes = value;
            }
        }

        /// <summary>
        /// The sex of this member.
        /// </summary>
        private string _Sex;

        /// <summary>
        /// The sex of this member.
        /// </summary>
        public string Sex
        {
            get
            {
                return this._Sex;
            }
            set
            {
                this._Sex = value;
            }
        }

        /// <summary>
        /// The active badge this member is wearing.
        /// </summary>
        private int _ActiveBadge;

        /// <summary>
        /// The active badge this member is wearing.
        /// </summary>
        public int ActiveBadge
        {
            get
            {
                return this._ActiveBadge;
            }
            set
            {
                this._ActiveBadge = value;
            }
        }

        /// <summary>
        /// The motto of this member.
        /// </summary>
        private string _Motto;

        /// <summary>
        /// The motto of this member.
        /// </summary>
        public string Motto
        {
            get
            {
                return this._Motto;
            }
            set
            {
                this._Motto = value;
            }
        }

        /// <summary>
        /// The number of silver blocks this member has.
        /// </summary>
        private int _BlocksSilver;

        /// <summary>
        /// The number of silver blocks this member has.
        /// </summary>
        public int BlocksSilver
        {
            get
            {
                return this._BlocksSilver;
            }
            set
            {
                this._BlocksSilver = value;
            }
        }

        /// <summary>
        /// Initialises this database table.
        /// <param name="DatabaseHandler">The database handler.</param>
        public MembersDatabaseTable(MySQLDatabase DatabaseHandler)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
        }

        /// <summary>
        /// Initialises this database table.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler.</param>
        /// <param name="Data">The data stores in this table.</param>
        public MembersDatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
            FromDictionary(Data);
        }

        /// <summary>
        /// Fills this table with data from a dictionary.
        /// </summary>
        /// <param name="Data">The dictionary containing the data for this table.</param>
        public void FromDictionary(Dictionary<string, object> Data)
        {
            // Process the data.
            foreach (KeyValuePair<string, object> DataObject in Data)
            {
                switch (DataObject.Key)
                {
                    case MembersTableFields.Id:
                        this._Id = (int)DataObject.Value;
                        break;

                    case MembersTableFields.Username:
                        this._Username = (string)DataObject.Value;
                        break;

                    case MembersTableFields.Password:
                        this._Password = (string)DataObject.Value;
                        break;

                    case MembersTableFields.Email:
                        this._Email = (string)DataObject.Value;
                        break;

                    case MembersTableFields.Created:
                        this._Created = (int)DataObject.Value;
                        break;

                    case MembersTableFields.Activated:
                        this._Activated = (bool)DataObject.Value;
                        break;

                    case MembersTableFields.Automail:
                        this._Automail = (bool)DataObject.Value;
                        break;

                    case MembersTableFields.RankId:
                        this._RankId = (int)DataObject.Value;
                        break;

                    case MembersTableFields.Ip:
                        this._Ip = (string)DataObject.Value;
                        break;

                    case MembersTableFields.LastOnline:
                        this._LastOnline = (int)DataObject.Value;
                        break;

                    case MembersTableFields.Ban:
                        this._Ban = (bool)DataObject.Value;
                        break;

                    case MembersTableFields.BanId:
                        this._BanId = (int)DataObject.Value;
                        break;

                    case MembersTableFields.Clothes:
                        this._Clothes = (string)DataObject.Value;
                        break;

                    case MembersTableFields.Sex:
                        this._Sex = (string)DataObject.Value;
                        break;

                    case MembersTableFields.ActiveBadge:
                        this._ActiveBadge = (int)DataObject.Value;
                        break;

                    case MembersTableFields.Motto:
                        this._Motto = (string)DataObject.Value;
                        break;

                    case MembersTableFields.BlocksSilver:
                        this._BlocksSilver = (int)DataObject.Value;
                        break;

                    default:
                        throw new Exception("Unknown database column \"" + DataObject.Key + "\"");
                }
            }
        }

        /// <summary>
        /// Creates a query that will update this table.
        /// </summary>
        public IDatabaseQuery Update()
        {
            if (this._Id == -1)
            {
                throw new Exception("This row has not been set an id, an update query cannot be generated.");
            }

            // Create the update query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Members)
                .Update()
                .Set(MembersTableFields.Username, this._Username)
                .Set(MembersTableFields.Password, this._Password)
                .Set(MembersTableFields.Email, this._Email)
                .Set(MembersTableFields.Created, this._Created)
                .Set(MembersTableFields.Activated, this._Activated)
                .Set(MembersTableFields.Automail, this._Automail)
                .Set(MembersTableFields.RankId, this._RankId)
                .Set(MembersTableFields.Ip, this._Ip)
                .Set(MembersTableFields.LastOnline, this._LastOnline)
                .Set(MembersTableFields.Ban, this._Ban)
                .Set(MembersTableFields.BanId, this._BanId)
                .Set(MembersTableFields.Clothes, this._Clothes)
                .Set(MembersTableFields.Sex, this._Sex)
                .Set(MembersTableFields.ActiveBadge, this._ActiveBadge)
                .Set(MembersTableFields.Motto, this._Motto)
                .Set(MembersTableFields.BlocksSilver, this._BlocksSilver)
                .Where(MembersTableFields.Id, DatabaseComparison.Equals, this._Id);
        }

        /// <summary>
        /// Creates a query that will save this table
        /// </summary>
        public IDatabaseQuery Save()
        {
            // Insert this into the database.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Members)
                .Insert()
                .Values(MembersTableFields.Username, this._Username)
                .Values(MembersTableFields.Password, this._Password)
                .Values(MembersTableFields.Email, this._Email)
                .Values(MembersTableFields.Created, this._Created)
                .Values(MembersTableFields.Activated, this._Activated)
                .Values(MembersTableFields.Automail, this._Automail)
                .Values(MembersTableFields.RankId, this._RankId)
                .Values(MembersTableFields.Ip, this._Ip)
                .Values(MembersTableFields.LastOnline, this._LastOnline)
                .Values(MembersTableFields.Ban, this._Ban)
                .Values(MembersTableFields.BanId, this._BanId)
                .Values(MembersTableFields.Clothes, this._Clothes)
                .Values(MembersTableFields.Sex, this._Sex)
                .Values(MembersTableFields.ActiveBadge, this._ActiveBadge)
                .Values(MembersTableFields.Motto, this._Motto)
                .Values(MembersTableFields.BlocksSilver, this._BlocksSilver);
        }

        /// <summary>
        /// Creates a query that will delete this table.
        /// </summary>
        public IDatabaseQuery Delete()
        {
            if (this._Id == -1)
            {
                throw new Exception("This row has not been set an id, a delete query cannot be generated.");
            }

            // Create the delete query.
            return this._DatabaseHandler.CreateQuery(DatabaseTables.Members)
                .Delete()
                .Where(MembersTableFields.Id, DatabaseComparison.Equals, this._Id);
        }
    }
}
