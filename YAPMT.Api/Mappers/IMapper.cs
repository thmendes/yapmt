using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YAPMT.Api.Mappers
{
    public interface IMapper<source, destination>
    {
        destination MapProject(source source);
        List<destination> MapProjects(List<source> source);
    }
}
