module Server

open Router
open Saturn
open Config

let app =

    initDb ()
    
    application {
        url "http://0.0.0.0:8085"
        use_router appRouter
    }

run app
