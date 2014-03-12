namespace Fukujinzuke.ObjectModel

type Feature() =
    let mutable _title = ""
    let mutable _description = ""

    member this.Title
     with get() = _title
     and set(x) = _title <- x

    member this.Description
     with get() = _description
     and set(x) = _description <- x
