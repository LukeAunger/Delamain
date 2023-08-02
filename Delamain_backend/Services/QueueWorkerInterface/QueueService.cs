using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Delamain_backend.Services.QueueWorkerInterface
{
	public class QueueService : IQueueService
	{
        private readonly DataContext _context;

        public QueueService(DataContext context)
		{
            _context = context;
        }

        public async Task<string> Queuemethod()
        {
            var users = await _context.userReqmodels.ToListAsync();
            var risk = _context.riskmodals.OrderBy(r => r.ID).LastOrDefault();
            var que = await _context.queuemodels.ToListAsync();


            if (users.Count != 0)
            {
                foreach (var request in users.ToList())
                {
                    if (request.age < 50)
                    {
                        request.Riskscore += risk.age10to50;
                    }
                    else
                    {
                        request.Riskscore += risk.age51to100;
                    }

                    if (request.gender == "male")
                    {
                        request.Riskscore += risk.male;
                    }
                    else
                    {
                        request.Riskscore += risk.female;
                    }

                    if (request.BMI >= 18 && request.BMI <= 25)
                    {
                        request.Riskscore += risk.BMI18to25;
                    }
                    else
                    {
                        request.Riskscore += risk.BMIoutsideof18to25;
                    }

                    if (request.hyperlipemia == true)
                    {
                        request.Riskscore += risk.hyperlipmia;
                    }

                    if (request.hypertensive == true)
                    {
                        request.Riskscore += risk.hypertensive;
                    }

                    if (request.deficiencyanemias == true)
                    {
                        request.Riskscore += risk.deficiencyanemias;
                    }

                    if (request.atrialfibrillation == true)
                    {
                        request.Riskscore += risk.atrialfibrillation;
                    }

                    if (request.CHD_with_no_MI == true)
                    {
                        request.Riskscore += risk.CHD_with_no_MI;
                    }

                    if (request.COPD == true)
                    {
                        request.Riskscore += risk.COPD;
                    }

                    if (request.diabetes == true)
                    {
                        request.Riskscore += risk.diabetes;
                    }

                    if (request.depression == true)
                    {
                        request.Riskscore += risk.depression;
                    }

                    if (request.heart_rate >= 60 && request.heart_rate <= 100)
                    {
                        request.Riskscore += risk.hr60to100;
                    }
                    else
                    {
                        request.Riskscore += risk.hroutside60to100;
                    }

                    if (request.respitory_rate >= 12 && request.respitory_rate <= 16)
                    {
                        request.Riskscore += risk.rr12to16;
                    }
                    else
                    {
                        request.Riskscore += risk.rroutside12to16;
                    }

                    if (request.tempurature >= 36.1 && request.tempurature <= 37.2)
                    {
                        request.Riskscore += risk.goodbodytemp;
                    }
                    else
                    {
                        request.Riskscore += risk.outsidegoodbodytemp;
                    }
                }
                foreach (var request in users.ToList())
                {
                    var key = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
                    //If there is no items in the queue
                    if (que.Count == 0)
                    {
                        var queuemember = new Queuemodel
                        {
                            queueID = key,
                            queueordernum = 0,
                            pushbackcount = 0,
                            Riskscore = request.Riskscore
                        };
                        var usermember = new Userdetail
                        {
                            userReqID = request.userReqID,
                            name = request.name,
                            Symptoms = request.Symptoms,
                            age = request.age,
                            gender = request.gender,
                            BMI = request.BMI,
                            diabetes = request.diabetes,
                            atrialfibrillation = request.atrialfibrillation,
                            deficiencyanemias = request.deficiencyanemias,
                            hypertensive = request.hypertensive,
                            hyperlipemia = request.hyperlipemia,
                            CHD_with_no_MI = request.CHD_with_no_MI,
                            COPD = request.COPD,
                            depression = request.depression,
                            heart_rate = request.heart_rate,
                            respitory_rate = request.respitory_rate,
                            tempurature = request.tempurature,
                            geoloc = request.geoloc,
                            queueId = queuemember.queueID,
                        };
                        _context.queuemodels.Add(queuemember);
                        _context.userdetails.Add(usermember);
                        _context.userReqmodels.Remove(request);
                        await _context.SaveChangesAsync();
                    }

                    //Once there is people in the queue this will run
                    else
                    {
                        request.queueordernum = que.Count + 1;

                        for (var item = que.Count + 1; item >= 0; item--)
                        {
                            foreach (var entry in que)
                            {
                                if (entry.queueordernum == item)
                                {
                                    if (entry.Riskscore < request.Riskscore)
                                    {
                                        if (entry.pushbackcount != 10)
                                        {
                                            entry.pushbackcount++;
                                            if (request.queueordernum > entry.queueordernum)
                                            {
                                                request.queueordernum = entry.queueordernum;
                                                entry.queueordernum++;
                                            }
                                            else
                                            {
                                                entry.queueordernum++;
                                            }
                                        }
                                        else
                                        {
                                            request.queueordernum = entry.queueordernum;
                                            request.queueordernum++;
                                            goto queuefin;
                                        }
                                    }
                                    else
                                    {
                                        request.queueordernum = entry.queueordernum;
                                        request.queueordernum++;
                                        goto queuefin;
                                    }
                                }
                            }
                        }
                    queuefin:
                        var queuemember = new Queuemodel
                        {
                            queueID = key,
                            queueordernum = request.queueordernum,
                            pushbackcount = 0,
                            Riskscore = request.Riskscore
                        };

                        var usermember = new Userdetail
                        {
                            userReqID = request.userReqID,
                            name = request.name,
                            Symptoms = request.Symptoms,
                            age = request.age,
                            gender = request.gender,
                            BMI = request.BMI,
                            diabetes = request.diabetes,
                            atrialfibrillation = request.atrialfibrillation,
                            deficiencyanemias = request.deficiencyanemias,
                            hypertensive = request.hypertensive,
                            hyperlipemia = request.hyperlipemia,
                            CHD_with_no_MI = request.CHD_with_no_MI,
                            COPD = request.COPD,
                            depression = request.depression,
                            heart_rate = request.heart_rate,
                            respitory_rate = request.respitory_rate,
                            tempurature = request.tempurature,
                            geoloc = request.geoloc,
                            queueId = queuemember.queueID,
                        };
                        _context.queuemodels.Add(queuemember);
                        _context.userdetails.Add(usermember);
                        _context.userReqmodels.Remove(request);
                        await _context.SaveChangesAsync();
                    }
                }
                return ("TaskComplete");
            }
            else return ("NoResult");
        }
    }
}

