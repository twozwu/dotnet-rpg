using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
    // 在swagger的schema裡，將enum從val(1、2、3)轉換成key值(Knight、Mage...)
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3
    }
}
