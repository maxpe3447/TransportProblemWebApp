using TransportProblemWebApp.Domain.Entities;

namespace TransportProblemWebApp.Domain.Repositories.InformationFieldRepository
{
	public interface IInformationFieldRepository
	{
        IQueryable<InformationField> GetInformationFields();
        InformationField GetInformationFieldById(Guid id);
        void SaveInformationField(InformationField entity);
        void DeleteInformationField(Guid id);
	}
}
