
using Google.Cloud.Firestore;

namespace Firebase;

public class FirestoreService : IFirestoreService
{
    private readonly FirestoreDb _firestoreDb;
    private const string _collectionName = "Shoes";

    public FirestoreService(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    public async Task<List<Shoe>> GetAll()
    {
        var collection = _firestoreDb.Collection(_collectionName);
        var snapshot = await collection.GetSnapshotAsync();

        var shoeDocuments = snapshot.Documents.Select(s => s.ConvertTo<ShoeDocument>()).ToList();

        return shoeDocuments.Select(ConvertDocumentToModel).ToList();
    }
    public async Task<Shoe> GetById(string Id)
    {
        var collection = _firestoreDb.Collection(_collectionName);
        var snapshot = await collection.GetSnapshotAsync();

        var shoeDocument = snapshot.Documents.FirstOrDefault(s=>s.Id==Id).ConvertTo<ShoeDocument>();
        
        return ConvertDocumentToModel(shoeDocument);
    }
    public async Task Update(Shoe shoe)
    {
        var collection = _firestoreDb.Collection(_collectionName);
        var snapshot = await collection.GetSnapshotAsync();
        var documentReference = collection.Document(shoe.Id);
       ShoeDocument newShoe= ConvertModelToDocument(shoe);
        await documentReference.SetAsync(newShoe);
    }


    public async Task Add(Shoe shoe)
    {
        var collection = _firestoreDb.Collection(_collectionName);
        var shoeDocument = ConvertModelToDocument(shoe);

        await collection.AddAsync(shoeDocument);
    }

    public async Task Delete(string id)
    {
        var collection = _firestoreDb.Collection(_collectionName);
        var snapshot = await collection.GetSnapshotAsync();
        var documentReference = collection.Document(id);
        documentReference.DeleteAsync();
    }

    private static Shoe ConvertDocumentToModel(ShoeDocument shoeDocument)
    {
        return new Shoe
        {
            Id = shoeDocument.Id,
            Name = shoeDocument.Name,
            Brand = shoeDocument.Brand,
            Price = decimal.Parse(shoeDocument.Price),
            VideoUri = new Uri(shoeDocument.VideoUri)
        };
    }

    private static ShoeDocument ConvertModelToDocument(Shoe shoe)
    {
        return new ShoeDocument
        {
            Id = shoe.Id,
            Name = shoe.Name,
            Brand = shoe.Brand,
            Price = shoe.Price.ToString(),
            VideoUri = shoe.VideoUri.ToString()
        };
    }
}