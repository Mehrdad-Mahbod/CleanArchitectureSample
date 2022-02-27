using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.Ioc
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<ReservationDoctorVisitViewModel, ReservationDoctorVisit>()
            //        .ForMember(D => D.PatientExaminations, Option => Option.MapFrom(S => S.PatientExaminations))
            //        .ForMember(D => D.PatientDiagnoses, Option => Option.MapFrom(S => S.PatientDiagnoses))
            //        .ForMember(D => D.PatientsDrugs, Option => Option.MapFrom(S => S.PatientsDrugs))
            //        .ForMember(D => D.TextsPatientsTreatmentPrescriptions, Option => Option.MapFrom(S => S.TextsPatientsTreatmentPrescriptions));
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<MenuViewModel, Menu>().ReverseMap();
            CreateMap<UserRoleViewModel, UserRole>().ReverseMap();
            CreateMap<GeneralOfficeViewModel, GeneralOffice>().ReverseMap();
        }
    }
}
