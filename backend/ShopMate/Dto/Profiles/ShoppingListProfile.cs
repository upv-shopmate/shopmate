using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, ShoppingListReadDto>();
        }
    }
}