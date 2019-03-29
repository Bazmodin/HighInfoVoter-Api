using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;
using HighInfoVoter_Api.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace HighInfoVoter_Api.Services
{
    public class AccountService : IAccountService
    {
        private IDataProvider _dataProvider;

        public AccountService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int Create(AccountAddRequest model)
        {
            int id = 0;
            string password = model.Password;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            this._dataProvider.ExecuteNonQuery(
                "Account_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    paramCol.Add(param);
                    paramCol.AddWithValue("@Email", model.Email);
                    paramCol.AddWithValue("@Salt", salt);
                    paramCol.AddWithValue("@Password", hashedPassword);
                },
                returnParameters: delegate (SqlParameterCollection paramCol)
                {
                    id = (int)paramCol["@Id"].Value;
                }
            );
            return id;
        }

        public Account SelectByEmail(string email)
        {
            Account model = null;
            _dataProvider.ExecuteCmd(
                "Account_SelectByEmail",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Email", email);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = Mapper(reader);
                }
            );
            return model;
        }

        public bool VerifyPassword(AccountAddRequest request)
        {
            bool verified = false;
            Account account = SelectByEmail(request.Email);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, account.Salt);
            verified = BCrypt.Net.BCrypt.Verify(request.Password, hashedPassword);
            return verified;
        }

        private static Account Mapper(IDataReader reader)
        {
            Account model = new Account();
            int index = 0;
            model.Id = reader.GetInt32(index++);
            model.Email = reader.GetString(index++);
            model.Salt = reader.GetString(index++);
            model.Password = reader.GetString(index++);
            return model;
        }
    }
}
