using Common.DTO;
using DAL;

namespace BLL.Services
{
    public interface IRecepsionistService
    { 
        IEnumerable<Recepsionist>GetRecepsionists();
        void Create(Recepsionist recepsionist);
        void Delete(int id);
        Recepsionist GetById(int id);
        void Update(int id, RecepsionistAddModel model);
    }
    internal class RecepsionistService : IRecepsionistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static List<Recepsionist> _recepsionists=new List<Recepsionist>();
        public RecepsionistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Recepsionist recepsionist)
        {
            var existsRecepsionist = _unitOfWork.RecepsionistRepository.GetByName(recepsionist.Name);
            if(existsRecepsionist !=null)
            {
                throw new Exception("There is an Recepsionist with this name");
            }
            _unitOfWork.RecepsionistRepository.Add(new DAL.Entities.Recepsionist
            {
                Name = recepsionist.Name,
                Email = recepsionist.Email,
                PhoneNumber = recepsionist.PhoneNumber

            });
            _unitOfWork.Commit();
        }

        public void Delete( int id )
        {
            _unitOfWork.RecepsionistRepository.DeleteById(id);
            _unitOfWork.Commit();

        }

        public Recepsionist GetById(int id)
        {
           var recepsionist = _unitOfWork.RecepsionistRepository.GetById(id) ?? throw new Exception("There is no recepsionist with this ID");
            return new Recepsionist
            {
                Id = recepsionist.Id,
                Name = recepsionist.Name,
                Email = recepsionist.Email,
                PhoneNumber = recepsionist.PhoneNumber
            };
        }

        public IEnumerable<Recepsionist> GetRecepsionists()
        {
            return _unitOfWork.RecepsionistRepository.GetAll()
                .Select(r=> new Recepsionist
                {
                    Id = r.Id,
                    Name = r.Name,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber
                }).ToList();
        }

        public void Update(int id, RecepsionistAddModel model)
        {
            var recepsionist = _unitOfWork.RecepsionistRepository.GetById(id);
            recepsionist.Name = model.Name;
            recepsionist.Email = model.Email;
            recepsionist.PhoneNumber = model.PhoneNumber;
            _unitOfWork.Commit();
        }
    }
}
