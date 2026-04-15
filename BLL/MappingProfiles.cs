using DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.ViewModels.MemberViewModels;
using BLL.ViewModels.TrainerViewModels;
using BLL.ViewModels.SessionViewModels;
using BLL.ViewModels.PLanViewModels;

namespace BLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            MapSession();
            MapMember();
            MapTrainer();
            MapPlan();

        }
        private void MapSession()
        {
            //       TSoucrse , TDestination
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.SessionCategory.CategoryName))
                .ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(dest => dest.AvailableSlots, options => options.Ignore());


            CreateMap<CreateSessionViewModel, Session>();


            CreateMap<UpdateSessionViewModel, Session>().ReverseMap();
        }
        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address()
                {
                    City = src.City,
                    Street = src.Street,
                    BuildingNumber = src.BuildingNumber
                }))
                .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecord));



            CreateMap<HealthRecordViewModel, HealthRecord>().ReverseMap();

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));


            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));


            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });


        }
        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));
            CreateMap<Trainer, TrainerViewModel>()
               .ForMember(dest => dest.Address,
               opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpdatedAt = DateTime.Now;
            });
        }

        void MapPlan()
        {
            CreateMap<Plan, PLanViewModel>().ReverseMap();
            CreateMap<Plan, UpdatePlanViewModel>().ReverseMap();
        }
    }
   
}  
   