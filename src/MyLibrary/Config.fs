module Config

open MongoDB.Driver
open Domain

let mongoDbConnectString = "mongodb://localhost:27017"
let mongo = mongoDbConnectString |> MongoClient
let db = "MyLibrary" |> mongo.GetDatabase
let bookCollection =
    "Books" |> db.GetCollection<Book>

    
let initDb () =
    Builders<Book>.IndexKeys.Text(fun x -> x.Title :> obj)
    |> bookCollection.Indexes.CreateOne |> ignore