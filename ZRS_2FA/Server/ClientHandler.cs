using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientHandler
    {
        private JsonNetworkSerializer _serializer;
        private readonly TcpClient _client;
        private readonly Server _server;
        public long UserId { get; set; } = -1;
        public Task? RunningTask { get; set; }


        public ClientHandler(TcpClient client, Server server)
        {
            _client = client;
            _server = server;
            _serializer = new JsonNetworkSerializer(_client.GetStream());
        }

        public async Task HandleClientAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (_client.Connected && !cancellationToken.IsCancellationRequested)
                {
                    // You might want to check if data is available or wrap ReceiveAsync with timeout/cancellation support
                    var receiveTask = _serializer.ReceiveAsync<Request>();
                    var completedTask = await Task.WhenAny(receiveTask, Task.Delay(-1, cancellationToken));

                    if (completedTask != receiveTask)
                    {
                        // Cancellation requested, exit loop
                        break;
                    }

                    Request request = await receiveTask; // now completed successfully
                    Response response = ProcessRequest(request);

                    var sendTask = _serializer.SendAsync(response);
                    var completedSend = await Task.WhenAny(sendTask, Task.Delay(-1, cancellationToken));

                    if (completedSend != sendTask)
                    {
                        // Cancellation requested during sending
                        break;
                    }

                    await sendTask; // complete send
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when cancellation token triggers
                Console.WriteLine("Client handler cancellation requested.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error handling client: {e.Message}");
            }
            finally
            {
                try
                {
                    _serializer.Close();
                }
                catch { }

                try
                {
                    _client.GetStream().Close();
                }
                catch { }

                try
                {
                    _client.Close();
                }
                catch { }

                if (RunningTask != null)
                {
                    _server.RemoveClient(_client, RunningTask);
                }
                Console.WriteLine("Client disconnected and cleaned up.");
            }
        }

        public Response ProcessRequest(Request request)
        {
            Response response = new();

            try
            {
                switch (request.Operation)
                {
                    case Operation.Register:
                        Controller.Instance.Register(_serializer.ReadType<Credentials>(request.Argument));
                        break;
                    case Operation.LoginFirstStep:
                        LoginResultData rd = Controller.Instance.LoginFirstStep(_serializer.ReadType<Credentials>(request.Argument));
                        response.Result = rd.LoginResult;
                        UserId = rd.UserId;
                        break;
                    case Operation.EnableTwoFactorInit:
                        response.Result = Controller.Instance.EnableTwoFaInit(UserId);
                        break;
                    case Operation.EnableTwoFactorConfirm:
                        response.Result = Controller.Instance.EnableTwoFaConfirm(UserId, _serializer.ReadType<string>(request.Argument));
                        break;
                    case Operation.LoginSecondStep:
                        response.Result = Controller.Instance.LoginSecondStep(_serializer.ReadType<Credentials>(request.Argument));
                        break;
                    case Operation.UseBackupCode:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                response.ErrorMessage = e.Message;
            }

            return response;
        }
    }

}
