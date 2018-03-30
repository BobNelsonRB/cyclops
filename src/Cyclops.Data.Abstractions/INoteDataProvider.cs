using Cyclops.Data.Common;
using Cyclops.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclops.Data.Abstractions
{
    public interface INoteDataProvider
    {
        bool Delete(Parameters parameters);
        List<Note> Get(Parameters parameters);
        Note Post(Note model);
        Note Put(Note model);
    }
}
