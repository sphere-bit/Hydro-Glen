// Unique identifier formats:
/// <see href="link">https://learn.microsoft.com/en-us/dotnet/api/system.guid.tostring?view=net-7.0</see>
// Must inherit Property Attribute for the id component to be read only.

using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{
}