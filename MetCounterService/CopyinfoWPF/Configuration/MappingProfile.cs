using AutoMapper;
using CopyinfoWPF.ORM.AsystentDatabase.Entities;
using CopyinfoWPF.ViewModels;

namespace CopyinfoWPF.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Klient, ClientOverviewViewModel>()
                .ForMember(dst => dst.City, opt => opt.MapFrom(src => src.Miejscowosc))
                .ForMember(dst => dst.HouseNumber, opt => opt.ResolveUsing(src => $"{src.NrDomu}" + (string.IsNullOrWhiteSpace(src.NrLokalu) ? string.Empty : $"\\{src.NrLokalu}")))
                .ForMember(dst => dst.PostalCode, opt => opt.MapFrom(src => src.KodPoczt))
                .ForMember(dst => dst.Street, opt => opt.MapFrom(src => src.Ulica))
                .ForMember(dst => dst.PostalCity, opt => opt.MapFrom(src => src.Poczta))
                
                .ForMember(dst => dst.Name, opt => opt.ResolveUsing(src => $"{src.NazwaSkr} {src.Nazwa1}"))
                .ForMember(dst => dst.NIP, opt => opt.MapFrom(src => src.Nip))
                .ForMember(dst => dst.Note, opt => opt.MapFrom(src => src.Opis))
                .ForMember(dst => dst.ServiceAgreement, opt => opt.MapFrom(src => src.UmowaSerwisowa));


            CreateMap<UrzadzenieKlient, DeviceOverviewViewModel>()
                .ForMember(dst => dst.Manufacturer, opt => opt.ResolveUsing(src => src.ModelUrzadzenia?.MarkaUrzadzenia?.Nazwa1))
                .ForMember(dst => dst.SerialNumber, opt => opt.MapFrom(src => src.NrFabryczny))
                .ForMember(dst => dst.InstallationDate, opt => opt.MapFrom(src => src.DataInstalacji))
                .ForMember(dst => dst.Model, opt => opt.ResolveUsing(src => src.ModelUrzadzenia.Nazwa1));

            CreateMap<AdresKlient, DeviceOverviewViewModel>()
                .ForMember(dst => dst.Manufacturer, opt => opt.MapFrom(src => src.Miejscowosc))
                .ForMember(dst => dst.Street, opt => opt.MapFrom(src => src.Ulica))
                .ForMember(dst => dst.PostalCode, opt => opt.MapFrom(src => src.KodPoczt))
                .ForMember(dst => dst.PostalCity, opt => opt.MapFrom(src => src.Poczta))
                .ForMember(dst => dst.HouseNumber, opt => opt.ResolveUsing(src => Helpers.AddressHelpers.AddressHouseNumberToString(src)));
        }
    }
}