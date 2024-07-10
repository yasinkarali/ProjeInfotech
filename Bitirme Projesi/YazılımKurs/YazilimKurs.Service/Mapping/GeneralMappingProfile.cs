using AutoMapper;
using YazilimKurs.Entity.Concrete;
using YazilimKurs.Shared.Dtos;

namespace YazilimKurs.Service.Mapping
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            CreateMap<Course, CourseDto>()
             .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => $"{src.Teacher.Name} {src.Teacher.Surname}"))
            .ReverseMap();

            CreateMap<Course, AddCourseDto>().ReverseMap();
            CreateMap<Course, EditCourseDto>().ReverseMap();

            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Student, AddStudentDto>().ReverseMap();
            CreateMap<Student, EditStudentDto>().ReverseMap();

            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Teacher, AddTeacherDto>().ReverseMap();
            CreateMap<Teacher, EditTeacherDto>().ReverseMap();






        }
    }
}
