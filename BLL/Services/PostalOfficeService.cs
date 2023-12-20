using DAL;
using Common.DTO;
using Microsoft.EntityFrameworkCore;
namespace BLL.Services
{
    public interface IPostalOfficeService
    {
        IEnumerable<PostalOffice> GetPostalOffices();
        void Create(PostalOffice postalOffice);
        void Delete(int id);
        PostalOffice GetById(int id);
        void Update(int id, AddPostalOfficeModel model);
    }
    public class PostalOfficeService : IPostalOfficeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<PostalOffice> _dbSet;
        private static List<PostalOffice> _postalOffice = new List<PostalOffice>();
        //{
        //    new PostalOffice { PostalOfficeId=1, OfficeName="Office NR1", Location = "Zone 1", Address = "St. Address", PhoneNumber = " 123"}
        //};
        public PostalOfficeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PostalOffice> GetPostalOffices() 
        {
            return _unitOfWork.PostalOfficeRepository.GetAll()
            .Select(x => new PostalOffice
             {
                PostalOfficeId = x.PostalOfficeId,
                OfficeName = x.OfficeName,
                Location = x.Location,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber
             }).ToList();
        }
        public void Create(PostalOffice postalOffice)
        {
            var existsPostalOffice = _unitOfWork.PostalOfficeRepository.GetByName(postalOffice.OfficeName);
            if (existsPostalOffice != null)
            {
                throw new Exception("There is an existing Office by this name");
            }
            _unitOfWork.PostalOfficeRepository.Add(new DAL.Entities.PostalOffice
            {
                OfficeName = postalOffice.OfficeName,
                Location = postalOffice.Location,
                Address = postalOffice.Address,
                PhoneNumber = postalOffice.PhoneNumber
            });
            _unitOfWork.Commit();
        }
        public void Delete(int id)
        {
            _unitOfWork.PostalOfficeRepository.DeleteById(id);
            _unitOfWork.Commit();
        }
        public PostalOffice GetById(int id)
        {
            var dbPostalOffice = _unitOfWork.PostalOfficeRepository.GetById(id) ?? throw new Exception("There is no PostOffice with this ID");
            return new PostalOffice
            {
                PostalOfficeId= dbPostalOffice.PostalOfficeId,
                OfficeName = dbPostalOffice.OfficeName,
                Address = dbPostalOffice.Address,
                Location= dbPostalOffice.Location,
                PhoneNumber = dbPostalOffice.PhoneNumber
                     
            };
        }

        public void Update(int id, AddPostalOfficeModel model)
        {
            var postalOffice = _unitOfWork.PostalOfficeRepository.GetById(id);
            postalOffice.OfficeName = model.OfficeName;
            postalOffice.Location = model.Location;            
            postalOffice.Address = model.Address;
            postalOffice.PhoneNumber = model.PhoneNumber;
            _unitOfWork.Commit();
        }
    }
}
