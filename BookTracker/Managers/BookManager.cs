using System;
using BookTracker.Data;
using BookTracker.Entities;

namespace BookTracker.Managers
{
	public interface IBookManager
	{
        Task<bool> InsertAsync(Book book);

    }
    public class BookManager: IBookManager
    {
        //private readonly BookTrackerContext _context;
        //public BookManager(BookTrackerContext context)
        //{
        //    _context = context;

        //}

        public async Task<bool> InsertAsync(Book book)
        {
            try
            {
                using (var db = new BookTrackerContext())
                {
                    db.Add(book);
                    await db.SaveChangesAsync();
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}

