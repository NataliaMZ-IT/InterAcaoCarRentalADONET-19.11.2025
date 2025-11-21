using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Models
{
    public class Category
    {
        public readonly static string INSERTCATEGORY = "INSERT INTO tblCategorias VALUES (@Name, @Description, @DailyRate);";

        public readonly static string SELECTALLCATEGORIES = @"SELECT c.Nome, c.Descricao, c.Diaria 
                                                               FROM tblCategorias";

        public readonly static string SELECTCATEGORYBYNAME = @"SELECT * FROM tblCategorias
                                                               WHERE c.Nome = @Name";

        public readonly static string UPDATECATEGORYBYNAME = "UPDATE tblCategorias WHERE Nome = @Name";

        public readonly static string DELETECATEGORYBYID = "DELETE FROM tblCategorias WHERE CategoriaID = @CategoryID";

        public int CategoryID { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public decimal DailyRate { get; private set; }

        public Category (string name, decimal dailyRate)
        {
            Name = name;
            DailyRate = dailyRate;
        }

        public Category(string name, string? description, decimal dailyRate) : this(name, dailyRate)
        {
            Description = description;
        }

        public void SetCategoryID(int categoryID)
        {
            CategoryID = categoryID; 
        }

        public override string ToString()
        {
            return $"Category: {Name}\nDescription: {Description}\nDaily Rental Rate: {DailyRate}\n";
        }
    }
}
