using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using FCUUsbService.Models;
using System.Runtime.CompilerServices;

namespace FCUUsbService.Services;

public class DhcpClientProvider : ModelProvider
{
    private const int macIndex = 1, ipIndex = 3, hostnameIndex = 5, beginDateIndex = 7,
        beginTimeIndex = 8, endDateIndex = 10, endTimeIndex = 11;
    private readonly DhcpLeaseList dhcpLeaseList = new();

    public IEnumerable<DhcpClient> GetDhcpClients()
    { 
        var split = dhcpLeaseList.Run();

        if (split is null || split.Count() is <= 0) return [];
        
        var res = new List<DhcpClient>();
        
        foreach (var v in split)
            res.Add(
                new()
                {
                    MAC = PhysicalAddress.Parse(v[macIndex]),
                    IP = IPAddress.Parse(v[ipIndex]),
                    HostName = v[hostnameIndex],
                    Begin = DateTimeOffset.Parse($"{v[beginDateIndex]} {v[beginTimeIndex]}"),
                    End = DateTimeOffset.Parse($"{v[endDateIndex]} {v[endTimeIndex]}"),
                }
            );
        
        return res;
    }
}
