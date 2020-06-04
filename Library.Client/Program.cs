using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Library.Server;

namespace Library.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Books.BooksClient(channel);
            var reply = client.GetBooks(new GetBooksRequest
            {
                IsAvailable = true
            });
            foreach (var item in reply.Books)
            {
                Console.WriteLine($"Item id:{item.Id}");
                Console.WriteLine($"Item Title:{item.Title}");
                foreach (var category in item.CategoryIds)
                {
                    Console.WriteLine($"Category Id:{category}");
                }
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
