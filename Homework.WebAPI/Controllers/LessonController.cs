
using Homework.Business.Abstract;
using Homework.Business.Validation.Lesson;
using Homework.DAL.Dtos.Lesson;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        [HttpGet("GetLessonList")]
        public async Task<ActionResult<List<GetListLessonDto>>> GetLessonList()
        {
            try
            {
                return Ok(await _lessonService.GetLessonList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetLessonById/{id}")]
        public async Task<ActionResult<GetLessonDto>> GetLessonById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Ders ID' si geçersiz.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var currentLesson = await _lessonService.GetLessonById(id);
                if (currentLesson == null)
                {
                    list.Add("Ders Bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return currentLesson;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddLesson")]
        public async Task<ActionResult<string>> AddLesson(AddLessonDto addLessonDto)
        {
            var list = new List<string>();
            var validator = new LessonAddValidator();
            var validationResults = validator.Validate(addLessonDto);
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
                var result = await _lessonService.AddLesson(addLessonDto);
                if (result > 0)
                {
                    list.Add("Ekleme İşlemi Başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Ekleme İşlemi Başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateLesson/{id}")]
        public async Task<ActionResult<string>> UpdateLesson(int id, UpdateLessonDto updateLessonDto)
        {
            var list = new List<string>();
            var validator = new LessonUpdateValidator();
            var validationResults = validator.Validate(updateLessonDto);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add("Güncelleme İşlemi Başarısız.");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _lessonService.UpdateLesson(id,updateLessonDto);
                if (result>0)
                {
                    list.Add("Güncelleme İşlemi Başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Güncelleme İşlemi Başarısız");
                    return Ok(new { code = StatusCode(1001), message = list, type = "success" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteLesson/{id}")]
        public async Task<ActionResult<string>> DeleteLesson(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _lessonService.DeleteLesson(id);
                if (result>0)
                {
                    list.Add("Silme İşlemi Başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Silme İşlemi Başarısız");
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
