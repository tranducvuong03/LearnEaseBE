using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Service.Iservice
{
    public interface IHandbagService
    {
        IEnumerable<Handbag> GetAll();
        Handbag GetById(int id);
        void Create(Handbag handbag);
        void Update(Handbag handbag);
        void Delete(int id);
        IEnumerable<Handbag> Search(string modelName, string material);
    }

}
