using Homework.Business.Abstract;
using Homework.Business.Validation.StudentLesson;
using Homework.DAL.Dtos.StudentLesson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLessonController : ControllerBase
    {
        private readonly IStudentLessonService _studentLessonService;
        public StudentLessonController(IStudentLessonService studentLessonService)
        {
            _studentLessonService = studentLessonService;
        }
        [HttpGet("GetStudentLessonList")]
        public async Task<ActionResult<List<GetListStudentLessonDto>>> GetStudentLessonList()
        {
            try
            {
                return Ok(await _studentLessonService.StudentLessonList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetStudentById/{id}")]
        public async Task<ActionResult<GetStudentLessonDto>> GetStudentLessonById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Öğrenci/Ders ID' si geçersiz.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var currentStudentLesson = await _studentLessonService.GetStudentLessonById(id);
                if (currentStudentLesson == null)
                {
                    list.Add("Öğrenci/Ders Bulunamadı.");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                else
                {
                    return currentStudentLesson;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddStudentLesson")]
        public async Task<ActionResult<string>> AddStudentLesson(AddStudentLessonDto addStudentLessonDto)
        {
            var list = new List<string>();
            var validator = new StudentLessonAddValidator();
            var validationResults = validator.Validate(addStudentLessonDto);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _studentLessonService.AddStudentLesson(addStudentLessonDto);
                if (result > 0)
                {
                    list.Add("Öğrenci/Ders Ekleme İşlemi Başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Öğrenci/Ders Ekleme İşlemi Başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateStudentLesson/{id}")]
        public async Task<ActionResult<string>> UpdateStudentLesson(int id, UpdateStudentLessonDto updateStudentLessonDto)
        {
            var list = new List<string>();
            var validator = new StudentLessonUpdateValidator();
            var validationResults = validator.Validate(updateStudentLessonDto);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _studentLessonService.UpdateStudentLesson(id, updateStudentLessonDto);
                if (result > 0)
                {
                    list.Add("Öğrenci/Ders Güncelleme İşlemi Başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Öğrenci/Ders Kaydı Bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Öğrenci/Ders Güncelleme İşlemi Başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteStudentLesson/{id}")]
        public async Task<ActionResult<string>> DeleteStudentLesson(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _studentLessonService.DeleteStudentLesson(id);
                if (result > 0)
                {
                    list.Add("Öğrenci/Ders Silme İşlemi Başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Öğrenci/Ders Bilgisi Bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Öğrenci/Ders Silme İşlemi Başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
