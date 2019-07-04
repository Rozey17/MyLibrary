module AuthorRepository

open MongoDB.Bson
open MongoDB.Driver
open RequestModel
open Domain

let getAuthors (collection : IMongoCollection<Author>) =
    let authors =
        collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
    authors

let getBookById (collection : IMongoCollection<Author>, authorId : ObjectId) =
    let filter = Builders<Author>.Filter.Where(fun x -> x.Id = authorId)
    let author =
        collection.Find(filter).ToEnumerable() |> Seq.head
    author

let addAuthor (collection : IMongoCollection<Author>,
                           input : CreateBookRequest) =
    let id = ObjectId.GenerateNewId()

    let value =
        { Author.Id = id
          Name = input.
          Author = input.Author 
          Genre = input.Genre}
    value |> collection.InsertOne

let deleteBook (collection : IMongoCollection<Book>,
                           bookId : ObjectId) =
    collection.DeleteOne(fun x -> x.Id = bookId)

let editBook (collection : IMongoCollection<Book>,
                         input : UpdateBookRequest) =
    let filter =
        Builders<Book>
            .Filter.Eq((fun x -> x.Id), input.Id |> ObjectId.Parse)
    let update =
        Builders<Book>
            .Update.Set((fun x -> x.Title), input.Title)
            .Set((fun x -> x.Author), input.Author)
            .Set((fun x -> x.Genre), input.Genre)
    collection.UpdateOne(filter, update) |> ignore   