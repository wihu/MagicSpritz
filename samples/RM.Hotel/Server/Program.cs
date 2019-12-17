using Grpc.Core;
using MagicOnion.Hosting;
using MagicOnion.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MessagePack.Resolvers;

namespace RM.Hotel.Server
{
    class Program
    {
        static void RegisterResolvers()
        {
            CompositeResolver.RegisterAndSetAsDefault
            (
                // MagicOnion.Resolvers.MagicOnionResolver.Instance,
                MessagePack.Resolvers.GeneratedResolver.Instance,
                BuiltinResolver.Instance,
                PrimitiveObjectResolver.Instance
            );
        }

        static async Task Main(string[] args)
        {
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());
            RegisterResolvers();
            
            await MagicOnionHost.CreateDefaultBuilder()
                .UseMagicOnion(
                    new MagicOnionOptions(isReturnExceptionStackTraceInErrorDetail: true)
                    , new ServerPort("0.0.0.0", 8080, ServerCredentials.Insecure)
                )
                .RunConsoleAsync();
        }
    }
}
