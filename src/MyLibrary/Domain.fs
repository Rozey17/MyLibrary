module Domain

open MongoDB.Bson

type Book = {
    Id : ObjectId
    Title: string
    AuthorId: ObjectId
    Genre: string
}

type Author = {
    Id: ObjectId
    Name:string   
}
