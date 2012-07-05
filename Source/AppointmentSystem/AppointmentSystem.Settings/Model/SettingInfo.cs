
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Configuration;
using AppointmentSystem.Settings.FileConfiguration;
using AppointmentSystem.Data.Bases;

namespace AppointmentSystem.Settings.Model
{
    public class SettingInfo
    {
        #region members
        private Guid id;
        private CSharpTypes type;
        private string code;
        private string valueString;
        private byte[] valueBinary;
        #endregion members

        #region properties
        public Guid Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public CSharpTypes Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        public string ValueString
        {
            get { return this.valueString; }
            set { this.valueString = value; }
        }
        public byte[] ValueBinary
        {
            get { return this.valueBinary; }
            set { this.valueBinary = value; }
        }
        #endregion properties

        #region ctors
        public SettingInfo()
        {
        }
        public SettingInfo(Guid id, CSharpTypes type, string code, string valueString, byte[] valueBinary)
        {
            this.id = id;
            this.type = type;
            this.code = code;
            this.valueString = valueString;
            this.valueBinary = valueBinary;
        }
        #endregion ctors

        #region static methods
        /// <summary>
        /// I will use simple model for dealing with backend database
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static SettingInfo LoadSetting(string code)
        {
            //using (DbConnection connection = new SqlConnection(CustomSettings.Current.ConnectionString))
            NetTiersServiceSection section = (NetTiersServiceSection)WebConfigurationManager.GetSection("AppointmentSystem.Data");
            string Connectionstringname = section.Providers["SqlNetTiersProvider"].Parameters["connectionStringName"];
            using (DbConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings[Connectionstringname].ConnectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText =
                                    @"IF OBJECT_ID('[dbo].[tblSettings]','U') IS NULL
                                        CREATE TABLE [dbo].[tblSettings](
                                        [ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_tblSettings_id]  DEFAULT (newid()),
                                        [Type] [tinyint] NOT NULL,
                                        [Code] [nvarchar](50) COLLATE Cyrillic_General_CI_AS NOT NULL,
                                        [ValueString] [nvarchar](max) COLLATE Cyrillic_General_CI_AS NULL,
                                        [ValueBinary] [varbinary](max) NULL,
                                        CONSTRAINT [PK_tblSettings] PRIMARY KEY CLUSTERED 
                                        (
                                        [ID] ASC
                                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                        ) ON [PRIMARY]
                                    ";
                command.ExecuteNonQuery();
                command.CommandText = "SELECT ID, Type, Code, ValueString, ValueBinary FROM tblSettings WHERE Code = @Code";
                DbParameter param = command.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "Code";
                param.Value = code;
                command.Parameters.Add(param);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new SettingInfo(
                                reader.GetGuid(0),
                                (CSharpTypes)reader.GetByte(1),
                                reader.GetString(2),
                                reader.IsDBNull(3) ? String.Empty : reader.GetString(3),
                                reader.IsDBNull(4) ? null : (byte[])reader["ValueBinary"]
                            );
                    }
                }
                connection.Close();
            }
            return null;
        }
        /// <summary>
        /// I will use simple model for dealing with backend database
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static void SaveSetting(SettingInfo setting)
        {
            //using (DbConnection connection = new SqlConnection(CustomSettings.Current.ConnectionString))
            NetTiersServiceSection section = (NetTiersServiceSection)WebConfigurationManager.GetSection("AppointmentSystem.Data");
            string Connectionstringname = section.Providers["SqlNetTiersProvider"].Parameters["connectionStringName"];
            using (DbConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings[Connectionstringname].ConnectionString))
            using (DbCommand command = connection.CreateCommand())
            {
                connection.Open();
                if (setting.id == Guid.Empty)
                {
                    setting.Id = Guid.NewGuid();

                    command.CommandText =
                                        @"INSERT INTO tblSettings (Id, Type, Code, ValueString, ValueBinary)
                                        VALUES (@ID, @Type, @Code, @ValueString, @ValueBinary)";
                }
                else
                {
                    command.CommandText =
                                        @"UPDATE tblSettings 
                                        SET Type = @Type, Code = @Code, ValueString = @ValueString, ValueBinary = @ValueBinary
                                        WHERE ID = @ID";
                }

                #region params

                DbParameter param = command.CreateParameter();
                param.DbType = System.Data.DbType.Guid;
                param.ParameterName = "ID";
                param.Value = setting.Id;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.DbType = System.Data.DbType.Byte;
                param.ParameterName = "Type";
                param.Value = (byte)setting.Type;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "Code";
                param.Value = setting.Code;
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.DbType = System.Data.DbType.String;
                param.ParameterName = "ValueString";
                if (String.IsNullOrEmpty(setting.ValueString))
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = setting.ValueString;
                }
                command.Parameters.Add(param);

                param = command.CreateParameter();
                param.DbType = System.Data.DbType.Binary;
                param.ParameterName = "ValueBinary";
                if (setting.ValueBinary == null)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = setting.ValueBinary;
                }
                command.Parameters.Add(param);

                #endregion params

                command.ExecuteNonQuery();
            }
        }
        #endregion static methods
    }
}