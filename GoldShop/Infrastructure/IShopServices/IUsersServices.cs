using GoldShop.Infrastructure.Dtos;
using System.Threading.Tasks;

namespace GoldShop.Infrastructure.IShopServices
{
   public interface IUsersServices
    {
        Task<UsersDto> LoginUser(LoginDto dto);
        Task<int> GoldDiscountCalculator(GoldCalculatorDto dto);
    }
}
