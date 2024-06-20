using System;

[AttributeUsage(AttributeTargets.Field)]
public class ListenAttribute : Attribute
{
  public string ChannelName;

  public ListenAttribute(string channelName)
  {
    ChannelName = channelName;
  }
}