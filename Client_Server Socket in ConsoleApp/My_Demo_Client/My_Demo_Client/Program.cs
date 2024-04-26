using System.Net;
using System.Net.Sockets;
using System.Text;

//Client Code

//Info about localhost
IPHostEntry ipEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());

//localhost ip address----127.0.1.1
IPAddress ip= ipEntry.AddressList[0];

IPEndPoint iPEndPoint = new(ip,1234);

//client socket
using Socket client = new(
    iPEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp
    );
await client.ConnectAsync(iPEndPoint);

while (true) {
    Console.Write("Send a Message: ");
    var message=Console.ReadLine();

    //convert the string message to bytes message
    var messageBytes = Encoding.UTF8.GetBytes(message);

    //sends the message to the server
    await client.SendAsync(messageBytes,SocketFlags.None);

    var buffer = new byte[1_024];

    //we received the message in the form of bytes
    var received = await client.ReceiveAsync(buffer,SocketFlags.None);

    var messageString = Encoding.UTF8.GetString(buffer,0,received);

    Console.WriteLine(messageString);

}
