using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WinBM.Server.WebSocketConnect.Session
{
    public class SessionMessage
    {
        private JsonSerializerOptions _option = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public virtual MessageType MessageType { get; }

        public string Content { get; set; }



        public ArraySegment<byte> GetPayload()
        {
            return new ArraySegment<byte>(
                Encoding.UTF8.GetBytes(
                    JsonSerializer.Serialize(this, _option)));
        }
    }
}
