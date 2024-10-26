using System;

namespace ConfigureSample;

public class RemoteTcpServerSettings
{
    public const string Default = nameof(RemoteTcpServerSettings);

    public string Ip { get; set; }

    public int Port { get; set; }

    public string Description { get; set; }

}
