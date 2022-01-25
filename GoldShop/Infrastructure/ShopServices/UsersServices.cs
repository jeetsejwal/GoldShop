using GoldShop.Infrastructure.Dtos;
using GoldShop.Infrastructure.IShopServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoldShop.Infrastructure.ShopServices
{
    public class UsersServices : IUsersServices
    {
        private readonly GoldDBContext _db;
        public UsersServices(GoldDBContext db)
        {
            _db = db;
        }
        // Login Method()
        public async Task<UsersDto> LoginUser(LoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password))
                return null;
            try
            {
                var userInformation = await (from s in _db.Users
                                             where s.UserName == dto.UserName && s.Password == dto.Password
                                             select new UsersDto()
                                             {
                                                 UserId = s.UserId,
                                                 FullName = s.FirstName + " " + s.LastName,
                                                 Email = s.Email,
                                             }).SingleOrDefaultAsync();

                    // check if user not exists
                    if (userInformation == null)
                        return null;
                    return userInformation;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        // Gold Discount calculator Method()
        public async Task<int> GoldDiscountCalculator(GoldCalculatorDto dto)
        {
            if (dto.GoldWeight <= 0 || dto.GoldAmount <= 0)
                return 0;
            try
            {
                int discount = (Convert.ToInt32(dto.Discount) * (dto.GoldAmount * dto.GoldWeight)) / 100;
                return await Task.FromResult((dto.GoldAmount * dto.GoldWeight) - discount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
