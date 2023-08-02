using System;
using System.Security.Cryptography;
using Delamain_backend.Services.userRequestService;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Delamain_backend.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class userReqmodelController : ControllerBase
    {
            private readonly IuserRequestService _IuserRequestService;

            public userReqmodelController(IuserRequestService iuserRequestService)
            {
                _IuserRequestService = iuserRequestService;
            }

            [HttpGet("/hospitals")]
            public async Task<ActionResult<List<HospitalLocation>>> Gethospitals()
            {
                var result = await _IuserRequestService.Gethospitals();
                if (result != null)
                {
                    return (result);
                }
                else return BadRequest();
            }

            [HttpGet("/ping")]
            public string Getresponse()
            {
                return "running";
            }

            [HttpPost("/emergancy")]
            public async Task<ActionResult<userReqmodel>> Addrequest(userReqmodel request)
            {
                var success = await _IuserRequestService.Addrequest(request);
                if (success != null)
                {
                    return Ok(request.userReqID);
                }
                else return BadRequest();
            }
    }


