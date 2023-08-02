using System;
using Delamain_backend.Data;
using Delamain_backend.Models;
using Delamain_backend.Services.userRequestService;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delamain_backend.Tests.Repository
{
	public class userRequestServiceTests
	{
		private async Task<DataContext> GetDataContextAsync()
		{
			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;
			var databaseContext = new DataContext(options);
			databaseContext.Database.EnsureCreated();
			if(await databaseContext.userReqmodels.CountAsync() <= 0)
			{
				for(int i = 0; i < 1; i++)
				{
					databaseContext.HospitalLocations.Add(
						new HospitalLocation
						{
							name = "StJhons",
							lat = 9876,
							lng = 23456
						});
					databaseContext.userReqmodels.Add(
						new userReqmodel()
						{
							name = "JohnSmith",
							Symptoms = "Cut off thumb",
							age = 25,
							gender = "male",
							BMI = 25,
							diabetes = false,
							deficiencyanemias = true,
							hypertensive = false,
							hyperlipemia = false,
							atrialfibrillation = false,
							CHD_with_no_MI = false,
							COPD = false,
							depression = true,
							heart_rate = 62,
							respitory_rate = 12,
							tempurature = 27,
							phone = "89768",
						});
					await databaseContext.SaveChangesAsync();
				}
			}
			return databaseContext;
		}


		[Fact]
		public async void userRequestService_AddRequest_Success()
		{
			//Arrange
			var name = new userReqmodel()
            {
                name = "Frankie",
                Symptoms = "Scolded hands",
                age = 28,
                gender = "female",
                BMI = 24,
                diabetes = false,
                deficiencyanemias = false,
                hypertensive = false,
                hyperlipemia = false,
                atrialfibrillation = false,
                CHD_with_no_MI = false,
                COPD = false,
                depression = true,
                heart_rate = 62,
                respitory_rate = 12,
                tempurature = 27,
                phone = "89768",
            };
            var dbContext = await GetDataContextAsync();
			var userReqService = new userRequestService(dbContext);

			//Act
			var result = userReqService.Addrequest(name);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Task<ActionResult<userReqmodel>>>();
            result.Result.Value.Should().BeEquivalentTo(name);
        }
		[Fact]
		public async void userRequestService_GetHospitals_Success()
		{
			var dbContext = await GetDataContextAsync();
			var userReqService = new userRequestService(dbContext);
			//Act
			var result = userReqService.Gethospitals();
			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Task<ActionResult<List<HospitalLocation>>>>();
			result.Should().NotBe(0);
		}
	}
}

