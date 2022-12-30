// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;


Console.WriteLine("Message sent to the broadcast address");

Console.WriteLine("Hello to udp package sender simulator");
Console.WriteLine("Press any button to start the service");
Console.ReadKey();

BackgroundServiceTimerHelper backgroundServiceTimerHelper = new(TimeSpan.FromMilliseconds(1000));
backgroundServiceTimerHelper.Start();

Console.WriteLine("Press any button to stop the service");
Console.ReadKey();

await backgroundServiceTimerHelper.StopAsync();


internal class BackgroundServiceTimerHelper
{
    private readonly PeriodicTimer timer;
    private readonly CancellationTokenSource cts = new();
    private Task? timerTask;

    public BackgroundServiceTimerHelper(TimeSpan timerInterval)
    {
        timer = new(timerInterval);
    }

    public void Start()
    {
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        IPAddress broadcast = IPAddress.Parse("192.168.0.147");
        IPEndPoint ep = new IPEndPoint(broadcast, 11000);
        byte[] sendbuf = Encoding.ASCII.GetBytes("Hello, World!");
        byte[] sendbuf2 = Encoding.ASCII.GetBytes("You, World!");
        byte[] sendbuf3 = Encoding.ASCII.GetBytes("Thank You, World!");
        byte[] sendbuf4 = Encoding.ASCII.GetBytes("I am, World!");
        byte[] sendbuf5 = Encoding.ASCII.GetBytes("For You, World!");
        
        timerTask = DoWorkAsync(s, ep, sendbuf, sendbuf2, sendbuf3, sendbuf4, sendbuf5);
        Console.WriteLine("udp package sender simulator just started");
    }

    private async Task DoWorkAsync(Socket s, IPEndPoint ep, byte[]  sendbuf, byte[] sendbuf2, byte[] sendbuf3,
        byte[] sendbuf4, byte[] sendbuf5)
    {
        try
        {
            while (await timer.WaitForNextTickAsync(cts.Token))
            {
                s.SendTo(sendbuf, ep);
                Console.WriteLine(string.Concat("Hello, World! ", DateTime.Now.ToString("O")));
                s.SendTo(sendbuf2, ep);
                Console.WriteLine(string.Concat("Hi, World! ", DateTime.Now.ToString("O")));
                s.SendTo(sendbuf3, ep);
                Console.WriteLine(string.Concat("Thank You, World! ", DateTime.Now.ToString("O")));
                s.SendTo(sendbuf4, ep);
                Console.WriteLine(string.Concat("I am, World! ", DateTime.Now.ToString("O")));
                s.SendTo(sendbuf5, ep);
                Console.WriteLine(string.Concat("For You, World! ", DateTime.Now.ToString("O")));
                await Task.Delay(500);
            }
        }
        catch (OperationCanceledException)
        {

        }
    }

    public async Task StopAsync()
    {
        if (timerTask is null)
        {
            return;
        }

        cts.Cancel();
        await timerTask;
        cts.Dispose();
        Console.WriteLine("Udp package sender simulator just stopped");
        Console.ReadKey();
    }
}