using CRUDStoredProcedure.Models;

namespace CRUDStoredProcedure.Interface
{
    public interface IStudent
    {
        public List<Student> GetStudentDetails();

        public void AddStudent(Student student);

        public void UpdateStudentDetails(Student student);

        public Student GetStudentData(int id);

        public void DeleteStudent(int id);
    }
}
