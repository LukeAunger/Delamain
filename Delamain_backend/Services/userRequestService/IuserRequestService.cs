using System;
namespace Delamain_backend.Services.userRequestService
{
	public interface IuserRequestService
	{
       Task<ActionResult<List<HospitalLocation>>> Gethospitals();
        Task<ActionResult<userReqmodel>> Addrequest(userReqmodel request);
    }
}

