﻿using YazilimKursClient.Models;

namespace YazilimKursClient.Repository
{
    public static class DataRepository
    {
        private static readonly List<StudentViewModel> _students = [
            new StudentViewModel() { Id=1, Name="Yasin",Surname="Karali",Username="yaska",Password="aret" },
            new StudentViewModel() { Id=2, Name="Merih",Surname="Karali",Username="meri",Password="aret" },
            new StudentViewModel() { Id=3, Name="Merve",Surname="Karali",Username="mery",Password="aret" },
            new StudentViewModel() { Id=4, Name="Zehra",Surname="Karali",Username="zeh",Password="aret" },
            new StudentViewModel() { Id=5, Name="Abdullah",Surname="Karali",Username="abd",Password="aret" },
            new StudentViewModel() { Id=6, Name="Oğuzhan",Surname="Karali",Username="rabbit",Password="aret" }
    ];

        private static readonly List<CourseViewModel> _courses = [
            new CourseViewModel(){
                Id = 1,Name="Html",Description="html temelleri",Price=250,TeacherName="Murat",ImageUrl="abc.png"
            },
            new CourseViewModel(){
                Id = 2,Name="Css",Description="css temelleri",Price=485,TeacherName="Engin",ImageUrl="abcd.png"
            },
            new CourseViewModel(){
                Id = 3,Name="Javascript",Description="JS temelleri",Price=120,TeacherName="Fahrettin",ImageUrl="abcde.png"
            },
            new CourseViewModel(){
                Id = 4,Name="OOP",Description="OOP temelleri",Price=750,TeacherName="Zeynep",ImageUrl="abcdef.png"
            }
        ];
        public static List<CourseViewModel> GetCourses()
        {
            //Fake course bilgileri döndürecek
            return _courses;

        }

        public static CourseViewModel GetCourse(int id)
        {
            //Fake bir course bilgisi döndürecek
            return _courses.Where(x=>x.Id == id).FirstOrDefault();

        }

        public static List<StudentViewModel> GetStudents()
        {
            //Fake course bilgileri döndürecek
            return _students;

        }
        public static StudentViewModel GetStudent(int id)
        {
            //Fake bir course bilgisi döndürecek
            return _students.Where(x => x.Id == id).FirstOrDefault();

        }


    }
}