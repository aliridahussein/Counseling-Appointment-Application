using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class MockCategoryRepository:ICategoryRepository
    {
        private List<Category> _categoryList;
        public MockCategoryRepository()
        {
            _categoryList = new List<Category>() {
                new Category
                {
                    categoryName ="Business",
                    categoryDescription="Expert advice in business setup,we provide Business Services, Individual Services, Wealth Management Service, Specialist Services",
                    CategoryPhoto="~/Images/success.jpg"
                },
                 new Category
                {
                    categoryName ="Lifestyle",
                    categoryDescription="Some articles about how to eat healthier,lose weight,stop smoking and alot of other lifestyle advices",
                    CategoryPhoto="~/Images/LifestyleAdvice-770x434.jpg"
                 },
                  new Category
                {
                    categoryName ="Depression",
                    categoryDescription=" You have more power over depression than you may think. These tips can help you feel happier, healthier, and more hopeful",
                    CategoryPhoto="~/Images/depression.jpg"
                  },
            };
        }

        public Category Add(Category category)
        {
            
            _categoryList.Add(category);
            return category;

        }

        public Category GetCategory(string Id)
        {
            return _categoryList.FirstOrDefault(e => e.categoryId == Id);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryList;
        }


    }
}
