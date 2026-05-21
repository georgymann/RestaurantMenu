using Model.Core.Abstract;
using Model.Data.Serialization.DTO;
using System.Xml.Serialization;

namespace Model.Data.Serialization;

public class XmlDataSerializer : SerializerBase
{
    public void XmlSerializer(Establishment establishment, string filePath)
    {
        EstablishmentDTO dto = SerializeEstablishment(establishment);
        XmlSerializer serializer = new XmlSerializer(dto.GetType());
        using FileStream stream = new FileStream(filePath, FileMode.Create);
        serializer.Serialize(stream, dto);
    }
    
    public Establishment? LoadEstablishment<T>(string filePath) where T : EstablishmentDTO
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using FileStream stream = new FileStream(filePath, FileMode.Open);
        T? dto = serializer.Deserialize(stream) as T;
        if (dto == null) { return null; }
        return DeserializeEstablishment(dto);
    }
}