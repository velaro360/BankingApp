using Application.DTO;
using AutoMapper;
using BankingApp.Request;

namespace BankingApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthRequest, AuthDTO>();
            CreateMap<AddUserRequest, UserDTO>();
            CreateMap<AddAccountRequest, AccountDTO>();
            CreateMap<TransferRequest, TransferDTO>();
        }
    }
}
