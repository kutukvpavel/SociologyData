using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MoreLinq;

namespace SociologyData.Data
{
    public class GibddJsonScheme
    {
        public GibddJsonScheme() { }

        [JsonProperty(PropertyName = "cards")]
        public DtpCard[] Cards { get; set; }

        public int GetDriverPdd()
        {
            return Cards.Sum(x => x.Info.Ts.Count(x => x.People.Any(
                x => (x.Category == "Водитель") && (x.Violation.FirstOrDefault() != "Нет нарушений"))));
        }

        public int GetPedestrianPdd()
        {
            return Cards.Sum(x => x.Info.Other.Count(x => 
                (x.Category == "Пешеход") && (x.Violation.FirstOrDefault() != "Нет нарушений")));
        }
    }

    public class DtpCard
    {
        public DtpCard() { }

        [JsonProperty(PropertyName = "infoDtp")]
        public InfoDtp Info { get; set; }
    }

    public class InfoDtp
    {
        public InfoDtp() { }

        [JsonProperty(PropertyName = "ts_info")]
        public TsInfo[] Ts { get; set; }
        [JsonProperty(PropertyName = "uchInfo")]
        public UchInfo[] Other { get; set; }
    }

    public class TsInfo
    {
        public TsInfo() { }

        [JsonProperty(PropertyName = "ts_uch")]
        public TsUch[] People { get; set; }
    }

    public class TsUch
    {
        public TsUch() { }

        [JsonProperty(PropertyName = "K_UCH")]
        public string Category { get; set; }
        [JsonProperty(PropertyName = "NPDD")]
        public string[] Violation { get; set; }
    }

    public class UchInfo
    {
        public UchInfo() { }

        [JsonProperty(PropertyName = "K_UCH")]
        public string Category { get; set; }
        [JsonProperty(PropertyName = "NPDD")]
        public string[] Violation { get; set; }
    }
}
