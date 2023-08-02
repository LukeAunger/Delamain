using System.Threading;
using Delamain_backend.Data;
using Delamain_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delamain_backend.Services;
//Background services to allow the server to run async while this works neets to inherit backgroundService
// background service also needs to run in the ExecuteAsync because if I run in the startAsync that is a Ihosted
// method which will not allow the server to run whilst that is active.
public class IcuDataWorkerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    public IcuDataWorkerService(IServiceProvider context)
    {
        _serviceProvider = context;
    }
    //When creating ran into issue where server would not startup after building this whole method put it down to method not being async whilst i was using struct objects
    //Modified the code to use awaits making it an asyncronous method which means it will allow the server to load whilst this runs in the background.
    protected override async Task ExecuteAsync(CancellationToken stoptoken)
    {
        var count = 0;
        while (!stoptoken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var icuDatamodels = await _context.IcuDatas.ToListAsync();

            if (count < icuDatamodels.Count)
            {
                var risk = await _context.riskmodals.ToListAsync();
                double Age10to50count = 1;
                double Ageoutside10to50count = 1;
                double malecount = 1;
                double femalecount = 1;
                double BMI18to25count = 1;
                double BMIoutside18to25count = 1;
                double Hypertensivecount = 1;
                double artrialfibrilationcount = 1;
                double ChdWithNoMicount = 1;
                double Diabetescount = 1;
                double deficiencyanemiascount = 1;
                double Depressioncount = 1;
                double Hyperlipemiacount = 1;
                double COPDcount = 1;
                double Heartrate60to100count = 1;
                double Heartrateoutside60to100count = 1;
                double respitory12to16count = 1;
                double respitoryoutside12to16count = 1;
                double goodbodytempcount = 1;
                double outsidegoodbodytempcount = 1;

                double Age10to50deathcount = 1;
                double Ageoutside10to50deathcount = 1;
                double maledeathcount = 1;
                double femaledeathcount = 1;
                double BMI18to25deathcount = 1;
                double BMIoutside18to25deathcount = 1;
                double Hypertensivedeathcount = 1;
                double artrialfibrilationdeathcount = 1;
                double ChdWithNoMideathcount = 1;
                double Diabetesdeathcount = 1;
                double deficiencyanemiasdeathcount = 1;
                double Depressiondeathcount = 1;
                double Hyperlipemiadeathcount = 1;
                double COPDdeathcount = 1;
                double Heartrate60to100deathcount = 1;
                double Heartrateoutside60to100deathcount = 1;
                double respitory12to16deathcount = 1;
                double respitoryoutside12to16deathcount = 1;
                double goodbodytempdeathcount = 1;
                double outsidegoodbodytempdeathcount = 1;

                foreach (var item in icuDatamodels)
                {
                    if (item.Age < 50)
                    {
                        Age10to50count++;
                    }
                    else
                    {
                        Ageoutside10to50count++;
                    }

                    if (item.Gender == "male")
                    {
                        malecount++;
                    }
                    else if (item.Gender == "female")
                    {
                        femalecount++;
                    }

                    if (item.Bmi >= 18 && item.Bmi <= 25)
                    {
                        BMI18to25count++;
                    }
                    else if (item.Bmi < 18 && item.Bmi > 25)
                    {
                        BMIoutside18to25count++;
                    }

                    if (item.Hyperlipemia == true)
                    {
                        Hyperlipemiacount++;
                    }

                    if (item.Hypertensive == true)
                    {
                        Hypertensivecount++;
                    }

                    if (item.Deficiencyanemias == true)
                    {
                        deficiencyanemiascount++;
                    }

                    if (item.Atrialfibrillation == true)
                    {
                        artrialfibrilationcount++;
                    }

                    if (item.ChdWithNoMi == true)
                    {
                        ChdWithNoMicount++;
                    }

                    if (item.Copd == true)
                    {
                        COPDcount++;
                    }

                    if (item.Diabetes == true)
                    {
                        Diabetescount++;
                    }

                    if (item.Depression == true)
                    {
                        Depressioncount++;
                    }

                    if (item.HeartRate >= 60 && item.HeartRate <= 100)
                    {
                        Heartrate60to100count++;
                    }
                    else if (item.HeartRate > 60 && item.HeartRate < 100)
                    {
                        Heartrateoutside60to100count++;
                    }

                    if (item.RespitoryRate >= 12 && item.RespitoryRate <= 16)
                    {
                        respitory12to16count++;
                    }
                    else if (item.RespitoryRate < 12 && item.RespitoryRate > 16)
                    {
                        respitoryoutside12to16count++;
                    }

                    if (item.Temperature >= 36.1 && item.Temperature <= 37.2)
                    {
                        goodbodytempcount++;
                    }
                    else if (item.Temperature < 36.1 && item.Temperature > 37.2)
                    {
                        outsidegoodbodytempcount++;
                    }
                    }
                    foreach (var item in icuDatamodels)
                    {
                    if (item.Age < 50 && item.Outcome == true)
                    {
                        Age10to50deathcount++;
                    }
                    else if (item.Age >= 50 && item.Outcome == true)
                    {
                        Ageoutside10to50deathcount++;
                    }

                    if (item.Gender == "male" && item.Outcome == true)
                    {
                        maledeathcount++;
                    }
                    else if (item.Gender == "female" && item.Outcome == true)
                    {
                        femaledeathcount++;
                    }

                    if (item.Bmi >= 18 && item.Bmi <= 25 && item.Outcome == true)
                    {
                        BMI18to25deathcount++;
                    }
                    else if (item.Bmi < 18 && item.Bmi > 25 && item.Outcome == true)
                    {
                        BMIoutside18to25deathcount++;
                    }

                    if (item.Hyperlipemia == true && item.Outcome == true)
                    {
                        Hyperlipemiadeathcount++;
                    }

                    if (item.Hypertensive == true && item.Outcome == true)
                    {
                        Hypertensivedeathcount++;
                    }

                    if (item.Deficiencyanemias == true && item.Outcome == true)
                    {
                        deficiencyanemiasdeathcount++;
                    }

                    if (item.Atrialfibrillation == true && item.Outcome == true)
                    {
                        artrialfibrilationdeathcount++;
                    }

                    if (item.ChdWithNoMi == true && item.Outcome == true)
                    {
                        ChdWithNoMideathcount++;
                    }

                    if (item.Copd == true && item.Outcome == true)
                    {
                        COPDdeathcount++;
                    }

                    if (item.Diabetes == true && item.Outcome == true)
                    {
                        Diabetesdeathcount++;
                    }

                    if (item.Depression == true && item.Outcome == true)
                    {
                        Depressiondeathcount++;
                    }

                    if (item.HeartRate >= 60 && item.HeartRate <= 100 && item.Outcome == true)
                    {
                        Heartrate60to100deathcount++;
                    }
                    else if (item.HeartRate > 60 && item.HeartRate < 100 && item.Outcome == true)
                    {
                        Heartrateoutside60to100deathcount++;
                    }

                    if (item.RespitoryRate >= 12 && item.RespitoryRate <= 16 && item.Outcome == true)
                    {
                        respitory12to16deathcount++;
                    }
                    else if (item.RespitoryRate < 12 && item.RespitoryRate > 16 && item.Outcome == true)
                    {
                        respitoryoutside12to16deathcount++;
                    }

                    if (item.Temperature >= 36.1 && item.Temperature <= 37.2 && item.Outcome == true)
                    {
                        goodbodytempdeathcount++;
                    }
                    else if (item.Temperature < 36.1 && item.Temperature > 37.2 && item.Outcome == true)
                    {
                        outsidegoodbodytempdeathcount++;
                    }
                }
                var file = new Riskmodel
                {
                    age10to50 = Age10to50deathcount / Age10to50count * 100,
                    age51to100 = Ageoutside10to50deathcount / Ageoutside10to50count * 100,
                    male = maledeathcount / malecount * 100,
                    female = femaledeathcount / femalecount * 100,
                    BMI18to25 = BMI18to25deathcount / BMI18to25count * 100,
                    BMIoutsideof18to25 = BMIoutside18to25deathcount / BMIoutside18to25count * 100,
                    hyperlipmia = Hyperlipemiadeathcount / Hyperlipemiadeathcount * 100,
                    atrialfibrillation = artrialfibrilationdeathcount / artrialfibrilationcount * 100,
                    CHD_with_no_MI = ChdWithNoMideathcount / ChdWithNoMicount * 100,
                    diabetes = Diabetesdeathcount / Diabetescount * 100,
                    deficiencyanemias = deficiencyanemiasdeathcount / deficiencyanemiascount * 100,
                    depression = Depressiondeathcount / Depressioncount * 100,
                    COPD = COPDdeathcount / COPDcount * 100,
                    hr60to100 = Heartrate60to100deathcount / Heartrate60to100count * 100,
                    hroutside60to100 = Heartrateoutside60to100deathcount / Heartrateoutside60to100count * 100,
                    rr12to16 = respitory12to16deathcount / respitory12to16count * 100,
                    rroutside12to16 = respitoryoutside12to16deathcount / respitoryoutside12to16count * 100
                };
                count = icuDatamodels.Count;

                var checker = _context.riskmodals.OrderBy(r => r.ID).LastOrDefault();
                if(checker == null )
                {
                    _context.riskmodals.Add(file);
                    await _context.SaveChangesAsync(stoptoken);
                } else if (checker != null)
                {
                    if (checker.age10to50 != file.age10to50 && checker.goodbodytemp != file.goodbodytemp && checker.hyperlipmia != file.hyperlipmia)
                    {
                        _context.riskmodals.Add(file);
                        await _context.SaveChangesAsync(stoptoken);
                    }
                }
            }
            else
            {
                await Task.Delay(1000, stoptoken);
            }
        }
    }
}