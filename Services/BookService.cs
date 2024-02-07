using Library.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Library.Services;

public class BooksService
{
    private readonly IMongoCollection<Book> _booksCollection;
    private readonly IMongoCollection<RateBook> _rateBookCollection;

    public BooksService(
        IOptions<LibraryDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            bookStoreDatabaseSettings.Value.DatabaseName);

        _booksCollection = mongoDatabase.GetCollection<Book>(
            bookStoreDatabaseSettings.Value.BooksCollectionName);

        _rateBookCollection = mongoDatabase.GetCollection<RateBook>(
            bookStoreDatabaseSettings.Value.RateBookCollectionName);
    }

    public async Task<List<Book>> GetAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetAsync(string id) =>
        await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) =>
        await _booksCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Book updatedBook) =>
        await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task UpdateRatingAsync(string id, RateBook updatedRating) =>
        await _rateBookCollection.ReplaceOneAsync(x => x.Id == id, updatedRating);
    

    public async Task<List<RateBook>> GetRatingAsync() =>
           await _rateBookCollection.Find(_ => true).ToListAsync();

    public async Task<RateBook?> GetRatingAsync(string id) =>
        await _rateBookCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task RemoveAsync(string id) =>
        await _booksCollection.DeleteOneAsync(x => x.Id == id);
}
