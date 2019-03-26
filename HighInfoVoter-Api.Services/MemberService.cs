using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;
using HighInfoVoter_Api.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HighInfoVoter_Api.Services
{
    public class MemberService : IMemberService
    {
        private IDataProvider _dataProvider;

        public MemberService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int Create(MemberAddRequest model)
        {
            int id = 0;
            _dataProvider.ExecuteNonQuery(
                "Member_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    paramCol.Add(param);
                    paramCol.AddWithValue("@MemberId", model.MemberId);
                    paramCol.AddWithValue("@ApiUri", model.ApiUri);
                    paramCol.AddWithValue("@FirstName", model.FirstName);
                    paramCol.AddWithValue("@MiddleName", model.MiddleName);
                    paramCol.AddWithValue("@LastName", model.LastName);
                    paramCol.AddWithValue("@Name", model.Name);
                    paramCol.AddWithValue("@Gender", model.Gender);
                    paramCol.AddWithValue("@District", model.District);
                    paramCol.AddWithValue("@Party", model.Party);
                    paramCol.AddWithValue("@Role", model.Role);
                    paramCol.AddWithValue("@NextElection", model.NextElection);
                    paramCol.AddWithValue("@FacebookAccount", model.FacebookAccount);
                    paramCol.AddWithValue("@TwitterId", model.TwitterId);
                    paramCol.AddWithValue("@YoutubeId", model.YoutubeId);
                    paramCol.AddWithValue("@PortraitUrl", model.PortraitUrl);
                },
                returnParameters: delegate (SqlParameterCollection paramCol)
                {
                    id = (int)paramCol["@Id"].Value;
                }
            );
            return id;
        }

        public List<Member> GetAll()
        {
            List<Member> result = new List<Member>();
            _dataProvider.ExecuteCmd(
                "Member_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    Member model = Mapper(reader);
                    result.Add(model);
                }
            );
            return result;
        }

        public Member GetById(int id)
        {
            Member model = null;
            _dataProvider.ExecuteCmd(
                "Member_SelectById",
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

        public void Update(MemberUpdateRequest model)
        {
            _dataProvider.ExecuteNonQuery(
                "Member_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", model.Id);
                    paramCol.AddWithValue("@MemberId", model.MemberId);
                    paramCol.AddWithValue("@ApiUri", model.ApiUri);
                    paramCol.AddWithValue("@FirstName", model.FirstName);
                    paramCol.AddWithValue("@MiddleName", model.MiddleName);
                    paramCol.AddWithValue("@LastName", model.LastName);
                    paramCol.AddWithValue("@Name", model.Name);
                    paramCol.AddWithValue("@Gender", model.Gender);
                    paramCol.AddWithValue("@District", model.District);
                    paramCol.AddWithValue("@Party", model.Party);
                    paramCol.AddWithValue("@Role", model.Role);
                    paramCol.AddWithValue("@NextElection", model.NextElection);
                    paramCol.AddWithValue("@FacebookAccount", model.FacebookAccount);
                    paramCol.AddWithValue("@TwitterId", model.TwitterId);
                    paramCol.AddWithValue("@YoutubeId", model.YoutubeId);
                    paramCol.AddWithValue("@PortraitUrl", model.PortraitUrl);
                }
            );
        }

        public void Delete(int id)
        {
            _dataProvider.ExecuteNonQuery(
                "Member_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", id);
                }
            );
        }

        private static Member Mapper(IDataReader reader)
        {
            Member model = new Member();
            int index = 0;
            model.Id = reader.GetInt32(index++);
            model.MemberId = reader.GetString(index++);
            model.ApiUri = reader.GetString(index++);
            model.FirstName = reader.GetString(index++);
            model.MiddleName = reader.GetString(index++);
            model.LastName = reader.GetString(index++);
            model.Name = reader.GetString(index++);
            model.Gender = reader.GetString(index++);
            model.District = reader.GetString(index++);
            model.Party = reader.GetString(index++);
            model.Role = reader.GetString(index++);
            model.NextElection = reader.GetString(index++);
            model.FacebookAccount = reader.GetString(index++);
            model.TwitterId = reader.GetString(index++);
            model.YoutubeId = reader.GetString(index++);
            model.PortraitUrl = reader.GetString(index++);
            return model;
        }
    }
}
