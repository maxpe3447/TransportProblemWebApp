using Microsoft.EntityFrameworkCore;
using TransportProblemWebApp.Domain.Entities;

namespace TransportProblemWebApp.Domain.Repositories.TextFieldRepository
{
	public class TextFieldRepository :ITextFieldRepository
    {
        private readonly AppDbContext _context;

        public TextFieldRepository(AppDbContext context)
        {
            this._context = context;
        }
        public IQueryable<TextField> GetTextField()
        {
            return _context.TextFields;
        }

        public TextField GetTextFieldById(Guid id)
        {
            return _context.TextFields.FirstOrDefault(x=> x.Id == id);
        }

        public TextField GetTextFieldByCodeWord(string codeWord)
        {
			return _context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }

		public void SaveTextField(TextField entity)
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

        public void DeleteTextField(Guid id)
        {
            _context.TextFields.Remove(new TextField() { Id = id });
            _context.SaveChanges();
        }
    }
}
