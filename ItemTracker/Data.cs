using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemTracker
{
	public class ItemContext: DbContext
    { 

   
        public ItemContext(DbContextOptions<ItemContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryType> CategoryTypes { get; set; }

        
    }
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string ISBN { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Title { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Author { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string Status { get; set; } //started reading, in progress, completed, got bored and quit
    }

        public class Category
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CategoryType")]
        public int CategoryTypeId { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string NameToken { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
        //since Book uses CategoryId as a foreign key, we need to define a collection of Book.
    }

    public class CategoryType
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
        //since Category uses CategoryType as foreign key, we need to define a collection of Category.
    }
}

