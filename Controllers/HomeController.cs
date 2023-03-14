using _360Widgets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace _360Widgets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        #region "Using JSON"

        public IActionResult Calculate()
        {
            using (StreamReader r = new StreamReader("input.json"))
            {
                string json = r.ReadToEnd();
                InputModel input = JsonConvert.DeserializeObject<InputModel>(json);
                OutputModel output = GenerateOutput(input);
                return View("Index", JsonConvert.SerializeObject(output));
            }

            return View("Index", new OutputModel());
        }

        public OutputModel GenerateOutput(InputModel input)
        {
            OutputModel response = new OutputModel();
            string[] ideal = input.Reference.Split(" ");
            CheckTemperature(input.Thermometer, ideal[0], ref response);
            CheckHumidity(input.Humidity, ideal[1], ref response);
            CheckMonoxide(input.Monoxide, ideal[2], ref response);
            return response;
        }

        public List<string> CheckTemperature(List<Dictionary<string, string>> input, string ideal, ref OutputModel response)
        {
            List<string> strArray = new List<string>();
            if (input.Any())
            {
                for (int i = 0; i < input.Count; i++)
                {
                    string[] readings = input[i].Values.ToArray();
                    if (readings.Length > 0)
                    {
                        List<decimal> temps = new List<decimal>();
                        for (int j = 0; j < readings.Count(); j++)
                        {
                            temps.Add(Convert.ToDecimal(readings[j].Split(" ")[1]));
                        }
                        decimal average = temps.Average();
                        decimal veration = average - Convert.ToDecimal(ideal);
                        if (veration > -0.5M && veration < 0.5M)
                        {
                            response.Output.Add("temp-" + (i + 1), Status.UltraPrecise.ToString());
                        }
                        else if (veration > -3 && veration < 3)
                        {
                            response.Output.Add("temp-" + (i + 1), Status.VeryPrecise.ToString());
                        }
                        else
                        {
                            response.Output.Add("temp-" + (i + 1), Status.Precise.ToString());
                        }
                    }
                }
            }
            return strArray;
        }
        public List<string> CheckHumidity(List<Dictionary<string, string>> input, string ideal, ref OutputModel response)
        {
            List<string> strArray = new List<string>();
            if (input.Any())
            {
                for (int i = 0; i < input.Count; i++)
                {
                    string[] readings = input[i].Values.ToArray();
                    if (readings.Length > 0)
                    {
                        List<decimal> temps = new List<decimal>();
                        for (int j = 0; j < readings.Count(); j++)
                        {
                            temps.Add(Convert.ToDecimal(readings[j].Split(" ")[1]));
                        }
                        decimal average = temps.Average();
                        decimal veration = average - Convert.ToDecimal(ideal);
                        //decimal idealPercentage = (100 * 1) / Convert.ToDecimal(ideal);

                        if (veration > -1 && veration < 1)
                        {
                            response.Output.Add("hum-" + (i + 1), Status.Keep.ToString());
                        }
                        else
                        {
                            response.Output.Add("hum-" + (i + 1), Status.Discard.ToString());
                        }
                    }
                }
            }
            return strArray;
        }
        public List<string> CheckMonoxide(List<Dictionary<string, string>> input, string ideal, ref OutputModel response)
        {
            List<string> strArray = new List<string>();
            if (input.Any())
            {
                for (int i = 0; i < input.Count; i++)
                {
                    string[] readings = input[i].Values.ToArray();
                    if (readings.Length > 0)
                    {
                        List<decimal> temps = new List<decimal>();
                        for (int j = 0; j < readings.Count(); j++)
                        {
                            temps.Add(Convert.ToDecimal(readings[j].Split(" ")[1]));
                        }
                        decimal average = temps.Average();
                        decimal veration = average - Convert.ToDecimal(ideal);

                        if (veration > -3 && veration < 3)
                        {
                            response.Output.Add("mon-" + (i + 1), Status.Keep.ToString());
                        }
                        else
                        {
                            response.Output.Add("mon-" + (i + 1), Status.Discard.ToString());
                        }
                    }
                }
            }
            return strArray;
        }
        #endregion

        #region "Using Text"
        public IActionResult CalculateText()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            InputModel input = new InputModel();
            foreach (string line in lines)
            {
                if (line.Split(" ")[0] == "reference")
                {
                    input.Reference = line.Split(" ")[0];
                }
            }
            return View("Index", new OutputModel());
        }

        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public enum Status
        {
            [EnumMember(Value = "ultra precise")]
            UltraPrecise,
            [EnumMember(Value = "very precise")]
            VeryPrecise,
            [EnumMember(Value = "precise")]
            Precise,
            [EnumMember(Value = "discard")]
            Discard,
            [EnumMember(Value = "keep")]
            Keep
        }
    }
}
