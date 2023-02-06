using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportProblemLib.Enums
{
    public enum AlgorithmType
    {
        //SupportPlan
        MINIMAL_ELEMENT,
        NORD_WEST,
        VOGEL_APPROXIMATION,
        DOUBLE_PREFERENCE,

        //Optimisation
        POTENTIAL
    }
}
