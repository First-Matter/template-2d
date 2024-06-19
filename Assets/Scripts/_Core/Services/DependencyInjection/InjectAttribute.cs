using System;

/// <summary>
/// Marks fields within an <see cref="InjectableMonoBehaviour"/> for automatic dependency injection.
/// When the <see cref="InjectableMonoBehaviour"/> is initialized, the dependency injection system 
/// will automatically provide the appropriate service instance to fields marked with this attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class InjectAttribute : Attribute
{
}