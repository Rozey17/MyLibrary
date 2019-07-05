module AuthorsHandler

open Microsoft.AspNetCore.Http
open Giraffe
open AuthorRepository
open RequestModel
open Config

let getAuthorsHandler (next:HttpFunc) (ctx:HttpContext) =
    task {
        let authors = authorCollection |> getAuthors 
        return! Successful.OK authors next ctx
    }

let createAuthorHandler (next:HttpFunc) (ctx:HttpContext) =
    task {
        let! input = ctx.BindJsonAsync<CreateAuthorRequest>()
        let message = "Author " + input.Name + " created successfully."
        (authorCollection, input) |> addAuthor
        return! Successful.OK ( message ) next ctx
    }