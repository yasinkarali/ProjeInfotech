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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<StudentDto>> AddAsync(AddStudentDto addStudentDto)
        {
            Student student = _mapper.Map<Student>(addStudentDto);
            Student createStudent = await _studentRepository.CreateAsync(student);
            if (createStudent == null)
            {
                return Response<StudentDto>.Fail("Bir sorun oluştu", 404);
            }
            StudentDto studentDto = _mapper.Map<StudentDto>(createStudent);
            return Response<StudentDto>.Success(studentDto, 201);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var deletedCourse = await _studentRepository.GetByIdAsync(id);
            if (deletedCourse == null)
            {
                return Response<NoContent>.Fail("Böyle bir kurs bulunamadı", 404);
            }
            await _studentRepository.DeleteAsync(deletedCourse);
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<List<StudentDto>>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            if (students == null)
            {
                return Response<List<StudentDto>>.Fail("Hiç öğrenci bulunamadı", 0);
            }
            var allStudents = _mapper.Map<List<StudentDto>>(students);
            return Response<List<StudentDto>>.Success(allStudents, 200);
        }

        public async Task<Response<StudentDto>> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return Response<StudentDto>.Fail("Bu kullanıcı ismine sahip bir öğrenci bulunamadı", 404);
            }
            var studentDto = _mapper.Map<StudentDto>(student);
            return Response<StudentDto>.Success(studentDto, 200);
        }

        public async Task<Response<StudentDto>> GetStudentByUsernameAsync(string username)
        {
            var student = await _studentRepository.GetStudentByUsernameAsync(username);
            if (student == null)
            {
                return Response<StudentDto>.Fail("Bu kullanıcı ismine sahip bir öğrenci bulunamadı", 404);
            }
            var studentDto = _mapper.Map<StudentDto>(student);
            return Response<StudentDto>.Success(studentDto, 200);

        }

        public async Task<Response<StudentDto>> UpdateAsync(EditStudentDto editStudentDto)
        {
            Student editedStudent = _mapper.Map<Student>(editStudentDto);
            if (editedStudent == null)
            {
                return Response<StudentDto>.Fail("Güncellenecek öğrenci kaydı bulunamadı", 404);
            }
            await _studentRepository.UpdateAsync(editedStudent);
            StudentDto studentDto = _mapper.Map<StudentDto>(editedStudent);
            return Response<StudentDto>.Success(studentDto, 200);
        }
    }
}