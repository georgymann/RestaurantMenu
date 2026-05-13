using Model.Core.Abstract;
namespace Model.Core.Interfaces;

public interface IMenu
{
    Dish[] Dishes { get; }
    
    void AddDish(Dish dish);
    void RemoveDish(Dish dish);
}