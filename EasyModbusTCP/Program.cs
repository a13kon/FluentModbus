using FluentModbus;
using System.Net;

var client = new ModbusTcpClient();
bool connect = false;
const string ip = "172.17.5.103";
const int port = 502;

Connect(connect, ip, port);

while (true)
{
   
    Thread.Sleep(500);
    try
    {
        var floatData = client.ReadInputRegisters<float>(0xFF, 4, 1);
        foreach (var value in floatData)
        {
            Console.WriteLine(value);
        }
    }
    catch {
        connect = client.IsConnected;
        Connect(connect, ip, port);
    } 

}

void Connect(bool connect, string ip, int port)
{
    while (!connect)
    {

        try
        {
            client.Connect(new IPEndPoint(IPAddress.Parse(ip), port), ModbusEndianness.BigEndian);
            connect = client.IsConnected;
            Console.WriteLine("Connected");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}

