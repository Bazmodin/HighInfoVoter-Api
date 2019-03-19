using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;
using System.Collections.Generic;

namespace HighInfoVoter_Api.Services.Interfaces
{
    public interface IConfigService
    {
        int Create(ConfigAddRequest model);
        List<Config> GetAll();
        Config GetById(int id);
        Config GetByKey(string key);
        void Update(ConfigUpdateRequest model);
        void Delete(int id);
    }
}
