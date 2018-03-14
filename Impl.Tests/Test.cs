using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutex.Data.Impl.Tests
{
    public class Test : ITest
    {
        public int? Id { get; private set; }

        public string RequireString20 { get; set; }

        public string NullableString10 { get; set; }

        public DateTime? RequireDateTime { get; set; }

        public DateTime? NullableDateTime { get; set; }

        public byte[] NullableBytes { get; set; }

        public object NullableVariant { get; set; }
    }
}
