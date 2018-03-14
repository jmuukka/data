namespace Mutex.Data.Impl.UnitTests
{
    public class Product : IProduct
    {
        public int? Id { get; internal set; }

        public string Name { get; set; }
    }
}
