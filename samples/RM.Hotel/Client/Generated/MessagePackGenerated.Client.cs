#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Resolvers.Client
{
    using System;
    using MessagePack;
    using MessagePack.Formatters;
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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(7)
            {
                {typeof(global::System.Collections.Immutable.ImmutableList<global::RM.Hotel.Models.Room>), 0 },
                // {typeof(global::RM.Hotel.NewGameAction), 1 },
                {typeof(global::RM.Hotel.Models.Room), 2 },
                {typeof(global::RM.Hotel.Models.Hotel), 3 },
                {typeof(global::RM.Hotel.Models.Stats), 4 },
                {typeof(global::RM.Hotel.Models.PlayerData), 5 },
                // {typeof(global::RM.Hotel.Transaction), 6 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.ImmutableCollection.ImmutableListFormatter<global::RM.Hotel.Models.Room>();
                // case 1: return new MessagePack.Formatters.RM.Hotel.NewGameActionFormatter();
                case 2: return new MessagePack.Formatters.RM.Hotel.Models.RoomFormatter();
                case 3: return new MessagePack.Formatters.RM.Hotel.Models.HotelFormatter();
                case 4: return new MessagePack.Formatters.RM.Hotel.Models.StatsFormatter();
                case 5: return new MessagePack.Formatters.RM.Hotel.Models.PlayerDataFormatter();
                // case 6: return new MessagePack.Formatters.RM.Hotel.TransactionFormatter();
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

namespace MessagePack.Formatters.RM.Hotel.Models
{
    using System;
    using MessagePack;


    public sealed class RoomFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::RM.Hotel.Models.Room>
    {

        public int Serialize(ref byte[] bytes, int offset, global::RM.Hotel.Models.Room value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TypeId);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Level);
            return offset - startOffset;
        }

        public global::RM.Hotel.Models.Room Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __TypeId__ = default(int);
            var __Level__ = default(int);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __TypeId__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Level__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::RM.Hotel.Models.Room();
            ____result.TypeId = __TypeId__;
            ____result.Level = __Level__;
            return ____result;
        }
    }


    public sealed class HotelFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::RM.Hotel.Models.Hotel>
    {

        public int Serialize(ref byte[] bytes, int offset, global::RM.Hotel.Models.Hotel value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Level);
            offset += global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Immutable.ImmutableList<global::RM.Hotel.Models.Room>>().Serialize(ref bytes, offset, value.Rooms, formatterResolver);
            return offset - startOffset;
        }

        public global::RM.Hotel.Models.Hotel Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(string);
            var __Level__ = default(int);
            var __Rooms__ = default(global::System.Collections.Immutable.ImmutableList<global::RM.Hotel.Models.Room>);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Level__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __Rooms__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Immutable.ImmutableList<global::RM.Hotel.Models.Room>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::RM.Hotel.Models.Hotel();
            ____result.Name = __Name__;
            ____result.Level = __Level__;
            ____result.Rooms = __Rooms__;
            return ____result;
        }
    }


    public sealed class StatsFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::RM.Hotel.Models.Stats>
    {

        public int Serialize(ref byte[] bytes, int offset, global::RM.Hotel.Models.Stats value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Level);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Xp);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Coins);
            return offset - startOffset;
        }

        public global::RM.Hotel.Models.Stats Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Level__ = default(int);
            var __Xp__ = default(int);
            var __Coins__ = default(int);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Level__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Xp__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __Coins__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::RM.Hotel.Models.Stats();
            ____result.Level = __Level__;
            ____result.Xp = __Xp__;
            ____result.Coins = __Coins__;
            return ____result;
        }
    }


    public sealed class PlayerDataFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::RM.Hotel.Models.PlayerData>
    {

        public int Serialize(ref byte[] bytes, int offset, global::RM.Hotel.Models.PlayerData value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
            offset += formatterResolver.GetFormatterWithVerify<global::RM.Hotel.Models.Stats>().Serialize(ref bytes, offset, value.Stats, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<global::RM.Hotel.Models.Hotel>().Serialize(ref bytes, offset, value.Hotel, formatterResolver);
            return offset - startOffset;
        }

        public global::RM.Hotel.Models.PlayerData Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                throw new InvalidOperationException("typecode is null, struct not supported");
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Stats__ = default(global::RM.Hotel.Models.Stats);
            var __Hotel__ = default(global::RM.Hotel.Models.Hotel);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Stats__ = formatterResolver.GetFormatterWithVerify<global::RM.Hotel.Models.Stats>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Hotel__ = formatterResolver.GetFormatterWithVerify<global::RM.Hotel.Models.Hotel>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::RM.Hotel.Models.PlayerData();
            ____result.Stats = __Stats__;
            ____result.Hotel = __Hotel__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
