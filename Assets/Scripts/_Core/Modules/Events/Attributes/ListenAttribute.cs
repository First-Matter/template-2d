using System;

[AttributeUsage(AttributeTargets.Field)]
public class SubscribeAttribute : Attribute
{
  public string ChannelName;

  public SubscribeAttribute(string channelName)
  {
    ChannelName = channelName;
  }
}