using Model.Core.Abstract;
using Model.Core.Interfaces;
namespace Model.Core.Menus;

public partial class Menu : IMenu
{
    protected string _name;
    protected string _description;
    protected List<Dish> _dishes;
    
    public string Name => _name;
    public string Description => _description;
    public Dish[] Dishes => _dishes.ToArray();
    public int Count => _dishes.Count;
    public bool IsEmpty => _dishes.Count == 0;

    public Menu()
    {
        _name = "Основное меню";
        _description = "Меню заведения";
        _dishes = new List<Dish>();
    }
    
    public Menu(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название меню не может быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Описание меню не может быть пустым.");
        }
        
        _name = name;
        _description = description;
        _dishes = new List<Dish>();
    }
    public virtual string GetInfo()
    {
        return $"{Name} | {Description} | Количество блюд: {Count}";
    }

    public override string ToString()
    {
        return GetInfo();
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Название меню не может быть пустым.");
        }

        _name = newName;
    }

    public void ChangeDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
        {
            throw new ArgumentException("Описание меню не может быть пустым.");
        }

        _description = newDescription;
    }

}