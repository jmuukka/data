using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutex.Data.Impl.Tests
{
    public interface ITest
    {
        int? Id { get; }

        string RequireString20 { get; set; }

        string NullableString10 { get; set; }

        DateTime? RequireDateTime { get; set; }

        DateTime? NullableDateTime { get; set; }

        byte[] NullableBytes { get; set; }

        object NullableVariant { get; set; }
    }
}
