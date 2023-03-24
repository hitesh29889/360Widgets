using _360Widgets.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace _360Widgets
{
    public class ProcessLogFiles
    {
        public static string EvaluateLogFile(string strInput)
        {
            if (strInput.Length > 0)
            {
                string[] lines = strInput.Split("\n");
                Dictionary<string, string> output = new Dictionary<string, string>();
                InputModelText input = new InputModelText();
                CreateInputModel(ref input, lines);
                GenerateOutputText(input, ref output);
                return JsonConvert.SerializeObject(output).ToString();
            }
            return string.Empty;
        }
        private static void CreateInputModel(ref InputModelText input, string[] lines)
        {
            int thermo = 0;
            int humidity = 0;
            int monoxide = 0;
            foreach (string line in lines)
            {
                if (line.Split(" ")[0] == "reference")
                {
                    input.Reference = line.Replace("reference ", "");
                }
                else if (line.Split(" ")[0] == "thermometer")
                {
                    thermo += 1;
                    humidity = 0;
                    monoxide = 0;
                    input.Thermometer.Add(new List<string>());
                }
                else if (line.Split(" ")[0] == "humidity")
                {
                    thermo = 0;
                    humidity += 1;
                    monoxide = 0;
                    input.Humidity.Add(new List<string>());
                }
                else if (line.Split(" ")[0] == "monoxide")
                {
                    thermo = 0;
                    humidity = 0;
                    monoxide += 1;
                    input.Monoxide.Add(new List<string>());
                }

                if (thermo > 0)
                {
                    input.Thermometer[thermo - 1].Add(line.Split(" ")[1]);
                }
                if (humidity > 0)
                {
                    input.Humidity[humidity - 1].Add(line.Split(" ")[1]);
                }
                if (monoxide > 0)
                {
                    input.Monoxide[monoxide - 1].Add(line.Split(" ")[1]);
                }
            }
        }
        private static void GenerateOutputText(InputModelText input, ref Dictionary<string, string> ret)
        {

            CheckTemperature(input.Thermometer, input.Reference.Split(" ")[0], ref ret);
            CheckHumidity(input.Humidity, input.Reference.Split(" ")[1], ref ret);
            CheckMonoxide(input.Monoxide, input.Reference.Split(" ")[2], ref ret);
        }
        private static void CheckTemperature(List<List<string>> input, string ideal, ref Dictionary<string, string> ret)
        {
            if (input.Any())
            {
                for (int i = 0; i < input.Count; i++)
                {
                    string[] readings = input[i].ToArray();
                    if (readings.Length > 0)
                    {
                        List<decimal> temps = new List<decimal>();
                        for (int j = 1; j < readings.Count(); j++)
                        {
                            temps.Add(Convert.ToDecimal(readings[j]));
                        }
                        decimal average = temps.Average();
                        decimal veration = average - Convert.ToDecimal(ideal);
                        if (veration > -0.5M && veration < 0.5M)
                        {
                            ret.Add(readings[0], Status.UltraPrecise.ToString());
                        }
                        else if (veration > -3 && veration < 3)
                        {
                            ret.Add(readings[0], Status.VeryPrecise.ToString());
                        }
                        else
                        {
                            ret.Add(readings[0], Status.Precise.ToString());
                        }
                    }
                }
            }
        }
        private static void CheckHumidity(List<List<string>> input, string ideal, ref Dictionary<string, string> ret)
        {
            if (input.Any())
            {
                for (int i = 0; i < input.Count; i++)
                {
                    string[] readings = input[i].ToArray();
                    if (readings.Length > 0)
                    {
                        List<decimal> temps = new List<decimal>();
                        for (int j = 1; j < readings.Count(); j++)
                        {
                            temps.Add(Convert.ToDecimal(readings[j]));
                        }
                        decimal average = temps.Average();
                        decimal veration = average - Convert.ToDecimal(ideal);
                        //decimal idealPercentage = (100 * 1) / Convert.ToDecimal(ideal);

                        if (veration > -1 && veration < 1)
                        {
                            ret.Add(readings[0], Status.Keep.ToString());
                        }
                        else
                        {
                            ret.Add(readings[0], Status.Discard.ToString());
                        }
                    }
                }
            }
        }
        private static void CheckMonoxide(List<List<string>> input, string ideal, ref Dictionary<string, string> ret)
        {
            if (input.Any())
            {
                for (int i = 0; i < input.Count; i++)
                {
                    string[] readings = input[i].ToArray();
                    if (readings.Length > 0)
                    {
                        List<decimal> temps = new List<decimal>();
                        for (int j = 1; j < readings.Count(); j++)
                        {
                            temps.Add(Convert.ToDecimal(readings[j]));
                        }
                        decimal average = temps.Average();
                        decimal veration = average - Convert.ToDecimal(ideal);

                        if (veration > -3 && veration < 3)
                        {
                            ret.Add(readings[0], Status.Keep.ToString());
                        }
                        else
                        {
                            ret.Add(readings[0], Status.Discard.ToString());
                        }
                    }
                }
            }
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
