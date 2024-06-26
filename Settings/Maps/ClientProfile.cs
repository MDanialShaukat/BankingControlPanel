using AutoMapper;
using BankingControlPanel.Dtos;
using BankingControlPanel.Models;

namespace BankingControlPanel.Settings.Maps
{
    public class ClientProfile : Profile
    {
        #region Initialisation
        public ClientProfile()
        {
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
        #endregion
    }
}
