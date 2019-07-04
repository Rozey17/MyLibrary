module Config

open MongoDB.Driver
open Domain

let mongoDbConnectString = "mongodb://localhost:27017"
let mongo = mongoDbConnectString |> MongoClient
let db = "MyLibrary" |> mongo.GetDatabase
let bookCollection =
    "Books" |> db.GetCollection<Book>
let authorCollection = 
    "Authors" |> db.GetCollection<Author>
    
let initDb () =
    Builders<Book>.IndexKeys.Text(fun x -> x.Title :> obj)
    |> bookCollection.Indexes.CreateOne |> ignore

    Builders<Author>.IndexKeys.Text(fun x -> x.Name :> obj)
    |> authorCollection.Indexes.CreateOne |> ignore