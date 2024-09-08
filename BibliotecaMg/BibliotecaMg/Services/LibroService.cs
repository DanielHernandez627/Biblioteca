using BibliotecaMg.Models;
using MongoDB.Driver;

namespace BibliotecaMg.Services
{
    public class LibroService
    {
        private readonly IMongoCollection<Libro> _librosCollection;

        public LibroService(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            var mongoDatabase = mongoClient.GetDatabase("biblioteca");
            _librosCollection = mongoDatabase.GetCollection<Libro>("libros");
        }

        public async Task<List<Libro>> GetAsync() =>
            await _librosCollection.Find(_ => true).ToListAsync();

        public async Task<Libro> GetAsync(string id) =>
            await _librosCollection.Find(libro => libro._ObjectId == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Libro newLibro) =>
            await _librosCollection.InsertOneAsync(newLibro);

        public async Task UpdateAsync(string id, Libro updatedLibro) =>
            await _librosCollection.ReplaceOneAsync(libro => libro._ObjectId == id, updatedLibro);

        public async Task RemoveAsync(string id) =>
            await _librosCollection.DeleteOneAsync(libro => libro._ObjectId == id);

        public async Task UpdateEstadoAsync(string id, bool nuevoEstado)
        {
            var update = Builders<Libro>.Update.Set(libro => libro.Estado, nuevoEstado);
            await _librosCollection.UpdateOneAsync(libro => libro._ObjectId == id, update);
        }
    }
}
