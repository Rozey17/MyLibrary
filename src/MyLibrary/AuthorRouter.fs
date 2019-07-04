module AuthorRouter

open AuthorsHandler
open Saturn
open Giraffe

let authorRouter =
    router {
        not_found_handler (text "resource not found")
        get "" getAuthorsHandler  
        //getf "/%s" getBookByIdHandler
        post "" createAuthorHandler  
        //putf "/%s" editBookHandler
        //deletef "/%s" deleteBookHandler //ne pas oublier le "/" devant le %s
    }
