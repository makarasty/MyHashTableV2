#nullable disable
namespace MyHashTableV2;

public class HashTableTests
{
	[Theory]
	[InlineData(typeof(DivisionHash))]
	[InlineData(typeof(MultiplicationHash))]
	public void Search_WithExistingKey_ReturnsProduct(Type hashFunctionType)
	{
		// Arrange
		IHashFunction hf = (IHashFunction)Activator.CreateInstance(hashFunctionType);
		var table = new HashTable(hf);
		var product = new Product { Index = 1, Name = "Apple", Count = 10 };
		table.Add(product);

		// Act
		var result = table.Search(1);

		// Assert
		Assert.Same(product, result);
	}

	[Theory]
	[InlineData(1, "Cake", 1)]
	[InlineData(2, "Pineapple", 500)]
	[InlineData(3, "Meet", 10)]
	[InlineData(3, "Cat", null)]
	[InlineData(3, "Pancake", 5)]
	public void Add_Get_ReturnsAddedProduct(int index, string name, int count)
	{
		// Arrange
		var hashFunction = new DivisionHash();
		var table = new HashTable(hashFunction);
		var product = new Product { Index = index, Name = name, Count = count };

		// Act
		table.Add(product);
		var result = table.Search(index);

		// Assert
		Assert.Equal(product, result);
	}

	[Theory]
	[InlineData(0, "NVIDIA Geforce RTX 4090", 1)]
	[InlineData(1, "NVIDIA Geforce RTX 3080 TI", 1)]
	[InlineData(2, "Intel Core i9 13900K", 1)]
	[InlineData(3, "Intel Core i5 12400F", 1)]
	[InlineData(4, "Samsung 990 EVO 5.0 NVme SSD", 1)]
	public void AddProduct_RemoveProduct_ShouldBeRemoved(int index, string name, int count)
	{
		// Arrange
		var hashFunction = new DivisionHash();
		var table = new HashTable(hashFunction);
		var product = new Product { Index = index, Name = name, Count = count };
		table.Add(product);

		// Act
		table.Remove(index);

		// Assert
		Assert.Null(table.Search(index));
	}

	[Theory]
	[InlineData(typeof(DivisionHash))]
	[InlineData(typeof(MultiplicationHash))]
	public void Search_WhenNoProduct_ShouldReturnNull(Type hashFunctionType)
	{
		// Arrange
		IHashFunction hf = (IHashFunction)Activator.CreateInstance(hashFunctionType);
		var table = new HashTable(hf);

		// Act
		var result = table.Search(123);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void AddProducts_WithCollisions_ShouldAddAll()
	{
		// Arrange
		var hashFunction = new DivisionHash();
		var table = new HashTable(hashFunction);

		var cat = new Product { Index = 1, Name = "ultramurkator3000", Count = 10 };
		var dog = new Product { Index = 11, Name = "woofWoof", Count = 20 };
		// 1 та 11 дає той самий хеш при діленні на 10

		// Act
		table.Add(cat);
		table.Add(dog);

		// Assert
		Assert.Equal(cat, table.Search(1));
		Assert.Equal(dog, table.Search(11));
	}
}