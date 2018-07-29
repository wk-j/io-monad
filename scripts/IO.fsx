open System

type IO<'T> =
    private | Action of(unit -> 'T)

[<AutoOpen>]
module MonadIO =
    let private raw (Action f) = f
    let private run io = raw io ()
    let private eff g io = raw io () |> g
    let private bind io rest = Action(fun () -> io |> eff rest |> run)
    let private comb io1 io2 = Action(fun () -> run io1; run io2)

    type IOBuilder() =
        member b.Return x = Action(fun () -> x)
        member b.ReturnFrom x = x
        member b.Delay(g) = g()
        member b.Bind(io, rest) = bind io rest
        member b.Combine(io1, io2) = comb io1 io2

    let io = new IOBuilder()
    let (|Action|) io = run io

[<AutoOpen>]
module PreludeIO =
    let putChar (c:Char) = Action(fun () -> stdout.Write c)
    let putStr (s:string) = Action(fun () -> stdout.Write s)
    let putStrLn (s: string) = Action(fun () -> stdout.WriteLine s)
    let print x = Action(fun () -> printfn "%A" x)
    let getChar = Action(fun () -> stdin.Read() |> char |> string)
    let getLine = Action(fun () -> stdin.ReadLine())
    let getContents = Action(fun () -> stdin.ReadToEnd())

let lines(s: string) = s.Split([|stdout.NewLine|], StringSplitOptions.None) |> Seq.ofArray
let length xs = Seq.length xs

let (Action ()) = io {
    let! cs1 = getLine
    let! cs2 = getLine
    return! putStrLn cs1
    return! putStrLn cs2
}
