using FluentModbus;
using System.Net;

var client = new ModbusTcpClient();
bool connect = false;

Connect(connect);



while (true)
{
   
    System.Threading.Thread.Sleep(500);
    try
    {
        var ShortData = client.ReadInputRegisters<float>(0xFF, 4, 1);
        foreach (var value in ShortData)
        {
            Console.WriteLine(value);
        }
    }
    catch {
        connect = client.IsConnected;
        Connect(connect);
    } 

}
    Console.ReadLine();

void Connect(bool connect)
{
    while (!connect)
    {

        try
        {
            client.Connect(new IPEndPoint(IPAddress.Parse("172.17.5.103"), 502), ModbusEndianness.BigEndian);
            connect = client.IsConnected;
            Console.WriteLine("Connected");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}

