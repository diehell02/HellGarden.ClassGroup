using HellGarden.ClassGroup.Contracts.Interface;
using HellGarden.ClassGroup.GroupClassLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace HellGarden.ClassGroup.GroupClassLibrary.Config
{
    class WeightConfig
    {
        static WeightConfig()
        {
            //var _weights = new List<IWeight>()
            //{
            //    new Weight() { ID = id++, Index = 1, Type = WeightType.Score, Name = "Chinese", EnumValue = null, Limit = 10, Multiple = 10 },
            //    new Weight() { ID = id++, Index = 2, Type = WeightType.Score, Name = "Math", EnumValue = null, Limit = 10, Multiple = 10 },
            //    new Weight() { ID = id++, Index = 3, Type = WeightType.Score, Name = "English", EnumValue = null, Limit = 10, Multiple = 10 },
            //    new Weight() { ID = id++, Index = 4, Type = WeightType.Score, Name = "Physics", EnumValue = null, Limit = 10, Multiple = 10 },
            //    new Weight() { ID = id++, Index = 5, Type = WeightType.Score, Name = "Chemistry", EnumValue = null, Limit = 10, Multiple = 10 },
            //    new Weight() { ID = id++, Index = 6, Type = WeightType.Score, Name = "Biology", EnumValue = null, Limit = 10, Multiple = 10 },
            //    new Weight() { ID = id++, Index = 8, Type = WeightType.Enum, Name = "IsDowntown", EnumValue = "市直", Limit = 10, Multiple = 100 },
            //    new Weight() { ID = id++, Index = 9, Type = WeightType.Enum, Name = "IsMale", EnumValue = "男", Limit = 10, Multiple = 1000 },
            //    new Weight() { ID = id++, Index = 10, Type = WeightType.Enum, Name = "IsLodge", EnumValue = "寄宿", Limit = 10, Multiple = 100 },
            //};

            using (JsonReader jsonReader = new JsonTextReader(new StreamReader("WeightConfig.json")))
            {
                JsonSerializer serializer = new JsonSerializer();

                var _weights = serializer.Deserialize<List<Weight>>(jsonReader);

                if (_weights != null)
                {
                    int id = 0;
                    _weights.ForEach(weight =>
                    {
                        weight.ID = id++;
                    });

                    Weights = _weights.ToArray();
                }
            }
        }

        public static IWeight[] Weights { get; private set; }
    }
}
