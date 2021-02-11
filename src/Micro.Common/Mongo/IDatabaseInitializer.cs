using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Micro.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializerAsync();
    }
}
