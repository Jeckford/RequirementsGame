using System.Collections.Generic;
using System.Text.Json;

public class JsonBuilder {

    public Dictionary<string, object> Items { get; }

    public JsonBuilder() {

        Items = new Dictionary<string, object>();

    }

    public override string ToString() {

        return JsonSerializer.Serialize(Items);

    }

}

