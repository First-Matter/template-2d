using System;

[AttributeUsage(AttributeTargets.Field)]
public class DataAttribute : Attribute
{
  public string DataName;

  public DataAttribute(string dataName)
  {
    DataName = dataName;
  }
}