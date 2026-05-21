using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Model.Core.Abstract;
using Model.Data.Serialization.DTO;

namespace Model.Data.Serialization;

public class JsonDataSerializer : SerializerBase
{
    public void JSONSerializer(Establishment establishment, string filePath)
    {
        EstablishmentDTO dto = SerializeEstablishment(establishment);
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented };
        string json = JsonConvert.SerializeObject(dto, settings);
        File.WriteAllText(filePath, json);
    }

    public Establishment? JSONDeserializer<T>(string filePath) where T : EstablishmentDTO
    {
        string json = File.ReadAllText(filePath);
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        T? dto = JsonConvert.DeserializeObject<T>(json, settings);
        if (dto == null) { return null; }
        return DeserializeEstablishment(dto);
    }
}