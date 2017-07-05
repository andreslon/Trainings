using AutoMapper;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;

namespace Excelsior.Business.Helpers
{
    public class UserHelper
    {
        public static UserLoginResponseDto UserToLoginDto(UserRequestDto user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequestDto, UserLoginResponseDto>();
            });
            var mapper = config.CreateMapper();
            return (mapper.Map<UserLoginResponseDto>(user));
        }

        public static bool ValidatePassword(Aspnet_Membership aspUser, string password, bool checkPin = true)
        {
            var pwd = Security.EncodePassword(password, aspUser.PasswordFormat, aspUser.PasswordSalt);
            if (aspUser.Password != pwd)
            {
                if (checkPin && !string.IsNullOrEmpty(aspUser.MobilePIN))
                {
                    var pin = Security.EncodePIN(password, aspUser.PasswordFormat, aspUser.PasswordSalt);
                    if (aspUser.MobilePIN != pin)
                        return false;
                }
                else
                    return false;
            }
            return true;
        }
    }
}
