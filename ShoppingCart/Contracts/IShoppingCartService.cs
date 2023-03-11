using System;
using System.Collections.Generic;
using ShoppingCart.Model;

namespace ShoppingCart.Contracts
{
    public interface IShoppingCartService
    {
        IEnumerable<ShoppingItem> GetAllItems();
        ShoppingItem Add(ShoppingItem newItem);
        ShoppingItem GetById(Guid id);
        void Remove(Guid id);
    }
}
