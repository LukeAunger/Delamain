using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Delamain_backend.Services.userRequestService
{
	public class userRequestService : IuserRequestService
	{
        private readonly DataContext _context;
        public userRequestService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<userReqmodel>> Addrequest(userReqmodel request)
        {
            request.userReqID = CreateRandomToken();
            _context.userReqmodels.Add(request);
            await _context.SaveChangesAsync();
            return (request);
        }

        public async Task<ActionResult<List<HospitalLocation>>> Gethospitals()
        {
            return (await _context.HospitalLocations.ToListAsync());
        }

        private string CreateRandomToken()
        {
            var newkey = "";
            var pass = false;
            while (pass == false)
            {
                newkey = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                var check = _context.userReqmodels.FirstOrDefault(c => c.userReqID == newkey);
                if (check == null)
                {
                    pass = true;
                }
            }
            return (newkey);
        }
    }
}

