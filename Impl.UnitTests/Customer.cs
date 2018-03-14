using System.Collections.Generic;

namespace Mutex.Data.Impl.UnitTests
{
    public class Customer
    {
        public Customer()
        {
            this.Address = new Address();
        }

        public Address Address { get; set; }

        public Address[] AddressesArray { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
    }
}
