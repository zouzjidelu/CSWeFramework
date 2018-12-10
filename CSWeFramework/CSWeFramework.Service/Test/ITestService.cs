using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Service.Test
{
    public interface ITestService
    {
        string Test();
    }
    public interface IGenericTestService<T>
    {
        string Test(T t);
    }
}
