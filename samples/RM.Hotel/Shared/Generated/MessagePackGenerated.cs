#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Resolvers
{
    using System;
    using MessagePack;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
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
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(3)
            {
                {typeof(global::RM.Hotel.NewGameAction), 0 },
                {typeof(global::RM.Hotel.Transaction), 1 },
                {typeof(global::MagicSpritz.IAction), 2 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new MessagePack.Formatters.RM.Hotel.NewGameActionFormatter();
                case 1: return new MessagePack.Formatters.RM.Hotel.TransactionFormatter();
                case 2: return new MessagePack.Formatters.RM.Hotel.ActionFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612



#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.RM.Hotel
{
    using System;
    using MessagePack;

    public sealed class ActionFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MagicSpritz.IAction>
    {
        public int Serialize(ref byte[] bytes, int offset, global::MagicSpritz.IAction value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            if (value is global::RM.Hotel.NewGameAction)
            {
                // TODO: use type hasher -- https://stackoverflow.com/questions/51020619/unique-id-for-each-class to lookup action type based on value.
                // value.GetType().GetHashCode() is inconsistent between app run.
                var formatter = formatterResolver.GetFormatter<global::RM.Hotel.NewGameAction>();
                return formatter.Serialize(ref bytes, offset, value as global::RM.Hotel.NewGameAction, formatterResolver);
            }
            
            return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
        }

        public global::MagicSpritz.IAction Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var formatter = formatterResolver.GetFormatter<global::RM.Hotel.NewGameAction>();
            var result = formatter.Deserialize(bytes, offset, formatterResolver, out readSize);
            return result;
        }
    }

    public sealed class NewGameActionFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::RM.Hotel.NewGameAction>
    {

        public int Serialize(ref byte[] bytes, int offset, global::RM.Hotel.NewGameAction value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 1);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.StartCoins);
            return offset - startOffset;
        }

        public global::RM.Hotel.NewGameAction Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __StartCoins__ = default(int);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __StartCoins__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::RM.Hotel.NewGameAction();
            ____result.StartCoins = __StartCoins__;
            return ____result;
        }
    }


    public sealed class TransactionFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::RM.Hotel.Transaction>
    {

        public int Serialize(ref byte[] bytes, int offset, global::RM.Hotel.Transaction value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Id);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Hash, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::MagicSpritz.IAction>().Serialize(ref bytes, offset, value.Action, formatterResolver);
            return offset - startOffset;
        }

        public global::RM.Hotel.Transaction Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Id__ = default(int);
            var __Hash__ = default(string);
            var __Action__ = default(global::MagicSpritz.IAction);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Id__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Hash__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Action__ = formatterResolver.GetFormatterWithVerify<global::MagicSpritz.IAction>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::RM.Hotel.Transaction();
            ____result.Id = __Id__;
            ____result.Hash = __Hash__;
            ____result.Action = __Action__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
