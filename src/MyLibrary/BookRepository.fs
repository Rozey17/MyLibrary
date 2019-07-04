module BookRepository

open MongoDB.Bson
open MongoDB.Driver
open RequestModel
open Domain

let getBooks (collection : IMongoCollection<Book>) =
    let books =
        collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
    books

let getBookById (collection : IMongoCollection<Book>, bookId : ObjectId) =
    let filter = Builders<Book>.Filter.Where(fun x -> x.Id = bookId)
    let book =
        collection.Find(filter).ToEnumerable() |> Seq.head
    book

let addBook (collection : IMongoCollection<Book>,
                           input : CreateBookRequest) =
    let id = ObjectId.GenerateNewId()

    let value =
        { Book.Id = id
          Title= input.Title
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