using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookTracker.Managers
{
	public interface ICategoryManager
	{
		Task<IEnumerable<SelectListItem>> GetBookCategorySelectListItemsAsync();

    }

	public class CategoryManager: ICategoryManager
	{
		public CategoryManager()
		{
		}

        public async Task<IEnumerable<SelectListItem>> GetBookCategorySelectListItemsAsync()
        {
			try
			{
				var categorySelectListItems = new List<SelectListItem>();
				//we need to retrieve category type from database now we are trying hard coding.
				Dictionary<int, string> categories = new Dictionary<int, string>
				{
					{ 1,"Suspense" },
                    { 2,"Comedy" },
                    { 3,"Romance" },
                    { 4,"Drama" },
                    { 5,"Thriller" },

                };
				if (categories == null) return categorySelectListItems;
			
				foreach (var item in categories)
				{
					categorySelectListItems.Add(new SelectListItem
					{
						Text= item.Value,
						Value= item.Key.ToString()
					});
				}
				return categorySelectListItems;

			}
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }
}

