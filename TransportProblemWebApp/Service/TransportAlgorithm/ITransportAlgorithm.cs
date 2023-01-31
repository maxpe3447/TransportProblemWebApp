using TransportProblemWebApp.Model;

namespace TransportProblemWebApp.Service.MinElementAlgorithm
{
    public interface ITransportAlgorithm
    {
        AlgorithmResultViewModel GetResultModel(MatrixViewModel model);

    }
}
