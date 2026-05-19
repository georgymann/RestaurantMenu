using Model.Core.Interfaces;
using Model.Core.Menus;

namespace Model.Core.Abstract;

public abstract partial class Establishment : ISeasonalMenu
{
    protected Guid _id;
    protected string _name;
    protected string _address;
    protected string _description;
    protected Menu _menu;
    protected bool _isOpen;
    protected double _rating;

    public Guid ID => _id;
    public string Name => _name;
    public string Address => _address;
    public string Description => _description;
    public Menu Menu => _menu;
    public bool IsOpen => _isOpen;
    public double Rating => _rating;

    protected Establishment()
    {
        _name = string.Empty;
        _address = string.Empty;
        _description = string.Empty;
        _isOpen = true;
        _rating = 0;
        _id = Guid.NewGuid();
        _menu = new Menu();
        _seasonalMenu = new SeasonalMenu();
        _hasSeasonalMenu = false;
    }

    protected Establishment(string name, string address, string description, Menu menu, double rating)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название заведения не может быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Адрес заведения не может быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Описание заведения не может быть пустым.");
        }

        if (rating < 0 || rating > 5)
        {
            throw new ArgumentException("Рейтинг должен быть от 0 до 5.");
        }
        
        _name = name;
        _address = address;
        _description = description;
        _isOpen = true;
        _rating = rating;
        _id = Guid.NewGuid();
        _menu = menu ?? throw new ArgumentNullException(nameof(menu));
        _seasonalMenu = new SeasonalMenu();
        _hasSeasonalMenu = false;
    }

    public abstract string GetEstablishmentType();

    public virtual string GetInfo()
    {
        return $"{GetEstablishmentType()} | {Name} | {Address} | Рейтинг: {Rating}";
    }

    public override string ToString()
    {
        return GetInfo();
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Название заведения не может быть пустым.");
        }

        _name = newName;
    }

    public void ChangeAddress(string newAddress)
    {
        if (string.IsNullOrWhiteSpace(newAddress))
        {
            throw new ArgumentException("Адрес заведения не может быть пустым.");
        }

        _address = newAddress;
    }

    public void ChangeDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
        {
            throw new ArgumentException("Описание заведения не может быть пустым.");
        }

        _description = newDescription;
    }

    public void ChangeMenu(Menu newMenu)
    {
        _menu = newMenu ?? throw new ArgumentNullException(nameof(newMenu));
    }

    public void ChangeRating(double newRating)
    {
        if (newRating < 0 || newRating > 5)
        {
            throw new ArgumentException("Рейтинг должен быть от 0 до 5.");
        }

        _rating = newRating;
    }

    public void Open()
    {
        _isOpen = true;
    }

    public void Close()
    {
        _isOpen = false;
    }
}