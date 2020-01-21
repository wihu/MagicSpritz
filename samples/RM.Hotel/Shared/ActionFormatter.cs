namespace RM.Hotel.Shared.Resolvers
{
    using System;
    using MessagePack;
    using MessagePack.Formatters;
    using System.Collections.Generic;

    public class CustomResolver : IFormatterResolver
    {
        public static readonly MessagePack.IFormatterResolver Instance = new CustomResolver();

        IMessagePackFormatter<T> IFormatterResolver.GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GetFormatterHelper.GetFormatter<T>();
                if (f != null)
                {
                    formatter = f as IMessagePackFormatter<T>;
                }
            }
        }

        static class GetFormatterHelper
        {
            static readonly Dictionary<Type, int> _lookup;

            static GetFormatterHelper()
            {
                _lookup = new Dictionary<Type, int>(1)
                {
                    {typeof(MagicSpritz.IAction), 0},
                };
            }

            internal static IMessagePackFormatter GetFormatter<T>()
            {
                if (!_lookup.TryGetValue(typeof(T), out int key))
                {
                    return null;
                }
                switch (key)
                {
                    case 0: return new RM.Hotel.Shared.Formatters.ActionFormatter();
                    default: return null;
                }
            }
        }
    }
}

namespace RM.Hotel.Shared.Formatters
{
    using System;
    using System.Collections.Generic;
    using MessagePack;
    using MessagePack.Formatters;

    public sealed class ActionFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MagicSpritz.IAction>
    {
        public int Serialize(ref byte[] bytes, int offset, global::MagicSpritz.IAction value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            return FormatterHelper.Serialize(ref bytes, offset, value, formatterResolver);;
        }

        public global::MagicSpritz.IAction Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            return FormatterHelper.Deserialize(bytes, offset, formatterResolver, out readSize);
        }

        static class FormatterHelper
        {
            static readonly Dictionary<Type, int> _lookup = new Dictionary<Type, int>();

            static FormatterHelper()
            {
                _lookup = new Dictionary<Type, int>(1)
                {
                    {typeof(global::RM.Hotel.NewGameAction), 0},
                    {typeof(global::RM.Hotel.BuyDecoAction), 1},
                    {typeof(global::RM.Hotel.UpgradeRoomAction), 2},
                };
            }

            internal static int GetId(Type type)
            {
                if (!_lookup.TryGetValue(type, out int key))
                {
                    return -1;
                }
                return key;
            }

            internal static int Serialize(ref byte[] bytes, int offset, global::MagicSpritz.IAction value, IFormatterResolver formatterResolver)
            {
                int typeId = GetId(value.GetType());
                if (typeId == -1)
                {
                    return MessagePackBinary.WriteNil(ref bytes, offset);
                }
                int startOffset = offset;
                offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, typeId);
                
                switch (typeId)
                {
                    case 0: offset += formatterResolver.GetFormatterWithVerify<global::RM.Hotel.NewGameAction>().Serialize(ref bytes, offset, value as NewGameAction, formatterResolver); break;
                    case 1: offset += formatterResolver.GetFormatterWithVerify<global::RM.Hotel.BuyDecoAction>().Serialize(ref bytes, offset, value as BuyDecoAction, formatterResolver); break;
                    case 2: offset += formatterResolver.GetFormatterWithVerify<global::RM.Hotel.UpgradeRoomAction>().Serialize(ref bytes, offset, value as UpgradeRoomAction, formatterResolver); break;
                    default: offset += MessagePackBinary.WriteNil(ref bytes, offset); break;
                }

                return offset - startOffset;
            }

            internal static global::MagicSpritz.IAction Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
            {

                int startOffset = offset;
                int typeId = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
                if (typeId == -1)
                {
                    MessagePackBinary.ReadNil(bytes, offset, out readSize);
                    return null;
                }
                offset += readSize;

                var result = (global::MagicSpritz.IAction)null;
                switch (typeId)
                {
                    case 0: result = formatterResolver.GetFormatterWithVerify<global::RM.Hotel.NewGameAction>().Deserialize(bytes, offset, formatterResolver, out readSize); break;
                    case 1: result = formatterResolver.GetFormatterWithVerify<global::RM.Hotel.BuyDecoAction>().Deserialize(bytes, offset, formatterResolver, out readSize); break;
                    case 2: result = formatterResolver.GetFormatterWithVerify<global::RM.Hotel.UpgradeRoomAction>().Deserialize(bytes, offset, formatterResolver, out readSize); break;
                    default: result = null; MessagePackBinary.ReadNil(bytes, offset, out readSize); break;
                }
                offset += readSize;
                readSize = offset - startOffset;
                return result;
            }
        }
    }
}
