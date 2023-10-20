﻿using Microsoft.AspNetCore.Http;
namespace Core.SSE
{
    public class SSEClient
    {
        public int Id { get; set; }
        public HttpResponse Client { get; set; }
        public DateTime JoinTime { get; set; }

        private SSEClient(HttpResponse response) =>
            (Id, Client, JoinTime) = ( ,response, DateTime.Now);

        public static SSEClient New(HttpResponse response) =>
            new SSEClient(response);
    }
}
