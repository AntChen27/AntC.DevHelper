using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AntC.CodeGenerate.Converts
{
    public class TableGroupInfoConvert : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TableGroupInfo);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //base.WriteJson(writer, value, serializer);
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var tableGroupInfo = (TableGroupInfo)value;
                var formatter = $"{{\"{tableGroupInfo.TableName}\":\"{tableGroupInfo.GroupName}\"}}";
                writer.WriteValue(formatter);
            }
        }
    }
}
