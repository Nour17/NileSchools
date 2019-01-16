using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NileSchool.API.Data;
using NileSchool.API.Data.Interfaces;
using NileSchool.API.Dtos;
using NileSchool.API.Models;

namespace NileSchool.API.Controllers.EmployesControllers
{
    [Authorize(Roles="8")]
    [ApiController]
    [Route("api/[controller]")]
    public class HumanResourcesController : ControllerBase
    {
        private readonly IUserRepository _authRepo;
        private readonly IUserTypeRepository _userRepo;

        public HumanResourcesController(IUserRepository authRepo, IUserTypeRepository userRepo)
        {
            _authRepo = authRepo;
            _userRepo = userRepo;
        }

        [HttpPost("makeUser")]
        public async Task<IActionResult> MakeUser(UserForRegisterDto newUser){
            
            newUser.Username = newUser.Username.ToLower();

            if(await _authRepo.UserExists(newUser.Username)){
                return BadRequest("Username already exists");
            }

            var userToCreate = new User{
                Name = newUser.Name,
                Username = newUser.Username,
                Email = newUser.Email,
                SSN = newUser.SSN,
                UserTypeId = newUser.UserType
            };
            
            var createdUser = await _authRepo.MakeUser(userToCreate, newUser.Password);

            return StatusCode(201);
        }

        // [HttpPost("addTeacherToDepartment")]
        // public async Task<IActionResult> AssignTeacherToDeparmtnet(TeacherToDepartmentDto teacherToDepartment){
            
        //     var teacher = await _authRepo.GetUser(teacherToDepartment.TeacherId);
        //     if(teacher == null){
        //         return BadRequest("Teacher do not exist");
        //     }

        //     if(!await _userRepo.isTeacher(teacherToDepartment.TeacherId)){
        //         return BadRequest("Given User is not Teacher");
        //     }

        //     teacher
        // }
    }
}