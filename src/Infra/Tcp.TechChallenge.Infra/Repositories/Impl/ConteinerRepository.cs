namespace Tcp.TechChallenge.Infra.Repositories.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Tcp.TechChallenge.Infra.Context;
    using Tcp.TechChallenge.Infra.Models;

    public class ConteinerRepository : IConteinerRepository
    {
        private readonly ConteinerContext _context;
        public ConteinerRepository(ConteinerContext context)
            => _context = context;

        public IList<Conteiner> FindAll()
            => _context.Conteiners.ToList();

        public Conteiner FindById(string identifier)
            => _context.Conteiners
                .Where(c => c.Numero.Equals(identifier)).FirstOrDefault();

        public async Task Save(Conteiner conteiner)
        {
            _context.Conteiners.Add(conteiner);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Conteiner conteiner)
        {
            _context.Entry(conteiner).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(string identifier)
        {
            var result = FindById(identifier);
            var entry = _context.Remove(result);
            await _context.SaveChangesAsync();
            return entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached;
        }
    }
}
