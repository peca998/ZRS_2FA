using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Communication
{
    public class JsonNetworkSerializer
    {
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        public JsonNetworkSerializer(Stream stream)
        {
            _reader = new StreamReader(stream);
            _writer = new StreamWriter(stream) { AutoFlush = true };
        }

        public async Task SendAsync(object data)
        {
            string json = JsonSerializer.Serialize(data);
            await _writer.WriteLineAsync(json);
        }

        public async Task<T> ReceiveAsync<T>()
        {
            string? line = await _reader.ReadLineAsync() ?? throw new InvalidOperationException("Received null line from stream.");
            return JsonSerializer.Deserialize<T>(line) 
                   ?? throw new InvalidOperationException("Deserialization returned null.");
        }

        public T ReadType<T>(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");
            }
            return JsonSerializer.Deserialize<T>((JsonElement)data) 
                   ?? throw new InvalidOperationException("Deserialization returned null.");
        }

        public void Close()
        {
            _reader.Close();
            _writer.Close();
        }
    }
}
