using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System.Security.Cryptography.X509Certificates;

namespace IMS.Plugins.InMemory;

public class InventoryRepository : IInventoryRepository

{
    private readonly List<Inventory> _inventories;

    public InventoryRepository()
    {
        _inventories = new List<Inventory>()
        {
            new Inventory { InventoryId = 1, InventoryName = "Bike Seat", Quantity = 10, Price = 9.5M},
            new Inventory { InventoryId = 2, InventoryName = "Bike Body", Quantity = 7, Price = 25.0M},
            new Inventory { InventoryId = 3, InventoryName = "Bike Wheels", Quantity = 25, Price = 20.0M},
            new Inventory { InventoryId = 4, InventoryName = "Bike Pedals", Quantity = 20, Price = 12.0M},
            new Inventory { InventoryId = 5, InventoryName = "Bike Horn", Quantity = 30, Price = 7.0M},
        };
    }

    public Task AddInventoryAsync(Inventory inventory)
    {
        if (_inventories.Any(item => item.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }
        var maxId = _inventories.Max(item => item.InventoryId);
        inventory.InventoryId = ++maxId;
        _inventories.Add(inventory);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_inventories);
        return _inventories.Where(item => item.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase));  
    }

    public Task<Inventory> GetInventoryByIdAsync(int id)
    {
        var inv = _inventories.FirstOrDefault(item => item.InventoryId == id);

        if (inv == null) return (Task<Inventory>)Task.CompletedTask;

        return Task.FromResult(inv);
    }

    public Task UpdateInventoryAsync(Inventory inventory)
    {
        if (_inventories.Any(item => item.InventoryId != inventory.InventoryId && item.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase)))
        {
            return Task.CompletedTask;
        }
        var inv = _inventories.FirstOrDefault(item => item.InventoryId == inventory.InventoryId);
        if (inv != null)
        {
            inv.InventoryName = inventory.InventoryName;
            inv.Quantity = inventory.Quantity;
            inv.Price = inventory.Price;
        }
        return Task.CompletedTask;
    }
}

