using Microsoft.EntityFrameworkCore;
using TransportProblemWebApp.Domain.Entities;

namespace TransportProblemWebApp.Domain.Repositories.InformationFieldRepository
{
	public class InformationFieldRepository:IInformationFieldRepository
    {
        private readonly AppDbContext _context;

        public InformationFieldRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<InformationField> GetInformationFields()
        {
            return _context.InformationFields;
        }

        public InformationField GetInformationFieldById(Guid id)
        {
            return _context.InformationFields.FirstOrDefault(x => x.Id == id);
        }

        public void SaveInformationField(InformationField entity)
        {
            if (entity.Id == default)
            {
                _context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            _context.SaveChanges();
        }

        public void DeleteInformationField(Guid id)
        {
            _context.InformationFields.Remove(new InformationField() { Id = id });
            _context.SaveChanges();
        }
    }
}
