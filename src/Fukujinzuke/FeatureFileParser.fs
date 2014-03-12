// Learn more about F# at http://fsharp.net

module FeatureFileParser

open System
open FParsec
open Fukujinzuke.AST

// convenience type for locking down generic type inference issues
// from: http://www.quanttec.com/fparsec/tutorial.html#fs-value-restriction
type private UserState = unit

let private ws = spaces 
let private nb_ws = anyOf " \t"

let private ch c = pchar c
let private ch_ws c = ch c .>> ws
let private ws_ch_ws c = ws >>. ch c .>> ws

let private str s = pstring s
let private str_ws s = str s .>> ws
let private ws_str_ws s = ws >>. str s .>> ws

// make this compiler directive condition true to trace the parsers
#if DEBUG
let (<!>) (p: Parser<_,_>) label : Parser<_,_> =
    fun stream ->
        printfn "%A: Entering %s" stream.Position label
        let reply = p stream
        printfn "%A: Leaving %s (%A)" stream.Position label reply.Status
        reply
#else
let (<!>) (p: Parser<_,_>) label : Parser<_,_> =
    fun stream -> p stream
#endif

let private trim(s:String) = s.Trim()

let private pfeature_title = str "Feature:" >>. restOfLine(true) 
                             |>> trim 
                             |>> FeatureTitle <!> "feature title"

let private pfeature_description_line = nb_ws >>. restOfLine(true) 
let private pfeature_description = many pfeature_description_line 
                                   |>> List.fold (fun r s -> r + s.Trim() + "\n") ""
                                   |>> trim
                                   |>> FeatureDescription <!> "feature description"

let private pfeature_header = pfeature_title .>>. pfeature_description 
                              |>> FeatureHeader <!> "feature header"

let private pfeature_file = ws >>. pfeature_header .>> ws 
                            |>> FeatureFile <!> "feature file"

let private parser = pfeature_file .>> eof 

exception ParseError of string * ParserError

type ParseException (message:string, context:ParserError) =
    inherit ApplicationException(message, null)
    let Context = context

let private PrettyPrint a = sprintf "%A" a
let private ParseAST str =
    match run parser str with
    | Success(result, _, _)   -> result 
    | Failure(errorMsg, errorContext, _) -> raise (new ParseException(errorMsg, errorContext))

let Parse str = ParseAST str |> ASTtoObjectModelVisitor.Visit




