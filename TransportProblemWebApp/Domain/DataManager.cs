using TransportProblemWebApp.Domain.Repositories.InformationFieldRepository;
using TransportProblemWebApp.Domain.Repositories.TextFieldRepository;

namespace TransportProblemWebApp.Domain
{
    public class DataManager
    {
        public ITextFieldRepository TextField { get; set; }
        public IInformationFieldRepository InformationField { get; set; }
        public DataManager(ITextFieldRepository textFieldRepository, 
                           IInformationFieldRepository informationFieldRepository)
        {
            TextField = textFieldRepository;
            InformationField = informationFieldRepository;
        }
                    
    }
}
