namespace SimpleBookForm

open WebSharper
open WebSharper.UI
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.UI.Notation
open WebSharper.Forms
open WebSharper.JavaScript
[<JavaScript>]
module Client =

    let bookForm =
        let title = Var.Create ""
        let author = Var.Create ""
        let year = Var.Create ""
        
        Form.Return (fun title author year -> title, author, year)
        <*> (Form.YieldVar title
            |> Validation.IsNotEmpty "Please enter a book title")
        <*> (Form.YieldVar author
            |> Validation.IsNotEmpty "Please enter an author's name")
        <*> (Form.YieldVar year
            |> Validation.IsNotEmpty "Please enter the year of publication")
        |> Form.WithSubmit
        |> Form.Run (fun (t, a, y) ->
            title := ""; author := ""; year := ""; // Reset the form
            JS.Alert(sprintf "Book: %s\nAuthor: %s\nYear: %s" t a y)
        )
        |> Form.Render (fun title author year submit ->
            div [] [
                div [] [label [] [text "Book Title: "]; Doc.Input [] title]
                div [] [label [] [text "Author: "]; Doc.Input [] author]
                div [] [label [] [text "Year: "]; Doc.Input [] year]
                Doc.Button "Submit" [] submit.Trigger
                div [] [
                    Doc.ShowErrors submit.View (fun errors ->
                        errors
                        |> Seq.map (fun m -> p [] [text m.Text])
                        |> Seq.cast
                        |> Doc.Concat)
                ]
            ]
        )
        |> fun s -> s.RunById "main"
