using YazilimKursClient.Areas.Admin.Models;
using YazilimKursClient.Models;

namespace YazilimKursClient.Repository
{
    public static class DataRepository
    {
        private static readonly List<StudentModel> _students = [
            new StudentModel() { Id=1, Name="Yasin",Surname="Karali",Username="yaska",Password="aret" },
            new StudentModel() { Id=2, Name="Merih",Surname="Karali",Username="meri",Password="aret" },
            new StudentModel() { Id=3, Name="Merve",Surname="Karali",Username="mery",Password="aret" },
            new StudentModel() { Id=4, Name="Zehra",Surname="Karali",Username="zeh",Password="aret" },
            new StudentModel() { Id=5, Name="Abdullah",Surname="Karali",Username="abd",Password="aret" },
            new StudentModel() { Id=6, Name="Oğuzhan",Surname="Karali",Username="rabbit",Password="aret" }
    ];

        private static readonly List<CourseViewModel> _courses = [
            new CourseViewModel(){
                Id = 1,Name="Html",Description="html temelleri",Price=250,TeacherName="Murat Yücedağ",ImageUrl="abc.png"
            },
            new CourseViewModel(){
                Id = 2,Name="Css",Description="css temelleri",Price=485,TeacherName="Engin Niyazi Ergül",ImageUrl="abcd.png"
            },
            new CourseViewModel(){
                Id = 3,Name="Javascript",Description="JS temelleri",Price=120,TeacherName="Fahrettin Erdinç",ImageUrl="abcde.png"
            },
			new CourseViewModel(){
				Id = 4,Name="Java",Description="Java temelleri",Price=720,TeacherName="Murat Güven",ImageUrl="abcrdef.png"
			},
			new CourseViewModel(){
				Id = 5,Name="Phyton",Description="Phyton temelleri",Price=350,TeacherName="Murat Yücedağ",ImageUrl="abctdef.png"
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

        public static List<StudentModel> GetStudents()
        {
            //Fake course bilgileri döndürecek
            return _students;

        }
        public static StudentModel GetStudent(int id)
        {
            //Fake bir course bilgisi döndürecek
            return _students.Where(x => x.Id == id).FirstOrDefault();

        }


    }
}
