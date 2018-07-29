
let (|ToUpper|) (x: string) = x.ToUpper()

let (|ToColor|) x =
    match x with
    | "red" -> "REDD"
    | _ -> "GREENN"

let (ToColor col) = "red"
printfn "%A" col
