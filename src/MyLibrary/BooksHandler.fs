module BooksHandler

open BookRepository
open FSharp.Control.Tasks.V2.ContextInsensitive
open Microsoft.AspNetCore.Http
open Giraffe
open Config
open RequestModel
open MongoDB.Bson

let getBooksHandler : HttpHandler =
    fun (next:HttpFunc) (ctx:HttpContext) ->
     task {
           let books = bookCollection |> getBooks
           return! Successful.OK books next ctx
          }

let createBookHandler : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! input = ctx.BindJsonAsync<CreateBookRequest>()
            match input.HasErrors with
            | Some msg -> return! (setStatusCode 400 >=> json msg) next ctx
            | None -> 
                (bookCollection, input)
                |> addBook 
                return! Successful.OK (input) next ctx }

let getBookByIdHandler (bookId: string)= 
    fun (next:HttpFunc) (ctx:HttpContext) ->
     task{
        return! match bookId |> ObjectId.TryParse with
                | false,_ ->
                    let message = "Incorrect ID"
                    RequestErrors.BAD_REQUEST message next ctx
                |true, bookId -> 
                    let book = (bookCollection, bookId) |> getBookById
                    Successful.OK (book) next ctx
     }


let deleteBookHandler (bookId: string)= 
    fun (next:HttpFunc) (ctx:HttpContext) ->
     task {
        return! match bookId |> ObjectId.TryParse with
                | false,_ ->
                    let message = "Incorrect ID"
                    RequestErrors.BAD_REQUEST message next ctx
                |true, bookId -> 
                    let book = (bookCollection, bookId) |> deleteBook
                    Successful.OK (book) next ctx
     }
        

let editBookHandler (bookId : string) : HttpHandler =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            return! match bookId |> ObjectId.TryParse with
                    | false, _ ->
                        let message = "L'ID est nul"
                        RequestErrors.BAD_REQUEST message next ctx
                    | true, bookId ->
                        task {
                            let! input = ctx.BindJsonAsync<UpdateBookRequest>
                                             ()
                            let message = "Book with id =" + bookId.ToString() + " was updated" //convertir bookId en string
                            (bookCollection, input)
                            |> editBook
                            return! Successful.OK (message) next ctx
                        }
        }

AuthiorsHan