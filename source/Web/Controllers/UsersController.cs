using DotNetCore.AspNetCore;
using DotNetCore.Objects;
using DotNetCoreArchitecture.Application;
using DotNetCoreArchitecture.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Web
{
    [ApiController]
    [RouteController]
    public class UsersController : BaseController
    {
        private readonly IUserApplicationService _userApplicationService;

        public UsersController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddUserModel addUserModel)
        {
            return Result(await _userApplicationService.AddAsync(addUserModel));
        }

        [AuthorizeEnum(Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Result(await _userApplicationService.DeleteAsync(id));
        }

        [HttpGet("Grid")]
        public async Task<IActionResult> GridAsync([FromQuery]PagedListParameters parameters)
        {
            return Result(await _userApplicationService.ListAsync(parameters));
        }

        [HttpPatch("{id}/Inactivate")]
        public async Task InactivateAsync(long id)
        {
            await _userApplicationService.InactivateAsync(id);
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            return Result(await _userApplicationService.ListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SelectAsync(long id)
        {
            return Result(await _userApplicationService.SelectAsync(id));
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(SignInModel signInModel)
        {
            return Result(await _userApplicationService.SignInAsync(signInModel));
        }

        [HttpPost("SignOut")]
        public async Task SignOutAsync()
        {
            await _userApplicationService.SignOutAsync(new SignOutModel(UserModel.Id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(UpdateUserModel updateUserModel)
        {
            return Result(await _userApplicationService.UpdateAsync(updateUserModel));
        }
    }
}
