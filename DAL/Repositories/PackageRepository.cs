using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Drawing.BarCodes;

namespace DAL.Repositories
{
    public interface IPackageRepository : IBaseRepository<Package, int>
    {
        Task<IEnumerable<Package>> GetAll();
        Task<IEnumerable<Package>> FindByBarcode(string barcode);
        Task<IEnumerable<Package>> FindByZipCode(string zipCode);
        Task<bool> ExistsByBarcode(string barcode);
    }

    internal class PackageRepository : BaseRepository<Package, int>, IPackageRepository
    {
        public PackageRepository(DeliverySystemContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Package>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Package>> FindByBarcode(string barcode)
        {

            return await _noTrackingDbSet.Where(x => x.Name.ToLower().Contains(barcode.ToLower()) ||
                                     x.BarcodePackage.ToLower().Contains(barcode.ToLower()) ||
                                     x.SenderInformation.ToLower().Contains(barcode.ToLower()) ||
                                     x.Email.ToLower().Contains(barcode.ToLower()) ||
                                     x.SentAddress.ToLower().Contains(barcode.ToLower()) ||
                                     x.SentZipCode.ToString().Contains(barcode.ToLower()) ||
                                     x.DestinationAddress.ToLower().Contains(barcode.ToLower()) ||
                                     x.DestinationZipCode.ToString().Contains(barcode.ToLower()))
                         .ToListAsync();

        }

        public async Task<IEnumerable<Package>> FindByZipCode(string zipCode)
        {

            return await _noTrackingDbSet.Where(x => x.Name.ToLower().Contains(zipCode.ToLower()) ||
                                     x.BarcodePackage.ToLower().Contains(zipCode.ToLower()) ||
                                     x.SenderInformation.ToLower().Contains(zipCode.ToLower()) ||
                                     x.Email.ToLower().Contains(zipCode.ToLower()) ||
                                     x.SentAddress.ToLower().Contains(zipCode.ToLower()) ||
                                     x.SentZipCode.ToString().Contains(zipCode.ToLower()) ||
                                     x.DestinationAddress.ToLower().Contains(zipCode.ToLower()) ||
                                     x.DestinationZipCode.ToString().Contains(zipCode.ToLower()))
                         .ToListAsync();
        }

        public async Task<bool> ExistsByBarcode(string barcode)
        {
            return await _noTrackingDbSet.AnyAsync(x =>
                x.BarcodePackage.ToLower() == barcode.ToLower());
        }
    }
}
