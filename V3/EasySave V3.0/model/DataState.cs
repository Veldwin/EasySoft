using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class IdToStringConverter : JsonConverter
{
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JToken jt = JValue.ReadFrom(reader);

        return jt.Value<long>();
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(System.Int64).Equals(objectType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value.ToString());
    }
}

namespace EasySaveApp.model
{
    class DataState
    {
        // Declaration of properties that are used for saving information for the report file in JSON
        public string SaveNameState { get; set; }
        public string BackupDateState { get; set; }
        public bool SaveState { get; set; }
        public string SourceFileState { get; set; }
        public string TargetFileState { get; set; }
        public float TotalFileState { get; set; }
        [JsonConverter(typeof(IdToStringConverter))]
        public long TotalSizeState { get; set; }
        public float ProgressState { get; set; }
        public long FileRestState { get; set; }
        public long TotalSizeRestState { get; set; }

        public DataState(string saveNameState)
        {
            SaveNameState = saveNameState;
        }
    }
}