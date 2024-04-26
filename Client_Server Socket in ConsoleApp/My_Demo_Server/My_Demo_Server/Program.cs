using System.Net;
using System.Net.Sockets;
using System.Text;

    //Server Code
    //Info about our localhost --includes the ip address
    IPHostEntry ipEntry=await Dns.GetHostEntryAsync(Dns.GetHostName());

//we will extract the local host ip
IPAddress ip = ipEntry.AddressList[0];

//Connects the server socket to client socket
IPEndPoint iPEndPoint = new(ip,1234);

using Socket server = new(
    iPEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp
    );

server.Bind(iPEndPoint);
server.Listen();
Console.WriteLine("Server Started Listening on port: 1234");

var handler = await server.AcceptAsync();

while (true) {
    var buffer = new byte[1_024];
    //receive the message from client but as Bytes
    var received = await handler.ReceiveAsync(buffer,SocketFlags.None);
    //convert bytes to string message
    var messageString = Encoding.UTF8.GetString(buffer,0,received);

    if (messageString != null) { 
        Console.WriteLine("Message from client: {0}",messageString);
        var response = "Message Received!";

        var responseByte = Encoding.UTF8.GetBytes(response);

        await handler.SendAsync(responseByte,SocketFlags.None);
    
    }





}


