using Homework.DAL.Dtos.Student;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework.Business.Abstract
{
    public interface IStudentService
    {
        /// <summary>
        /// Bütün öğrencileri listeler
        /// </summary>
        /// <returns></returns>
        public Task<List<GetListStudentDto>> GetStudenList();
        /// <summary>
        /// Idye göre öğrenci listeler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<GetStudentDto> GetStudentById(int id);
        /// <summary>
        /// Öğrenci ekler
        /// </summary>
        /// <param name="addStudentDto"></param>
        /// <returns></returns>
        public Task<int> AddStudent(AddStudentDto addStudentDto);
        /// <summary>
        /// Öğrenci bilgilerini günceller
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateStudentDto"></param>
        /// <returns></returns>
        public Task<int> UpdateStudent(int id, UpdateStudentDto updateStudentDto);
        /// <summary>
        /// Silme işlemini gerçekleştirir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DeleteStudent(int id);
    }
}
