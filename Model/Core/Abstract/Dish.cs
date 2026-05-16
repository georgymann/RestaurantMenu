namespace Model.Core.Abstract;

public abstract class Dish
{
    protected int _id;
    protected string _name;
    protected double _price;
    protected string _description;
    protected string _category;
    protected bool _isAvailable;
    protected static int _nextId = 100001;

    public int ID => _id;
    public string Name => _name;
    public double Price => _price;
    public string Description => _description;
    public string Category => _category;
    public bool IsAvailable => _isAvailable;

    protected Dish()
    {
        _name = string.Empty;
        _price = 0;
        _description = string.Empty;
        _category = string.Empty;
        _isAvailable = true;
        _id = _nextId++;
    }
    
    protected Dish(string name, double price, string description, string category)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название блюда не может быть пустым.");
        }

        if (price < 0)
        {
            throw new ArgumentException("Цена блюда не может быть отрицательной.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Описание блюда не может быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Категория блюда не может быть пустой.");
        }
        
        _name = name;
        _price = price;
        _description = description;
        _category = category;
        _isAvailable = true;
        _id = _nextId++;
    }

    public abstract string GetDishType();
    
    public virtual string GetInfo()
    {
        return $"{GetDishType()} | {Name} | {Category} | {Price}";
    }
    
    public override string ToString()
    {
        return GetInfo();
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Название блюда не может быть пустым.");
        }

        _name = newName;
    }

    public void ChangePrice(double newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Цена не может быть отрицательной");
        }
        
        _price = newPrice;
    }
    
    public void ChangeDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
        {
            throw new ArgumentException("Описание не может быть пустым");
        }

        _description = newDescription;
    }
    
    public void ChangeCategory(string newCategory)
    {
        if (string.IsNullOrWhiteSpace(newCategory))
        {
            throw new ArgumentException("Категория блюда не может быть пустой.");
        }

        _category = newCategory;
    }
    
    public void Enable()
    {
        _isAvailable = true;
    }
    
    public void Disable()
    {
        _isAvailable = false;
    }
}