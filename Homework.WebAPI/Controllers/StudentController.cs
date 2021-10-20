using Homework.Business.Abstract;
using Homework.Business.Validation.Student;
using Homework.DAL.Dtos.Student;
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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetStudentList")]
        public async Task<ActionResult<List<GetListStudentDto>>> GetStudentList()
        {
            try
            {
                return Ok(await _studentService.GetStudenList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<ActionResult<GetStudentDto>> GetStudentById(int id)
        {
            var list = new List<string>();

            if (id <= 0)
            {
                list.Add("Öğrenci id geçersiz.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            try
            {
                var currentStudent = await _studentService.GetStudentById(id);
                if (currentStudent == null)
                {
                    list.Add("Öğrenci bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return currentStudent;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddStudent")]
        public async Task<ActionResult<string>> AddStudent(AddStudentDto addStudentDto)
        {
            var list = new List<string>();
            var validator = new StudentAddValidator();
            var validationResults = validator.Validate(addStudentDto);

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
                var result = await _studentService.AddStudent(addStudentDto);
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

        [HttpPut("UpdateStudent/{id}")]
        public async Task<ActionResult<string>> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            var list = new List<string>();
            var validator = new StudentUpdateValidator();
            var validationResults = validator.Validate(updateStudentDto);
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
                var result = await _studentService.UpdateStudent(id, updateStudentDto);
                if (result > 0)
                {
                    list.Add("Güncelleme İşlemi Başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Öğrenci bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Güncelleme İşlemi Başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<ActionResult<string>> DeleteStudent(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _studentService.DeleteStudent(id);
                if (result > 0)
                {
                    list.Add("Silme İşlemi Başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Öğrenci bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Silme İşlemi Başarısız.");
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
