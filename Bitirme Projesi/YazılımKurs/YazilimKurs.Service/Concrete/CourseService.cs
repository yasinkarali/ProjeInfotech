using System.Security.AccessControl;
using AutoMapper;
using Mapster;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Concrete
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<Response<CourseDto>> AddAsync(AddCourseDto addCourseDto)
        {
            Course course = _mapper.Map<Course>(addCourseDto);

            Course createdCourse = await _courseRepository.CreateAsync(course);
            if (createdCourse == null)
            {
                return Response<CourseDto>.Fail("Veritabanına kayıt işlemi gerçekleştirelemedi", 404);
            }

            CourseDto courseDto = _mapper.Map<CourseDto>(createdCourse);
            //courseDto.TeacherName = "test";
            return Response<CourseDto>.Success(courseDto, 201);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var deletedCourse = await _courseRepository.GetByIdAsync(id);
            if (deletedCourse == null)
            {
                return Response<NoContent>.Fail("Böyle bir kurs bulunamadı", 404);
            }
            await _courseRepository.DeleteAsync(deletedCourse);
            return Response<NoContent>.Success(200);

        }

        public async Task<Response<List<CourseDto>>> GetActiveCoursesAsync()
        {
            var courses = await _courseRepository.GetActiveCoursesAsync();
            if (courses == null)
            {
                return Response<List<CourseDto>>.Fail("Aktif kurs bulunamadı", 404);
            }
            var coursesList = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(coursesList, 200);

        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {

            var courses = await _courseRepository.GetAllAsync();

            if (courses == null || courses.Count == 0)
            {
                return Response<List<CourseDto>>.Fail("Hiç kayıt bulunmuyor", 404);
            }

            List<CourseDto> courseDtoList = _mapper.Map<List<CourseDto>>(courses);

            return Response<List<CourseDto>>.Success(courseDtoList, 200);
        }

        // public Response<List<CourseDto>> GetAllByTeacherId(int id)
        // {
        //     var courses = _courseRepository.GetAllWithFilter(o => o.TeacherId == id);
        //     if (courses == null)
        //     {
        //         return Response<List<CourseDto>>.Fail("Bu id'li kurs bulunamadı", 404);
        //     }
        //     List<CourseDto> coursesDto = _mapper.Map<List<CourseDto>>(courses);

        //     return Response<List<CourseDto>>.Success(coursesDto, 200);

        // }

        public async Task<Response<CourseDto>> GetByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return Response<CourseDto>.Fail("Bu id'li kurs bulunamadı", 404);
            }
            CourseDto courseDto = _mapper.Map<CourseDto>(course);

            return Response<CourseDto>.Success(courseDto, 200);

        }

        public async Task<Response<List<CourseDto>>> GetCoursesWithTeacherIdAsync(int id)
        {
            var courses = await _courseRepository.GetCoursesWithTeacherIdAsync(id);
            if(courses == null)
            {
                return Response<List<CourseDto>>.Fail("Bu id'li öğretmene ait kurslar bulunamadı",404);
            }
            var coursesList = _mapper.Map<List<CourseDto>>(courses);
            return Response<List<CourseDto>>.Success(coursesList, 200);


        }

        public async Task<Response<CourseDto>> UpdateAsync(EditCourseDto editCourseDto)
        {
            Course editedCourse = _mapper.Map<Course>(editCourseDto);
            if (editedCourse == null)
            {
                return Response<CourseDto>.Fail("Güncellenecek kurs bulunamadı", 404);
            }
            await _courseRepository.UpdateAsync(editedCourse);
            CourseDto courseDto = _mapper.Map<CourseDto>(editedCourse);
            return Response<CourseDto>.Success(courseDto, 200);
        }


    }
}