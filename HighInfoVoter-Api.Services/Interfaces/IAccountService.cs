using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;

namespace HighInfoVoter_Api.Services.Interfaces
{
    public interface IAccountService
    {
        int Create(AccountAddRequest model);
        Account SelectByEmail(string email);
        bool VerifyPassword(AccountAddRequest request);
    }
}
