using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;
using HighInfoVoter_Api.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HighInfoVoter_Api.Services
{
    public class ConfigService : IConfigService
    {
        private IDataProvider _dataProvider;

        public ConfigService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int Create(ConfigAddRequest model)
        {
            int id = 0;
            string currentUser = "Admin"; //Bad developer, implement authentication ASAP
            _dataProvider.ExecuteNonQuery(
                "Config_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    paramCol.Add(param);
                    paramCol.AddWithValue("@ConfigKey", model.ConfigKey);
                    paramCol.AddWithValue("@ConfigValue", model.ConfigValue);
                    paramCol.AddWithValue("@ModifiedBy", currentUser);
                },
                returnParameters: delegate (SqlParameterCollection paramCol)
                {
                    id = (int)paramCol["@Id"].Value;
                }
            );
            return id;
        }

        public List<Config> GetAll()
        {
            List<Config> result = new List<Config>();
            _dataProvider.ExecuteCmd(
                "Config_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Config model = Mapper(reader);
                    result.Add(model);
                }
            );
            return result;
        }

        public Config GetById(int id)
        {
            Config model = null;
            _dataProvider.ExecuteCmd(
                "Config_SelectById",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                },

                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = Mapper(reader);
                }
            );
            return model;
        }

        public Config GetByKey(string key)
        {
            Config model = null;
            _dataProvider.ExecuteCmd(
                "Config_SelectByKey",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@ConfigKey", key);
                },

                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = Mapper(reader);
                }
            );
            return model;
        }

        public void Update(ConfigUpdateRequest model)
        {
            string currentUser = "Admin"; //Bad developer, implement authentication ASAP
            _dataProvider.ExecuteNonQuery(
                "Config_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", model.Id);
                    paramCol.AddWithValue("@ConfigKey", model.ConfigKey);
                    paramCol.AddWithValue("@ConfigValue", model.ConfigValue);
                    paramCol.AddWithValue("@ModifiedBy", currentUser);
                }
            );
        }

        public void Delete(int id)
        {
            _dataProvider.ExecuteNonQuery(
                "Config_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
            );
        }

        private static Config Mapper(IDataReader reader)
        {
            Config model = new Config();
            int index = 0;
            model.Id = reader.GetInt32(index++);
            model.ConfigKey = reader.GetString(index++);
            model.ConfigValue = reader.GetString(index++);
            model.CreatedDate = reader.GetDateTime(index++);
            model.ModifiedDate = reader.GetDateTime(index++);
            model.ModifiedBy = reader.GetString(index++);
            return model;
        }
    }
}
