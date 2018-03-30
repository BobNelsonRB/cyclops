using Cyclops.Data.Abstractions;
using Cyclops.Data.Common;
using Cyclops.Model;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Cyclops.Data.MySql
{
    public class NoteDataProvider : DataProvider, INoteDataProvider
    {
        private const string idParamname = "@id";
        private const string displayParamname = "@display";
        private const string bodyParamname = "@body";
        private const string dispositionParamname = "@disposition";
        private const string tagsParamname = "@tags";
        private const string createdByParamname = "@createdby";
        private const string modifiedByParamname = "@modifiedby";

        public NoteDataProvider(ILogger<NoteDataProvider> logger,
            IConnectionStringProvider connectionStringProvider)
        {
            Logger = logger ?? throw new NullReferenceException(nameof(logger));
            ConnectionStringProvider = connectionStringProvider ?? throw new NullReferenceException(nameof(connectionStringProvider));
            ConnectionStringKey = "cyclops-data";
        }


        bool INoteDataProvider.Delete(Parameters parameters)
        {
            return Delete(parameters);
        }

        List<Note> INoteDataProvider.Get(Parameters parameters)
        {
            return Get(parameters);
        }

        Note INoteDataProvider.Post(Note model)
        {
            return Post(model);
        }

        Note INoteDataProvider.Put(Note model)
        {
            return Put(model);
        }


        private bool Delete(Parameters parameters)
        {
            return false;
        }

        private List<Note> Get(Parameters parameters)
        {
            var response = ExecuteReader<Note>(GetSqlCommand, Borrow, parameters);
            if (response.IsOkay)
            {
                return response.Items;
            }
            else
            {
                return new List<Note>();
            }
        }
        private void GetSqlCommand(MySqlCommand cmd, Parameters parameters)
        {
            cmd.CommandType = CommandType.Text;
            string key = parameters.GetStrategyKey();
            if (getCommandInitializers.ContainsKey(key))
            {
                getCommandInitializers[key](cmd, parameters);
            }
            else
            {
                GetAll(cmd, parameters);
            }

        }

        private static Dictionary<string, Action<MySqlCommand, Parameters>> getCommandInitializers = 
            new Dictionary<string, Action<MySqlCommand, Parameters>>(StringComparer.OrdinalIgnoreCase)
        {
            { "all",GetAll },
            { "id",GetByIdentifier },
            { "createdby",GetByCreatedBy }
        };

        private static void GetByIdentifier(MySqlCommand cmd, Parameters parameters)
        {
            cmd.CommandText = "SELECT `note`.`Id`,`note`.`Display`,`note`.`Body`,`note`.`Disposition`" +
                ",`note`.`Tags`,`note`.`CreatedBy`,`note`.`ModifiedBy`,`note`.`CreatedAt`,`note`.`ModifiedAt`" +
                "FROM `cyclops`.`note` where `note`.`Id` = " + idParamname;
            cmd.Parameters.AddWithValue(idParamname, parameters.GetValue<string>("id"));
        }

        private static void GetAll(MySqlCommand cmd, Parameters parameters)
        {
            cmd.CommandText = "SELECT `note`.`Id`,`note`.`Display`,`note`.`Body`,`note`.`Disposition`" +
                ",`note`.`Tags`,`note`.`CreatedBy`,`note`.`ModifiedBy`,`note`.`CreatedAt`,`note`.`ModifiedAt`" +
                "FROM `cyclops`.`note`;";
        }

        private static void GetByCreatedBy(MySqlCommand cmd, Parameters parameters)
        {
            cmd.CommandText = "SELECT `note`.`Id`,`note`.`Display`,`note`.`Body`,`note`.`Disposition`" +
                ",`note`.`Tags`,`note`.`CreatedBy`,`note`.`ModifiedBy`,`note`.`CreatedAt`,`note`.`ModifiedAt`" +
                "FROM `cyclops`.`note` where `note`.`createdby` = " + createdByParamname +
                " or `note`.`modifiedby` = " + createdByParamname;
            cmd.Parameters.AddWithValue(createdByParamname, parameters.GetValue<string>("createdby"));
        }


        private Note Post(Note model)
        {
            Note item = null;
            var response = ExecuteReader<Note>(PostSqlCommand, Borrow, model);
            if (response.IsOkay)
            {
                item = response.Model;
            }
            return item;
        }

        private static void PostSqlCommand(MySqlCommand cmd, Note model)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO `cyclops`.`note` (`Id`,`Display`,`Body`," +
                "`Disposition`,`Tags`,`CreatedBy`,`ModifiedBy`) VALUES (" +
                idParamname + "," + displayParamname + "," + bodyParamname + "," +
                displayParamname + "," + tagsParamname + "," + createdByParamname + "," + createdByParamname + ");" +
                "SELECT `note`.`Id`,`note`.`Display`,`note`.`Body`,`note`.`Disposition`" +
                ",`note`.`Tags`,`note`.`CreatedBy`,`note`.`ModifiedBy`,`note`.`CreatedAt`,`note`.`ModifiedAt`" +
                "FROM `cyclops`.`note` where `note`.`NoteId` = LAST_INSERT_ID()";
            cmd.Parameters.AddWithValue(idParamname, model.Id);
            cmd.Parameters.AddWithValue(displayParamname, model.Display);
            cmd.Parameters.AddWithValue(bodyParamname, model.Body);
            cmd.Parameters.AddWithValue(dispositionParamname, model.Disposition);
            cmd.Parameters.AddWithValue(createdByParamname, model.CreatedBy);
            cmd.Parameters.AddWithValue(tagsParamname, String.Join("`", model.Tags));
        }

        private Note Put(Note model)
        {
            Note item = null;
            var response = ExecuteReader<Note>(PutSqlCommand, Borrow, model);
            if (response.IsOkay)
            {
                item = response.Model;
            }
            return item;
        }

        private void PutSqlCommand(MySqlCommand cmd, Note model)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE `cyclops`.`note` SET `Id` = " + idParamname + 
                ",`Display` = " + displayParamname + ",`Body` = " + bodyParamname + 
                ",`Disposition` = " + dispositionParamname + ",`Tags` = " + tagsParamname + 
                ",`ModifiedBy` = " + modifiedByParamname + " WHERE `Id` = " + idParamname +
                "; SELECT `note`.`Id`,`note`.`Display`,`note`.`Body`,`note`.`Disposition`" +
                ",`note`.`Tags`,`note`.`CreatedBy`,`note`.`ModifiedBy`,`note`.`CreatedAt`,`note`.`ModifiedAt`" +
                "FROM `cyclops`.`note` where `note`.`Id` = " + idParamname;
            cmd.Parameters.AddWithValue(idParamname, model.Id);
            cmd.Parameters.AddWithValue(displayParamname, model.Display);
            cmd.Parameters.AddWithValue(bodyParamname, model.Body);
            cmd.Parameters.AddWithValue(dispositionParamname, model.Disposition);
            cmd.Parameters.AddWithValue(modifiedByParamname, model.ModifiedBy);
            cmd.Parameters.AddWithValue(tagsParamname, String.Join("`", model.Tags));
        }

        private void Borrow(MySqlDataReader reader, List<Note> list)
        {
            while (reader.Read())
            {
                Note item = new Note()
                {
                    Id = reader.GetString(reader.GetOrdinal("ID")),
                    Display = reader.GetString(reader.GetOrdinal("Display")),
                    Body = reader.GetString(reader.GetOrdinal("Body")),
                    Disposition = reader.GetString(reader.GetOrdinal("Disposition")),
                    CreatedBy= reader.GetString(reader.GetOrdinal("CreatedBy")),
                    ModifiedBy = reader.GetString(reader.GetOrdinal("ModifiedBy")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    ModifiedAt = reader.GetDateTime(reader.GetOrdinal("ModifiedAt"))
                };
                string tags = reader.GetString(reader.GetOrdinal("Tags"));
                item.Tags = new List<string>(tags.Split(new char[] {'`'}, StringSplitOptions.RemoveEmptyEntries));
                list.Add(item);
            }
        }

    }
}
