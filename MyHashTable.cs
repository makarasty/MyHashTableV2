namespace MyHashTableV2;

public interface IHashFunction
{
	int CalculateHash(int key);
}

public class DivisionHash : IHashFunction
{
	public int CalculateHash(int key)
	{
		return key % 10;
	}
}

public class MultiplicationHash : IHashFunction
{
	public int CalculateHash(int key)
	{
		return key * 7 % 10;
	}
}

public class HashTable
{
	private LinkedList<Product>[] buckets;
	private IHashFunction hashFunction;

	public HashTable(IHashFunction hashFunction)
	{
		this.hashFunction = hashFunction;
		buckets = new LinkedList<Product>[10];
		for (int i = 0; i < buckets.Length; i++)
		{
			buckets[i] = new LinkedList<Product>();
		}
	}

	public Product? Search(int key)
	{
		int hash = hashFunction.CalculateHash(key);
		LinkedList<Product> bucket = buckets[hash];
		foreach (Product product in bucket)
		{
			if (product.Index == key)
			{
				return product;
			}
		}
		return null;
	}

	public void Add(Product product)
	{
		int hash = hashFunction.CalculateHash(product.Index);
		LinkedList<Product> bucket = buckets[hash];
		bucket.AddLast(product);
	}

	public void Remove(int key)
	{
		int hash = hashFunction.CalculateHash(key);
		LinkedList<Product> bucket = buckets[hash];
		foreach (Product product in bucket)
		{
			if (product.Index == key)
			{
				bucket.Remove(product);
				return;
			}
		}
	}
}

public class Product
{
	public string Name { get; set; } = "Any product";
	public int Index { get; set; }
	public int Count { get; set; }
}