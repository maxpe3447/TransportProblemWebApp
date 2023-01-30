using TransportProblemWebApp.Domain.Entities;

namespace TransportProblemWebApp.Domain.Repositories.TextFieldRepository
{
	public interface ITextFieldRepository
    {
        IQueryable<TextField> GetTextField();
        TextField GetTextFieldById(Guid id);
        TextField GetTextFieldByCodeWord(string codeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
