using System;
using Delamain_backend.Data;
using Delamain_backend.Services;
using Delamain_backend.Models;
using Microsoft.EntityFrameworkCore;
using Delamain_backend.Services.QueueWorkerInterface;
using FluentAssertions;

namespace Delamain_backend.Tests.BackGroundWorkerServices
{
	public class QueuemodelBackgroundworkerServiceTest
	{
        private async Task<DataContext> GetDataContextAsync()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.userReqmodels.CountAsync() <= 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    databaseContext.userReqmodels.Add(
                        new userReqmodel()
                        {
                            userReqID = "1q",
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
                        }); databaseContext.userReqmodels.Add(
                        new userReqmodel()
                        {
                            userReqID = "2a",
                            name = "Jenny",
                            Symptoms = "Cut off thumb",
                            age = 63,
                            gender = "female",
                            BMI = 32,
                            diabetes = false,
                            deficiencyanemias = true,
                            hypertensive = false,
                            hyperlipemia = true,
                            atrialfibrillation = false,
                            CHD_with_no_MI = false,
                            COPD = true,
                            depression = true,
                            heart_rate = 62,
                            respitory_rate = 12,
                            tempurature = 27,
                            phone = "89768",
                        }); databaseContext.userReqmodels.Add(
                        new userReqmodel()
                        {
                            userReqID = "3a",
                            name = "Bob",
                            Symptoms = "Cut off thumb",
                            age = 43,
                            gender = "male",
                            BMI = 22,
                            diabetes = false,
                            deficiencyanemias = true,
                            hypertensive = false,
                            hyperlipemia = false,
                            atrialfibrillation = false,
                            CHD_with_no_MI = false,
                            COPD = false,
                            depression = true,
                            heart_rate = 130,
                            respitory_rate = 12,
                            tempurature = 29,
                            phone = "89768",
                        }); databaseContext.userReqmodels.Add(
                        new userReqmodel()
                        {
                            userReqID = "876trcfghvbuy",
                            name = "Frankie",
                            Symptoms = "scolded hands",
                            age = 31,
                            gender = "female",
                            BMI = 25,
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
                        }); databaseContext.userReqmodels.Add(
                        new userReqmodel()
                        {
                            userReqID = "5a",
                            name = "Jess",
                            Symptoms = "bruises",
                            age = 20,
                            gender = "female",
                            BMI = 24,
                            diabetes = false,
                            deficiencyanemias = false,
                            hypertensive = false,
                            hyperlipemia = false,
                            atrialfibrillation = false,
                            CHD_with_no_MI = false,
                            COPD = false,
                            depression = false,
                            heart_rate = 62,
                            respitory_rate = 12,
                            tempurature = 27,
                            phone = "89768",
                        }); databaseContext.queuemodels.Add(
                        new Queuemodel()
                        {
                            queueID = "2345",
                            queueordernum = 0,
                            pushbackcount = 9,
                            Riskscore = 3
                        }); databaseContext.queuemodels.Add(
                        new Queuemodel()
                        {
                            queueID = "34rtug",
                            queueordernum = 1,
                            pushbackcount = 5,
                            Riskscore = 2
                        }); databaseContext.queuemodels.Add(
                        new Queuemodel()
                        {
                            queueID = "34regfbvde",
                            queueordernum = 2,
                            pushbackcount = 8,
                            Riskscore = 1
                        }); databaseContext.riskmodals.Add(
                            new Riskmodel()
                            {
                                age10to50 = 1,
                                age51to100 = 1,
                                male = 1,
                                female = 1,
                                BMI18to25 = 1,
                                BMIoutsideof18to25 = 1,
                                hyperlipmia = 1,
                                atrialfibrillation = 1,
                                CHD_with_no_MI = 1,
                                diabetes = 1,
                                deficiencyanemias = 1,
                                depression = 1,
                                COPD = 1,
                                hr60to100 = 1,
                                hroutside60to100 = 1,
                                rr12to16 = 1,
                                rroutside12to16 = 1,
                            });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
        [Fact]
        public async void QueueService()
        {
            //Arrange
            var dbContext = await GetDataContextAsync();
            var Service = new QueueService(dbContext);
            //Act
            var result = await Service.Queuemethod();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<string>();
            result.Should().BeEquivalentTo("TaskComplete");
        }
	}
}

