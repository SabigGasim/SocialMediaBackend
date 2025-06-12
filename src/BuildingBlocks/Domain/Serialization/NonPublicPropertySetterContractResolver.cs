using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace SocialMediaBackend.BuildingBlocks.Domain.Serialization;

public class NonPublicPropertySetterContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        if (member is PropertyInfo propInfo && propInfo.GetSetMethod(true) is not null)
        {
            property.Writable = true;
        }

        return property;
    }
}
