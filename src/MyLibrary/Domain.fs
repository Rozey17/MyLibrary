module Domain

open MongoDB.Bson

type Book = {
    Id : ObjectId
    Title: string
    Author: string
    Genre: string
}

type Author = {
    Id: ObjectId
    Name:string
    Books : Book list
}
