namespace calc

open WebSharper
open WebSharper.UI.Html
open WebSharper.UI.Client
open WebSharper.Forms 
open WebSharper.JavaScript
open WebSharper.UI
open WebSharper.Html
open WebSharper.Sitelets
open WebSharper.Html.Client


type Operation = Add | Subtract | Multiply | Divide


type Model = {
    Number: Var<float>
    Operation: Var<Operation>
}


module SomeFuncs2=
    let operationToString (op: Var<Operation>) = 
        match op.Value with
        | Add -> "Addition"
        | Subtract -> "Subtraction"
        | Multiply -> "Multiplication"
        | Divide -> "Division"


module SomeFuncs=
    let createForm () =
        let model = { Number = Var.Create 0.0f; Operation = Var.Create Add }
        Formlet.Do {
            let! number = 
                Formlet.Input ""
                |> Validator.IsFloat "Please enter a valid number."
                |> Formlet.Map float
                |> Formlet.Init model.Number.Value
    
            let! operation = 
                Formlet.Radio [Add; Subtract; Multiply; Divide]
                |> Formlet.Labels (operationToString model.Operation)
                |> Formlet.Init model.Operation.Value
    
            return
            fun submit -> 
                model.Number.Value <- number
                model.Operation.Value <- operation
                printfn "Number: %f, Operation: %A" model.Number.Value model.Operation.Value
        }
        |> Formlet.Run

module Main =
    open SomeFuncs

    let main argv =
        let content = 
            Div [
                Div [
                H1 [Text "Operation Form"]
                createForm ()
                ]
            ]
    

        content.Append "main"
        0
