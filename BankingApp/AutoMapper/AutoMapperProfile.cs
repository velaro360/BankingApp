using Application.DTO;
using AutoMapper;
using BankingApp.Request;
using Domain.Aggregate.ValueObject;
using AccountAggr = Domain.Aggregate.Account;
using UserAggr = Domain.Aggregate.User;

namespace BankingApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //request -> dto
            CreateMap<AuthRequest, AuthDTO>();
            CreateMap<AddUserRequest, UserDTO>();
            CreateMap<AddAccountRequest, AccountDTO>();
            CreateMap<TransferRequest, TransferDTO>();

            //other
            CreateMap<MoneyVO, BalanceDTO>();
            CreateMap<AccountAggr.Account, AccountDTO>()
                .ForMember(accountDto => accountDto.BalanceMoney, x => x.MapFrom(account => account.Balance.Amount));
            CreateMap<UserAggr.User, UserDTO>()
                .ForMember(userDto => userDto.City, x => x.MapFrom(user => user.Address.City))
                .ForMember(userDto => userDto.Country, x => x.MapFrom(user => user.Address.Country))
                .ForMember(userDto => userDto.ZipCode, x => x.MapFrom(user => user.Address.ZipCode))
                .ForMember(userDto => userDto.Street, x => x.MapFrom(user => user.Address.Street))
                .ForMember(userDto => userDto.FlatNumber, x => x.MapFrom(user => user.Address.FlatNumber));
        }
    }
}
