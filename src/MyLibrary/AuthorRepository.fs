module AuthorRepository

open MongoDB.Bson
open MongoDB.Driver
open RequestModel
open Domain

let getAuthors (collection : IMongoCollection<Author>) =
    let authors =
        collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
    authors

let getAuthorById (collection : IMongoCollection<Author>, authorId : ObjectId) =
    let filter = Builders<Author>.Filter.Where(fun x -> x.Id = authorId)
    let author =
        collection.Find(filter).ToEnumerable() |> Seq.head
    author

let addAuthor (collection : IMongoCollection<Author>,
                           input : CreateAuthorRequest) =
    let id = ObjectId.GenerateNewId()

    let value =
        { Author.Id = id
          Name = input.Name
        }
    value |> collection.InsertOne

let deleteAuthor (collection : IMongoCollection<Author>,
                           bookId : ObjectId) =
    collection.DeleteOne(fun x -> x.Id = bookId)

let editAuthor (collection : IMongoCollection<Author>,
                         input : UpdateAuthorRequest) =
    let filter =
        Builders<Author>
            .Filter.Eq((fun x -> x.Id), input.Id |> ObjectId.Parse)
    let update =
        Builders<Author>
            .Update.Set((fun x -> x.Name), input.Name)
            
    collection.UpdateOne(filter, update) |> ignore   