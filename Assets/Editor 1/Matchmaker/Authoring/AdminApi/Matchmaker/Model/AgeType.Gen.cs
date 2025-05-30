#if UNITY_EDITOR || ENABLE_RUNTIME_ADMIN_APIS
//-----------------------------------------------------------------------------
// <auto-generated>
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Unity.Services.Multiplayer.Editor.Matchmaker.Authoring.AdminApi.Matchmaker.Model
{
    /// <summary>
    /// Defines AgeType
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    enum AgeType
    {
        /// <summary>
        /// Enum Youngest for value: Youngest
        /// </summary>
        [EnumMember(Value = "Youngest")]
        Youngest = 1,

        /// <summary>
        /// Enum Oldest for value: Oldest
        /// </summary>
        [EnumMember(Value = "Oldest")]
        Oldest = 2,

        /// <summary>
        /// Enum Average for value: Average
        /// </summary>
        [EnumMember(Value = "Average")]
        Average = 3

    }

}
#endif
