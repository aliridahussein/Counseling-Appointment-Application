using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public interface IUserCounselorRepository
    {
        UserCounselors Add(UserCounselors userCounselor);
        IEnumerable<UserCounselors> GetAllUserCounselors();
        void Delete(string CounselorId);
        bool exist(string UserId, string counselorId);

        ApplicationUser Update(ApplicationUser UserCounselorsChanges);
        IEnumerable<Ratings> GetRatings(string counselorid);
        void addRate(Ratings rate);
        bool IsRated(string UserId, string CounselorId);
    }
}
