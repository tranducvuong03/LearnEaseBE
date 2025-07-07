using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository;
using Service.Iservice;

namespace Service.Service
{
    public class HandbagService : IHandbagService
    {
        private readonly IHandbagRepository _repo;
        public HandbagService(IHandbagRepository repo) => _repo = repo;

        public IEnumerable<Handbag> GetAll() => _repo.GetAll();
        public Handbag GetById(int id) => _repo.GetById(id);
        public void Create(Handbag handbag) => _repo.Create(handbag);
        public void Update(Handbag handbag) => _repo.Update(handbag);
        public void Delete(int id) => _repo.Delete(id);
        public IEnumerable<Handbag> Search(string modelName, string material) => _repo.Search(modelName, material);
    }

}
