namespace Server
{
    internal class Program
    {
        static Server? server;
        static CancellationTokenSource cts = new();

        static async Task Main(string[] args)
        {
            Console.Title = "Server";
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                cts.Cancel();
            };

            Console.WriteLine("Press any key to start the server");
            Console.ReadKey();

            server = new Server();

            // Start the server and pass cancellation token
            var serverTask = server.StartAsync(cts.Token);

            Console.WriteLine("Server started, press ESC to exit");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    cts.Cancel();
                    break;
                }
            }

            await server.StopAsync();

            // Wait for server task to complete
            await serverTask;

            Console.WriteLine("Server stopped");
        }
    }
}
