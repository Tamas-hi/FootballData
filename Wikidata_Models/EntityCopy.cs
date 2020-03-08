using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json.Converters;
namespace FootballData
{

    public partial class Temperatures
    {
        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("success")]
        public long Success { get; set; }
    }

    public partial class Entities
    {
        [JsonPropertyNameBasedOnItemClassAttribute]
        public ID ID { get; set; }
    }

    public partial class ID
    {
        [JsonProperty("type")]
        public EntityTypeEnum Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("labels")]
        public Labels Labels { get; set; }

        [JsonProperty("claims")]
        public Claims Claims { get; set; }
    }

    public partial class Claims
    {
        [JsonProperty("P641")]
        public P1346[] P641 { get; set; }

        [JsonProperty("P31")]
        public P1346[] P31 { get; set; }

        [JsonProperty("P3450")]
        public P3450[] P3450 { get; set; }

        [JsonProperty("P17")]
        public P1346[] P17 { get; set; }

        [JsonProperty("P1923")]
        public P1923[] P1923 { get; set; }

        [JsonProperty("P2348")]
        public P1346[] P2348 { get; set; }

        [JsonProperty("P1346")]
        public P1346[] P1346 { get; set; }

        [JsonProperty("P1132")]
        public P1132[] P1132 { get; set; }

        [JsonProperty("P582")]
        public P58[] P582 { get; set; }

        [JsonProperty("P580")]
        public P58[] P580 { get; set; }

        [JsonProperty("P1350")]
        public P1132[] P1350 { get; set; }

        [JsonProperty("P1351")]
        public P1132[] P1351 { get; set; }

        [JsonProperty("P664")]
        public P1346[] P664 { get; set; }

        [JsonProperty("P393")]
        public P393[] P393 { get; set; }

        [JsonProperty("P2500")]
        public P1346[] P2500 { get; set; }

        [JsonProperty("P2882")]
        public P1346[] P2882 { get; set; }

        [JsonProperty("P710")]
        public P1346[] P710 { get; set; }
    }

    public partial class P1132
    {
        [JsonProperty("mainsnak")]
        public QualifierElement Mainsnak { get; set; }

        [JsonProperty("type")]
        public FluffyType Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }

        [JsonProperty("references", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleReference[] References { get; set; }
    }

    public partial class QualifierElement
    {
        [JsonProperty("snaktype")]
        public Snaktype Snaktype { get; set; }

        [JsonProperty("property")]
        public Property Property { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("datavalue")]
        public QualifierDatavalue Datavalue { get; set; }

        [JsonProperty("datatype")]
        public DatatypeEnum Datatype { get; set; }
    }

    public partial class QualifierDatavalue
    {
        [JsonProperty("value")]
        public PurpleValue Value { get; set; }

        [JsonProperty("type")]
        public DatatypeEnum Type { get; set; }
    }

    public partial class PurpleValue
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("unit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Unit { get; set; }
    }

    public partial class PurpleReference
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("snaks")]
        public PurpleSnaks Snaks { get; set; }

        [JsonProperty("snaks-order")]
        public SnaksOrder[] SnaksOrder { get; set; }
    }

    public partial class PurpleSnaks
    {
        [JsonProperty("P143")]
        public P155Element[] P143 { get; set; }

        [JsonProperty("P4656")]
        public PurpleMainsnak[] P4656 { get; set; }
    }

    public partial class P155Element
    {
        [JsonProperty("snaktype")]
        public Snaktype Snaktype { get; set; }

        [JsonProperty("property")]
        public string Property { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("datavalue")]
        public P155Datavalue Datavalue { get; set; }

        [JsonProperty("datatype")]
        public Datatype Datatype { get; set; }
    }

    public partial class P155Datavalue
    {
        [JsonProperty("value")]
        public FluffyValue Value { get; set; }

        [JsonProperty("type")]
        public PurpleType Type { get; set; }
    }

    public partial class FluffyValue
    {
        [JsonProperty("entity-type")]
        public EntityTypeEnum EntityType { get; set; }

        [JsonProperty("numeric-id")]
        public long NumericId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class PurpleMainsnak
    {
        [JsonProperty("snaktype")]
        public Snaktype Snaktype { get; set; }

        [JsonProperty("property")]
        public string Property { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("datavalue")]
        public P1545Datavalue Datavalue { get; set; }

        [JsonProperty("datatype")]
        public string Datatype { get; set; }
    }

    public partial class P1545Datavalue
    {
        [JsonProperty("value")]
        public ValueUnion Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class P1346
    {
        [JsonProperty("mainsnak")]
        public P155Element Mainsnak { get; set; }

        [JsonProperty("type")]
        public FluffyType Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }

        [JsonProperty("references", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleReference[] References { get; set; }
    }

    public partial class P1923
    {
        [JsonProperty("mainsnak")]
        public P155Element Mainsnak { get; set; }

        [JsonProperty("type")]
        public FluffyType Type { get; set; }

        [JsonProperty("qualifiers")]
        public Dictionary<string, QualifierElement[]> Qualifiers { get; set; }

        [JsonProperty("qualifiers-order")]
        public Property[] QualifiersOrder { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }
    }

    public partial class P3450
    {
        [JsonProperty("mainsnak")]
        public P155Element Mainsnak { get; set; }

        [JsonProperty("type")]
        public FluffyType Type { get; set; }

        [JsonProperty("qualifiers")]
        public Qualifiers Qualifiers { get; set; }

        [JsonProperty("qualifiers-order")]
        public string[] QualifiersOrder { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }
    }

    public partial class Qualifiers
    {
        [JsonProperty("P155")]
        public P155Element[] P155 { get; set; }

        [JsonProperty("P1545")]
        public PurpleMainsnak[] P1545 { get; set; }

        [JsonProperty("P156")]
        public P155Element[] P156 { get; set; }
    }

    public partial class P393
    {
        [JsonProperty("mainsnak")]
        public PurpleMainsnak Mainsnak { get; set; }

        [JsonProperty("type")]
        public FluffyType Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }

        [JsonProperty("references")]
        public P393Reference[] References { get; set; }
    }

    public partial class P393Reference
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("snaks")]
        public FluffySnaks Snaks { get; set; }

        [JsonProperty("snaks-order")]
        public SnaksOrder[] SnaksOrder { get; set; }
    }

    public partial class FluffySnaks
    {
        [JsonProperty("P143")]
        public P155Element[] P143 { get; set; }
    }

    public partial class P58
    {
        [JsonProperty("mainsnak")]
        public P580Mainsnak Mainsnak { get; set; }

        [JsonProperty("type")]
        public FluffyType Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public Rank Rank { get; set; }

        [JsonProperty("references")]
        public PurpleReference[] References { get; set; }
    }

    public partial class P580Mainsnak
    {
        [JsonProperty("snaktype")]
        public Snaktype Snaktype { get; set; }

        [JsonProperty("property")]
        public string Property { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("datavalue")]
        public PurpleDatavalue Datavalue { get; set; }

        [JsonProperty("datatype")]
        public string Datatype { get; set; }
    }

    public partial class PurpleDatavalue
    {
        [JsonProperty("value")]
        public TentacledValue Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class TentacledValue
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("timezone")]
        public long Timezone { get; set; }

        [JsonProperty("before")]
        public long Before { get; set; }

        [JsonProperty("after")]
        public long After { get; set; }

        [JsonProperty("precision")]
        public long Precision { get; set; }

        [JsonProperty("calendarmodel")]
        public Uri Calendarmodel { get; set; }
    }

    public partial class Labels
    {
        [JsonProperty("en")]
        public En En { get; set; }
    }

    public partial class En
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public enum DatatypeEnum { Quantity };

    public enum Property { P1132, P1350, P1351, P1352, P1355, P1356, P1357, P1359 };

    public enum Snaktype { Value };

    public enum Rank { Normal };

    public enum Datatype { WikibaseItem };

    public enum PurpleType { WikibaseEntityid };

    public enum EntityTypeEnum { Item };

    public enum SnaksOrder { P143, P4656 };

    public enum FluffyType { Statement };

    public partial struct ValueUnion
    {
        public long? Integer;
        public Uri PurpleUri;

        public static implicit operator ValueUnion(long Integer) => new ValueUnion { Integer = Integer };
        public static implicit operator ValueUnion(Uri PurpleUri) => new ValueUnion { PurpleUri = PurpleUri };
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                DatatypeEnumConverter.Singleton,
                PropertyConverter.Singleton,
                SnaktypeConverter.Singleton,
                RankConverter.Singleton,
                DatatypeConverter.Singleton,
                PurpleTypeConverter.Singleton,
                EntityTypeEnumConverter.Singleton,
                ValueUnionConverter.Singleton,
                SnaksOrderConverter.Singleton,
                FluffyTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class DatatypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DatatypeEnum) || t == typeof(DatatypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "quantity")
            {
                return DatatypeEnum.Quantity;
            }
            throw new Exception("Cannot unmarshal type DatatypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DatatypeEnum)untypedValue;
            if (value == DatatypeEnum.Quantity)
            {
                serializer.Serialize(writer, "quantity");
                return;
            }
            throw new Exception("Cannot marshal type DatatypeEnum");
        }

        public static readonly DatatypeEnumConverter Singleton = new DatatypeEnumConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class PropertyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Property) || t == typeof(Property?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "P1132":
                    return Property.P1132;
                case "P1350":
                    return Property.P1350;
                case "P1351":
                    return Property.P1351;
                case "P1352":
                    return Property.P1352;
                case "P1355":
                    return Property.P1355;
                case "P1356":
                    return Property.P1356;
                case "P1357":
                    return Property.P1357;
                case "P1359":
                    return Property.P1359;
            }
            throw new Exception("Cannot unmarshal type Property");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Property)untypedValue;
            switch (value)
            {
                case Property.P1132:
                    serializer.Serialize(writer, "P1132");
                    return;
                case Property.P1350:
                    serializer.Serialize(writer, "P1350");
                    return;
                case Property.P1351:
                    serializer.Serialize(writer, "P1351");
                    return;
                case Property.P1352:
                    serializer.Serialize(writer, "P1352");
                    return;
                case Property.P1355:
                    serializer.Serialize(writer, "P1355");
                    return;
                case Property.P1356:
                    serializer.Serialize(writer, "P1356");
                    return;
                case Property.P1357:
                    serializer.Serialize(writer, "P1357");
                    return;
                case Property.P1359:
                    serializer.Serialize(writer, "P1359");
                    return;
            }
            throw new Exception("Cannot marshal type Property");
        }

        public static readonly PropertyConverter Singleton = new PropertyConverter();
    }

    internal class SnaktypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Snaktype) || t == typeof(Snaktype?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "value")
            {
                return Snaktype.Value;
            }
            throw new Exception("Cannot unmarshal type Snaktype");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Snaktype)untypedValue;
            if (value == Snaktype.Value)
            {
                serializer.Serialize(writer, "value");
                return;
            }
            throw new Exception("Cannot marshal type Snaktype");
        }

        public static readonly SnaktypeConverter Singleton = new SnaktypeConverter();
    }

    internal class RankConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Rank) || t == typeof(Rank?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "normal")
            {
                return Rank.Normal;
            }
            throw new Exception("Cannot unmarshal type Rank");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Rank)untypedValue;
            if (value == Rank.Normal)
            {
                serializer.Serialize(writer, "normal");
                return;
            }
            throw new Exception("Cannot marshal type Rank");
        }

        public static readonly RankConverter Singleton = new RankConverter();
    }

    internal class DatatypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Datatype) || t == typeof(Datatype?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "wikibase-item")
            {
                return Datatype.WikibaseItem;
            }
            throw new Exception("Cannot unmarshal type Datatype");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Datatype)untypedValue;
            if (value == Datatype.WikibaseItem)
            {
                serializer.Serialize(writer, "wikibase-item");
                return;
            }
            throw new Exception("Cannot marshal type Datatype");
        }

        public static readonly DatatypeConverter Singleton = new DatatypeConverter();
    }

    internal class PurpleTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PurpleType) || t == typeof(PurpleType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "wikibase-entityid")
            {
                return PurpleType.WikibaseEntityid;
            }
            throw new Exception("Cannot unmarshal type PurpleType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurpleType)untypedValue;
            if (value == PurpleType.WikibaseEntityid)
            {
                serializer.Serialize(writer, "wikibase-entityid");
                return;
            }
            throw new Exception("Cannot marshal type PurpleType");
        }

        public static readonly PurpleTypeConverter Singleton = new PurpleTypeConverter();
    }

    internal class EntityTypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EntityTypeEnum) || t == typeof(EntityTypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "item")
            {
                return EntityTypeEnum.Item;
            }
            throw new Exception("Cannot unmarshal type EntityTypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EntityTypeEnum)untypedValue;
            if (value == EntityTypeEnum.Item)
            {
                serializer.Serialize(writer, "item");
                return;
            }
            throw new Exception("Cannot marshal type EntityTypeEnum");
        }

        public static readonly EntityTypeEnumConverter Singleton = new EntityTypeEnumConverter();
    }

    internal class ValueUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ValueUnion) || t == typeof(ValueUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new ValueUnion { Integer = l };
                    }
                    try
                    {
                        var uri = new Uri(stringValue);
                        return new ValueUnion { PurpleUri = uri };
                    }
                    catch (UriFormatException) { }
                    break;
            }
            throw new Exception("Cannot unmarshal type ValueUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ValueUnion)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            if (value.PurpleUri != null)
            {
                serializer.Serialize(writer, value.PurpleUri.ToString());
                return;
            }
            throw new Exception("Cannot marshal type ValueUnion");
        }

        public static readonly ValueUnionConverter Singleton = new ValueUnionConverter();
    }

    internal class SnaksOrderConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SnaksOrder) || t == typeof(SnaksOrder?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "P143":
                    return SnaksOrder.P143;
                case "P4656":
                    return SnaksOrder.P4656;
            }
            throw new Exception("Cannot unmarshal type SnaksOrder");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SnaksOrder)untypedValue;
            switch (value)
            {
                case SnaksOrder.P143:
                    serializer.Serialize(writer, "P143");
                    return;
                case SnaksOrder.P4656:
                    serializer.Serialize(writer, "P4656");
                    return;
            }
            throw new Exception("Cannot marshal type SnaksOrder");
        }

        public static readonly SnaksOrderConverter Singleton = new SnaksOrderConverter();
    }

    internal class FluffyTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FluffyType) || t == typeof(FluffyType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "statement")
            {
                return FluffyType.Statement;
            }
            throw new Exception("Cannot unmarshal type FluffyType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FluffyType)untypedValue;
            if (value == FluffyType.Statement)
            {
                serializer.Serialize(writer, "statement");
                return;
            }
            throw new Exception("Cannot marshal type FluffyType");
        }

        public static readonly FluffyTypeConverter Singleton = new FluffyTypeConverter();
    }
}