using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllSuppliersAsync();
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _supplierRepository.AddSupplierAsync(supplier);
        }
    }
}
