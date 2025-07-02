using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.IRepo;
using Repository.Model;

namespace Repository.Repo
{
    public class HandbagRepository : IHandbagRepository
    {
        private readonly Summer2025handbagdbContext _context;
        public HandbagRepository(Summer2025handbagdbContext context) => _context = context;

        public IEnumerable<Handbag> GetAll() => _context.Handbags.Include(h => h.Brand).ToList();

        public Handbag GetById(int id) => _context.Handbags.Include(h => h.Brand).FirstOrDefault(h => h.Id == id);

        public void Create(Handbag handbag)
        {
            _context.Handbags.Add(handbag);
            _context.SaveChanges();
        }

        public void Update(Handbag handbag)
        {
            _context.Handbags.Update(handbag);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var h = _context.Handbags.Find(id);
            if (h != null)
            {
                _context.Handbags.Remove(h);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Handbag> Search(string modelName, string material)
        {
            var query = _context.Handbags.Include(h => h.Brand).AsQueryable();
            if (!string.IsNullOrWhiteSpace(modelName)) query = query.Where(h => h.ModelName.Contains(modelName));
            if (!string.IsNullOrWhiteSpace(material)) query = query.Where(h => h.Material.Contains(material));
            return query.ToList();
        }
    }

}
