using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Models
{
    public class Category
    {
        public int CategoryID { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Diaria { get; private set; }

        // TODO: Create necessary set methods
        // TODO: Create ToString override
        // TODO: Create Controller for Category and make CRUD methods
    }
}
