using DAL.Repositories;

namespace DAL;

public interface IUnitOfWork
{
    #region Repositories
    IPackageRepository PackageRepository { get; }
    IRecepsionistRepository RecepsionistRepository { get; }
    IPostalOfficeRepository PostalOfficeRepository { get; }
    #endregion

    void Commit();
}

internal class UnitOfWork : IUnitOfWork
{
    private readonly DeliverySystemContext _dbContext;
    public UnitOfWork(DeliverySystemContext context)
    {
        _dbContext = context;
        _packageRepository = new PackageRepository(context);
        _recepsionistRepository = new RecepsionistRepository(context);
        _postalofficeRepository = new PostalOfficeRepository(context);
    }
    private IPackageRepository _packageRepository;
    public IPackageRepository PackageRepository
    {
        get
        {
            _packageRepository ??= new PackageRepository(_dbContext);
            return _packageRepository;
        }
    }
    private IRecepsionistRepository _recepsionistRepository;
    public IRecepsionistRepository RecepsionistRepository
    {
        get 
        {
            _recepsionistRepository ??= new RecepsionistRepository(_dbContext);
            return _recepsionistRepository;
        }
    }
    private IPostalOfficeRepository _postalofficeRepository;
    public IPostalOfficeRepository PostalOfficeRepository
    {
        get
        {
            _postalofficeRepository ??= new PostalOfficeRepository(_dbContext);
            return _postalofficeRepository;
        }
    }
    public void Commit() 
    {
        _dbContext.SaveChanges();
    }
}
