# Mutex.Data

Mutex.Data is built on top of System.Data. It has split into multiple packages to avoid Entourage antipattern and to follow Package Design Principles. Mutex.Data.Core contains only interfaces therefore it's 100% abstract. You should depend this in your libraries. In code you will use Dependency Inversion Principle. When you do this then you even cannot use new keyword for creating Connection etc. objects.

Reference Mutex.Data in your application and then compose the service graph. If you use SQL Server then you can reference Mutex.Data.SqlClient which creates SqlConnection objects when they are needed. If you use other relational database then you need to reference Mutex.Data.Client and implement one method in one interface. It's very easy.

## Supported features

- Follows Dependency Inversion Principle therefore you can use object composition, (e.g. with Decorator Pattern) and decide which implementation to use.
- Uses Factory Pattern therefore you have a possibility to inject your code, e.g. for tracking SQL performance.
- Reading values and objects from record is very easy:
	- var value = record.GetDateTime(2); // by index
	- var value = record.GetDateTime("name");
	- var customer = record.Get<Customer\>();
- Supports Nullable types:
	- int? value = null; command.Parameters.Add("name", value);
	- var value = record.GetNullableInt32("name");
	- var value = record.GetNullable<int\>("name");
- Does not require Attributes or public parameterless constructor.
	- If your class requires parameters in constructor then you can use a different overload of the methods and create the object yourself. Then set the values of the  properties your self, or pass the object back to Mutex.Data and let it set the values of the properties.
- Opens the connection and closes it automatically when needed.
- When you need to support multiple databases in you application, it's easy to do.

## How to use it?

Assume we have following classes.

```
public class Customer
{
	public int? Id { get; private set; }
	public string Name { get; set; }
}
```

An easy way to read and save a Customer is done this way:

```
public CustomerRepository : ICustomerRepository
{
	protected readonly ISql Sql;
	
	public CustomerRepository(ISql sql)
	{
		this.Sql = sql;
	}
	
	public Customer Get(int id)
	{
		return this.Sql.Query<Customer>("select * from Customer where Id = @Id",
				new { Id = id }).FirstOrDefault();
	}
	
	public void Update(Customer customer)
	{
		this.Sql.Execute("update Customer set Name = @Name where Id = @Id",
				customer);
	}
}
```

This is just one way and Mutex.Data contains more lower level approaches as well.
