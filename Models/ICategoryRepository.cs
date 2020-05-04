using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public interface ICategoryRepository
    {
         Category Add(Category category);
         Category GetCategory(string Id);
        IEnumerable<Category> GetAllCategories();
    }
}
