using Common.Communication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Communication
    {
        private TcpClient? _client;
        private JsonNetworkSerializer _serializer;
        private static Communication? _instance;
        public static Communication Instance => _instance ??= new();
        private Communication()
        {
        
        }
        public bool Connnect()
        {
            try
            {
                _client = new();
                _client.Connect("127.0.0.1", 9000);
                _serializer = new JsonNetworkSerializer(_client.GetStream());
                return true;
            }
            catch (SocketException e)
            {
                Debug.WriteLine($"SocketException: {e.Message}");
                return false;
            }
        }

        public async Task<string?> Register(string username, string password)
        {
            Credentials credentials = new()
            {
                Username = username,
                Password = password
            };
            Request request = new()
            {
                Operation = Operation.Register,
                Argument = credentials
            };
            await _serializer.SendAsync(request);
            Response response = await _serializer.ReceiveAsync<Response>();
            return response.ErrorMessage;
        }

        public async Task<(string?, LoginResult)> LoginFirstStep(string username, string password)
        {
            Credentials credentials = new()
            {
                Username = username,
                Password = password
            };
            Request request = new()
            {
                Operation = Operation.LoginFirstStep,
                Argument = credentials
            };
            await _serializer.SendAsync(request);
            Response response = await _serializer.ReceiveAsync<Response>();
            if(response.Result != null)
            {
                response.Result = _serializer.ReadType<LoginResult>(response.Result);
            }
            else
            {
                response.Result = LoginResult.WrongPassword;
            }
            return (response.ErrorMessage, (LoginResult)response.Result);
        }

        public async Task<string?> EnableTwoFaInit(bool regenerate)
        {
            Request request = new()
            {
                Operation = Operation.EnableTwoFactorInit,
                Argument = regenerate
            };
            await _serializer.SendAsync(request);
            Response response = await _serializer.ReceiveAsync<Response>();
            if (response.ErrorMessage != null)
            {
                MessageBox.Show(response.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (response.Result == null)
            {
                MessageBox.Show("Unexpected response from server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return _serializer.ReadType<string>(response.Result);
        }

        public async Task<List<string>> EnableTwoFaConfirm(string code)
        {
            Request request = new()
            {
                Operation = Operation.EnableTwoFactorConfirm,
                Argument = code
            };
            await _serializer.SendAsync(request);
            Response response = await _serializer.ReceiveAsync<Response>();
            List<string> r = [];
            if (response.ErrorMessage != null)
            {
                MessageBox.Show(response.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return r;
            }
            if(response.Result != null)
            {
                r = _serializer.ReadType<List<string>>(response.Result);
            }
            return r;
        }

        public async Task<(string?, LoginResult)> LoginSecondStep(string username, string code)
        {
            Credentials credentials = new()
            {
                Username = username,
                Password = code
            };
            Request request = new()
            {
                Operation = Operation.LoginSecondStep,
                Argument = credentials
            };
            await _serializer.SendAsync(request);
            Response response = await _serializer.ReceiveAsync<Response>();
            LoginResult r;
            if (response.Result != null)
            {
                r = _serializer.ReadType<LoginResult>(response.Result);
            }
            else
            {
                r = LoginResult.InTimeout;
            }
            return (response.ErrorMessage, r);
        }

        public async Task<(string?, LoginResult)> LoginBackupCode(string username, string code)
        {
            Credentials credentials = new()
            {
                Username = username,
                Password = code
            };
            Request request = new()
            {
                Operation = Operation.UseBackupCode,
                Argument = credentials
            };
            await _serializer.SendAsync(request);
            Response response = await _serializer.ReceiveAsync<Response>();
            LoginResult r;
            if (response.Result != null)
            {
                r = _serializer.ReadType<LoginResult>(response.Result);
            }
            else
            {
                r = LoginResult.InTimeout;
            }
            return (response.ErrorMessage, r);
        }
    }
}
