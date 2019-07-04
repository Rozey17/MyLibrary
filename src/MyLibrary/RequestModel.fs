module RequestModel

type CreateBookRequest =
   {
        Title: string
        Author: string
        Genre: string
    }
    member this.HasErrors =
        if this.Title = null || this.Title = "" then Some "Title is required"
        else if this.Author.Length > 255  then Some"Author is too long"
        else if this.Genre.Length > 15 then Some "Genre is too long"
        else None

type UpdateBookRequest = 
 { 
    Id : string
    Title : string
    Author: string
    Genre : string
 }
    member this.HasErrors =
        if this.Title = null || this.Title = "" then Some "Title is required"
        else if this.Author.Length > 255  then Some"Author is too long"
        else if this.Genre.Length > 15 then Some "Genre is too long"
        else None

    
type CreateAuthorRequest =
   {
        Name: string             
   }
    member this.HasErrors =
        if this.Name = null || this.Name = "" then Some "Name is required"
        else if this.Name.Length > 255  then Some "Author is too long"        
        else None

type UpdateAuthorRequest = 
 { 
    Id : string
    Name: string 
 }
    member this.HasErrors =
        if this.Name = null || this.Name = "" then Some "Name is required"
        else if this.Name.Length > 255  then Some"Author is too long"
        else None
