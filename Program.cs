
using System;
using System.Net;
using System.Net.NetworkInformation;
Console.WriteLine("=======================");
Console.WriteLine("Network Diagnostic Tool");
Console.WriteLine("=======================");

Console.WriteLine("Enter Hostname:");

var hostname = Console.ReadLine()??"";
if(string.IsNullOrWhiteSpace(hostname)){
    Console.WriteLine("請輸入至少一個實際網域名");
    return;
}

var website = hostname.Split([';',' ',','],StringSplitOptions.RemoveEmptyEntries);

foreach(var host in website){
    await CheckHost(host);
}

static async Task CheckHost(string host)
{
    Console.WriteLine("===================================");
    Console.WriteLine($"Your entered:{host}");
    Console.WriteLine("\n");
    Console.WriteLine($"正在解析:{host}...");
    var p = new Ping();
    
    try{
        var addresses = await Dns.GetHostAddressesAsync(host);
        foreach(var ip in addresses){
            Console.WriteLine($"Ip位址: \n{ip}");
            PingReply reply = await p.SendPingAsync(ip);
            if(reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"延遲: {reply.RoundtripTime} ms");
            }
            else
            {
                Console.WriteLine($"{reply.Status}");
            }
        }
        Console.WriteLine("Ping 成功");
    }
    catch(Exception ex){
        Console.WriteLine($"解析錯誤:{ex.Message}");
    }
    Console.WriteLine("===================================");
    Console.WriteLine("\n");
}
