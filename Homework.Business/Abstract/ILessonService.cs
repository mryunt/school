using Homework.DAL.Dtos.Lesson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework.Business.Abstract
{
    public interface ILessonService
    {
        /// <summary>
        /// Bütün dersleri listeler.
        /// </summary>
        public Task<List<GetListLessonDto>> GetLessonList();

        /// <summary>
        /// Id' ye göre ders getirir. 
        /// </summary>
        public Task<GetLessonDto> GetLessonById(int id);

        /// <summary>
        /// Ders ekler.
        /// </summary>
        public Task<int> AddLesson(AddLessonDto addLesson);

        /// <summary>
        /// Ders günceller.
        /// </summary>
        public Task<int> UpdateLesson(int id, UpdateLessonDto updateLesson);

        /// <summary>
        /// Ders siler.
        /// </summary>
        public Task<int> DeleteLesson(int id);

    }
}
