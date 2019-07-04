module BookRouter

open BooksHandler
open Saturn
open Giraffe

let bookRouter =
    router {
        not_found_handler (text "resource not found")
        get "" getBooksHandler  
        getf "/%s" getBookByIdHandler
        post "" createBookHandler  
        putf "/%s" editBookHandler
        deletef "/%s" deleteBookHandler //ne pas oublier le "/" devant le %s
    }

