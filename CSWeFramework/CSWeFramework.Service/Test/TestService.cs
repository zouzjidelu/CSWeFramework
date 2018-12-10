using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSWeFramework.Service.Test
{
    public class TestService : ITestService
    {
        public string Test()
        {
            return "我是TestService.Test()";
        }
    }

    public class GenericTestService<T> : IGenericTestService<T>
    {
        public string Test(T t)
        {
            return "我是TestService.Test(" + t + ")";
        }
    }
}