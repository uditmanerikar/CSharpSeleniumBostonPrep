using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.BiDi.Script;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace RealTimeCsharpSelenium.Utilities
{
    public class jsonReader
    {
        //string path = Path.Combine(
        //AppDomain.CurrentDomain.BaseDirectory,
        //"Testdata.json"
        //);

        public String extarctdata(String Tokenname)
        {
            String data = File.ReadAllText("Testdata.json");
            var Jsonobject = JToken.Parse(data);
            return Jsonobject.SelectToken(Tokenname).Value<String>();
        }

        public String[] extarctdataArray(String Tokenname)
        {
            String data = File.ReadAllText("Testdata.json");
            var Jsonobject = JToken.Parse(data);
            List<String> li = Jsonobject.SelectTokens(Tokenname).Values<String>().ToList();
            return li.ToArray();

        }
    }
}
