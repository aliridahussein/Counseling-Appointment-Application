using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class SqlUserCounselorRepository : IUserCounselorRepository
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public SqlUserCounselorRepository(AppDbContext context,
                                      UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public UserCounselors Add(UserCounselors userCounselor)
        {

            context.UserCounselors.Add(userCounselor);
            context.SaveChanges();
            return userCounselor;
        }
        public IEnumerable<UserCounselors> GetAllUserCounselors()
        {
            return context.UserCounselors;
        }

        public void Delete(string CounselorId)
        {
            var list = context.UserCounselors;
            foreach (var item in list)
            {
                if (item.CounselorId==CounselorId)
                {
                    context.UserCounselors.Remove(item);
                    
                }
            }
            context.SaveChanges();

        }

            public bool exist(string UserId,string counselorId)
            {
                var mylist = context.UserCounselors;
                foreach (var item in mylist)
                {
                    if (item.CounselorId    == counselorId && item.UserID == UserId)
                    {
                        return true;

                    }
                }
            
                return false;

            }

        public IEnumerable<Ratings> GetRatings(string counselorid)
        {
           return context.Ratings;
        }

        public ApplicationUser Update(ApplicationUser UserCounselorsChanges)
        {
            var UserCounselor = context.Attach(UserCounselorsChanges);
            UserCounselor.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UserCounselorsChanges;
        }
       public void addRate(Ratings rate)
        {
            context.Ratings.Add(rate);
            context.SaveChanges();
        }

        public bool IsRated( string UserId, string CounselorId) { 



            var mylist = context.Ratings;
            foreach (var item in mylist)
            {
                if (item.CounselorId    == CounselorId && item.UserId==UserId )
                {
                    return true;
                }
            }
            return false;
        }






    }
}
