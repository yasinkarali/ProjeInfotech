using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Concrete
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<Response<TeacherDto>> AddAsync(AddTeacherDto addTeacherDto)
        {
            Teacher teacher = _mapper.Map<Teacher>(addTeacherDto);
            Teacher createTeacher = await _teacherRepository.CreateAsync(teacher);
            if (createTeacher == null)
            {
                return Response<TeacherDto>.Fail("Bir sorun oluştu", 404);
            }
            TeacherDto teacherDto = _mapper.Map<TeacherDto>(createTeacher);
            return Response<TeacherDto>.Success(teacherDto, 201);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var deletedTeacher = await _teacherRepository.GetByIdAsync(id);
            if (deletedTeacher == null)
            {
                return Response<NoContent>.Fail("Böyle bir kurs bulunamadı", 404);
            }
            await _teacherRepository.DeleteAsync(deletedTeacher);
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<List<TeacherDto>>> GetAllAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            if (teachers == null)
            {
                return Response<List<TeacherDto>>.Fail("Hiç öğrenci bulunamadı", 0);
            }
            var allTeachers = _mapper.Map<List<TeacherDto>>(teachers);
            return Response<List<TeacherDto>>.Success(allTeachers, 200);
        }

        public async Task<Response<TeacherDto>> GetByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return Response<TeacherDto>.Fail("Bu kullanıcı ismine sahip bir öğretmen bulunamadı", 404);
            }
            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            return Response<TeacherDto>.Success(teacherDto, 200);
        }

     

        public async Task<Response<TeacherDto>> GetTeacherByNameAsync(string name)
        {
           var teacher = await _teacherRepository.GetTeacherByNameAsync(name);
           if (teacher == null)
           {
               return Response<TeacherDto>.Fail("Bu kullanıcı ismine sahip bir öğretmen bulunamadı", 404);
           }
           var teacherDto = _mapper.Map<TeacherDto>(teacher);
           return Response<TeacherDto>.Success(teacherDto, 200);
        }

        public async Task<Response<TeacherDto>> GetTeacherByUsernameAsync(string username)
        {
            var teacher = await _teacherRepository.GetTeacherByUsernameAsync(username);
            if (teacher == null)
            {
                return Response<TeacherDto>.Fail("Bu kullanıcı ismine sahip bir öğretmen bulunamadı", 404);
            }
            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            return Response<TeacherDto>.Success(teacherDto, 200);
        }

        public async Task<Response<TeacherDto>> UpdateAsync(EditTeacherDto editTeacherDto)
        {
            Teacher editedTeacher = _mapper.Map<Teacher>(editTeacherDto);
            if (editedTeacher == null)
            {
                return Response<TeacherDto>.Fail("Güncellenecek öğretmen kaydı bulunamadı", 404);
            }
            await _teacherRepository.UpdateAsync(editedTeacher);
            TeacherDto teacherDto = _mapper.Map<TeacherDto>(editedTeacher);
            return Response<TeacherDto>.Success(teacherDto, 200);
        }
    }
}