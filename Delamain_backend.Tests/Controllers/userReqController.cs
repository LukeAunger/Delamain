using System;
using Delamain_backend.Controllers;
using Delamain_backend.Data;
using Delamain_backend.Models;
using Delamain_backend.Services.userRequestService;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Delamain_backend.Tests.Controllers
{
	public class userReqController
	{
		private readonly IuserRequestService _iuserReqService;
		private readonly userReqmodelController _userReqController;

		public userReqController()
		{
			//Dependancies
			_iuserReqService = A.Fake<IuserRequestService>();

			//SUT
			_userReqController = new userReqmodelController(_iuserReqService);
		}
        [Fact]
        public async void userReqmodelController_GetHospital_ReturnOk()
        {
			//Arrange
			var hospital = A.Fake<ICollection<HospitalLocation>>();
			var hospitalslist = A.Fake<List<HospitalLocation>>();
			A.CallTo(() => _iuserReqService.Gethospitals()).Returns(hospitalslist);
			//Act
			var result = await _userReqController.Gethospitals();
			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<ActionResult<List<HospitalLocation>>>();
        }
		[Fact]
		public void userReqmodelController_AddRequest_ReturnOk()
		{
			//Arrange
			var userObj = A.Fake<userReqmodel>();
			A.CallTo(() => _iuserReqService.Addrequest(userObj)).Returns(userObj);
			var controller = _userReqController;
            //Act
			var result = controller.Addrequest(userObj);
			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Task<ActionResult<userReqmodel>>>();
        }
    }
}

