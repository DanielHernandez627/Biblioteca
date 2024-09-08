using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BibliotecaMg.Models
{
    public class Libro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _ObjectId { get; set; }

        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("genero")]
        public string Genero { get; set; }

        [BsonElement("estado")]
        public bool Estado { get; set; }
    }
}
