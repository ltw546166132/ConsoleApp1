module Test

open System
open System.Collections.Generic

//F#中的Measure
[<Measure>] type 元
[<Measure>] type 小孩
[<Measure>] type 大人

let kidPrice = 3<元/小孩>
let adultPrice = 5<元/大人>

let familyCost2 (child: int<小孩>) (adult: int<大人>) =
    let result = kidPrice * child + adultPrice * adult
    result