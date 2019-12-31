#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::MagicOnion;
    using global::MagicOnion.Client;

    public static partial class MagicOnionInitializer
    {
        static bool isRegistered = false;

        // [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            if(isRegistered) return;
            isRegistered = true;

            MagicOnionClientRegistry<RM.Hotel.IStoreService>.Register((x, y, z) => new RM.Hotel.IStoreServiceClient(x, y, z));

        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace MagicOnion.Resolvers
{
    using System;
    using MessagePack;

    public class MagicOnionResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new MagicOnionResolver();

        MagicOnionResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = MagicOnionResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class MagicOnionResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static MagicOnionResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(0)
            {
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace RM.Hotel {
    using System;
	using MagicOnion;
    using MagicOnion.Client;
    using Grpc.Core;
    using MessagePack;

    public class IStoreServiceClient : MagicOnionClientBase<global::RM.Hotel.IStoreService>, global::RM.Hotel.IStoreService
    {
        static readonly Method<byte[], byte[]> UpdateMethod;
        static readonly Func<RequestContext, ResponseContext> UpdateDelegate;

        static IStoreServiceClient()
        {
            UpdateMethod = new Method<byte[], byte[]>(MethodType.Unary, "IStoreService", "Update", MagicOnionMarshallers.ThroughMarshaller, MagicOnionMarshallers.ThroughMarshaller);
            UpdateDelegate = _Update;
        }

        IStoreServiceClient()
        {
        }

        public IStoreServiceClient(CallInvoker callInvoker, IFormatterResolver resolver, IClientFilter[] filters)
            : base(callInvoker, resolver, filters)
        {
        }

        protected override MagicOnionClientBase<IStoreService> Clone()
        {
            var clone = new IStoreServiceClient();
            clone.host = this.host;
            clone.option = this.option;
            clone.callInvoker = this.callInvoker;
            clone.resolver = this.resolver;
            clone.filters = filters;
            return clone;
        }

        public new IStoreService WithHeaders(Metadata headers)
        {
            return base.WithHeaders(headers);
        }

        public new IStoreService WithCancellationToken(System.Threading.CancellationToken cancellationToken)
        {
            return base.WithCancellationToken(cancellationToken);
        }

        public new IStoreService WithDeadline(System.DateTime deadline)
        {
            return base.WithDeadline(deadline);
        }

        public new IStoreService WithHost(string host)
        {
            return base.WithHost(host);
        }

        public new IStoreService WithOptions(CallOptions option)
        {
            return base.WithOptions(option);
        }
   
        static ResponseContext _Update(RequestContext __context)
        {
            return CreateResponseContext<global::RM.Hotel.Transaction, global::RM.Hotel.Transaction>(__context, UpdateMethod);
        }

        public global::MagicOnion.UnaryResult<global::RM.Hotel.Transaction> Update(global::RM.Hotel.Transaction t)
        {
            return InvokeAsync<global::RM.Hotel.Transaction, global::RM.Hotel.Transaction>("IStoreService/Update", t, UpdateDelegate);
        }
    }
}

#pragma warning restore 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 612
#pragma warning restore 618
