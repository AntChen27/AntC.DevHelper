using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AntC.CodeGenerate.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AntC.CodeGenerate.Converts
{
    public class TableGroupInfoConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TableGroupInfo);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            TableGroupInfo tableGroupInfo = null;

            do
            {
                if (reader.TokenType == JsonToken.StartObject)
                {
                    tableGroupInfo = new TableGroupInfo();
                }
                else if (reader.TokenType == JsonToken.PropertyName)
                {
                    tableGroupInfo.TableName = reader.Value?.ToString();
                }
                else if (reader.TokenType == JsonToken.String)
                {
                    tableGroupInfo.GroupName = reader.Value?.ToString();
                }
                else if (reader.TokenType == JsonToken.EndObject)
                {
                    return tableGroupInfo;
                }
            } while (reader.Read());
            return tableGroupInfo;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var tableGroupInfo = (TableGroupInfo)value;

                writer.WriteStartObject();
                writer.WritePropertyName(tableGroupInfo.TableName);
                writer.WriteValue(tableGroupInfo.GroupName);
                writer.WriteEndObject();
            }
        }
    }
}
