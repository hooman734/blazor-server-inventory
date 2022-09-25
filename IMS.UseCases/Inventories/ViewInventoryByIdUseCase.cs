using IMS.CoreBusiness;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Inventories;

public class ViewInventoryByIdUseCase : IViewInventoryByIdUseCase
{
	private readonly IInventoryRepository InventoryRepository;

	public ViewInventoryByIdUseCase(IInventoryRepository inventoryRepository)
	{
		InventoryRepository = inventoryRepository;
	}

	public async Task<Inventory> ExecuteAsync(int id)
	{
		return await InventoryRepository.GetInventoryByIdAsync(id);
	}
}
