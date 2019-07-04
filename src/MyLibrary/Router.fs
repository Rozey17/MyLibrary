module Router

open BookRouter
open Saturn
open Giraffe

let appRouter =
    router {
        not_found_handler (text "resource not found")
        get "/" (text "welcome to BookStore API")
        forward "/api/books" bookRouter 
        forward "/api/authors" authorRouter
    }