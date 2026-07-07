using System.Text.Json.Serialization;

namespace HarryPotter.Server.Models
{
    public class Spell  //defines the class for the spells mimicking the class 
    {
        [JsonPropertyName("ID")]
        public string? ID { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}


/*
[{ "id":"c76a2922-ba4c-4278-baab-44defb631236",
        "name":"Aberto",
        "description":"Opens locked doors"
},
{ "id":"06485500-d023-4799-93fd-77f2c3341aa3",
        "name":"Accio",
        "description":"Summons objects"
},
{ "id":"acbc0ae1-12e1-4813-b51e-09d22de40475",
        "name":"Aguamenti",
        "description":"Summons water"
},
{ "id":"c9d2f389-a419-4f7e-8d3d-254959638019",
        "name":"Alohomora",
        "description":"Unlocks objects"
},
{ "id":"018429a5-15d5-41af-bf8f-98a966733d77",
        "name":"Anapneo",
        "description":"Clears someone's airway"
}

*/