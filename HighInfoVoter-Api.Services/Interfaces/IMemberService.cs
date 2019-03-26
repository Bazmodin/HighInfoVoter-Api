using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;
using System.Collections.Generic;

namespace HighInfoVoter_Api.Services.Interfaces
{
    public interface IMemberService
    {
        int Create(MemberAddRequest model);
        List<Member> GetAll();
        Member GetById(int id);
        void Update(MemberUpdateRequest model);
        void Delete(int id);
    }
}
