using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private readonly TcpListener _listener;
        private readonly List<TcpClient> _clients = [];
        private readonly List<Task> _clientTasks = [];
        private CancellationTokenSource? _cts;

        public Server()
        {
            _listener = new(IPAddress.Parse("127.0.0.1"), 9000);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _listener.Start();

            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    TcpClient client = null;
                    try
                    {
                        var acceptTask = _listener.AcceptTcpClientAsync();
                        var completedTask = await Task.WhenAny(acceptTask, Task.Delay(-1, _cts.Token));
                        if (completedTask == acceptTask)
                        {
                            client = acceptTask.Result;
                        }
                        else
                        {
                            // Cancellation requested
                            break;
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        // Cancellation requested
                        break;
                    }
                    catch (SocketException)
                    {
                        // Listener stopped, exit
                        break;
                    }
                    catch (InvalidOperationException)
                    {
                        // Listener stopped, exit
                        break;
                    }

                    if (client == null)
                        break;

                    lock (_clients)
                    {
                        _clients.Add(client);
                    }

                    var clientHandler = new ClientHandler(client, this);

                    // Start handling client and keep track of the task
                    var task = Task.Run(() => clientHandler.HandleClientAsync(_cts.Token), cancellationToken);
                    clientHandler.RunningTask = task;
                    lock (_clientTasks)
                    {
                        _clientTasks.Add(task);
                    }

                    Console.WriteLine("Client connected");
                }
            }
            finally
            {
                Console.WriteLine("Server stopping accept loop.");
            }
        }

        public async Task StopAsync()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
                _cts.Cancel();

            // Close clients
            lock (_clients)
            {
                foreach (var client in _clients)
                {
                    try
                    {
                        client.Close();
                    }
                    catch { }
                }
                _clients.Clear();
            }

            _listener.Stop();
            Console.WriteLine("Server stopped");

            // Wait for all client tasks to complete
            Task[] tasksToWait;
            lock (_clientTasks)
            {
                tasksToWait = _clientTasks.ToArray();
            }
            await Task.WhenAll(tasksToWait);
        }

        public void RemoveClient(TcpClient client, Task clientTask)
        {
            lock (_clients)
            {
                _clients.Remove(client);
            }
            lock (_clientTasks)
            {
                _clientTasks.Remove(clientTask);
            }
            Console.WriteLine("Client removed from server lists");
        }

    }

}
