using Mono.Cecil;

namespace Store.Tests;

internal static class PropertiesSettersRule
{
    public static ConditionList HaveAllPropertiesWithInitOnly(this Conditions conditions)
        => conditions.MeetCustomRule(type => type.Fields.All(field => field.IsInitOnly));

    public static ConditionList HaveAllPropertiesWithoutPublicSetters(this Conditions conditions)
    {
        return conditions.MeetCustomRule(type => type.Properties.All(SetterIsNotExposedExternally));

        bool SetterIsNotExposedExternally(PropertyDefinition property)
        {
            return property.SetMethod == null || property.SetMethod.IsPublic == false;
        }
    }
}