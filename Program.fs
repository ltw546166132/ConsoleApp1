open System
open System.Collections.Generic
open System.Net
open Microsoft.FSharp.Control.WebExtensions

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

printf $"{System.DateTime.UtcNow: ``yyyy-MM-dd HH-mm-ss``}"
let a  = true

let d = 
    let a = 1 
    let b = 2 
    900

//元组
let user = 
    let name = "ltw"
    let age = 20
    let height = 178
    let salary = 999.99
    (name, age, salary, height)


let (a1, b1, c1, d1) = user

//记录
type Gender = 
    {
        gender:int
    }


type TestUser = 
    {
        name:string
        age:int
        salary:float
    }

type User = 
    {
        name:string
        age:int
        salary:float
        gender:Gender
    }


let user1 = 
    {
        age = 22
        name = "AA"
        salary = 32.32
    }

let user2 = 
    {
        age = 22
        name = "AA"
        salary = 32.32
        gender = {
            gender = 1
        }
    }

let name = user2.name
let pergender = user2.gender.gender

//匿名记录
let user4 =     
    {|
        name = "123"
        salary = 32.32
    |}


//修改记录的值
let user3 = 
    {
        user2 with name = "qees"; age = 45; gender = {gender = 0}
    }

let user5 = 
    {|
        user4 with name = "haha"
    |}

let nameOfUser5 = user5.name

//联合   类似java枚举
type testEnum = 
    |男
    |女

type PlayerRecord = 
    {
        name:string
        level:int
        strenth:int
        Exp:decimal
    }

type MonsterRecord = 
    {
        name:string
        level:int
        strenth:int
    }

type NPCRecord = 
    {
        name:string
        level:int
        strenth:int
    }

//区别联合
type Person = 
    | PlayerCase of PlayerRecord
    | MonsterCase of MonsterRecord
    | NPCCase of NPCRecord
    | Animal of int64

let npc1 = 
    {
        name = "qqq"
        level = 99
        strenth = 100
    }

let p1 = Person.NPCCase npc1

let mutable muTest = "aaa"
muTest <- "bbb"


//F#的类
type Class1(name:string, age:int, salary:float)=
    static let aa = 1
    let random = new System.Random()
    member val random1 = random.Next() with get, set
    member val name = name with get, set
    member val age = age with get, set
    member val salary = salary with get, set
    //类内部函数定义
    member this.f(x: int) : int =
        random.Next() * x * this.random1
    //类静态函数
    static member f2(x: int) : int = 
        aa * x


let class1 = Class1("qqq", 12, 8999.0)


//继承Class1
type Class2(name:string, age:int, salary:float, gender:Gender)=
    inherit Class1(name, age, salary)
    let g = gender
    do
        printfn "Class2 init"
    member val gender = gender with set, get
    member this.gender2 = g

let class2 = Class2("class2", 23, 67.88, gender = {gender = 1})
let class3 = class2 :> Class1
class3.name <- "class3"

//Interface
type IPrintable =
    abstract member Print : unit

type SomeClass1(x: int, y: float)=
    interface IPrintable with
        member  this.Print = printfn "%d %f" x y

type Interface1 =
    abstract member Method1 : int

type Interface2 =
    inherit Interface1
    abstract member Method2 : int

//实现接口
type MyClass2(n : int) = 
    interface Interface2 with
        member this.Method1 = n * 2
        member this.Method2 = n / 3

let main argev = 
    let x1 = new SomeClass1(1, 1.3)
    (x1 :> IPrintable).Print
    0

//F#集合
let list1 = [1; 2; 3]
let list2 = [
    1
    2
    3
]
let list3 = [1..10]
let list4 = [1..2..10]
let emptyList = []
let listAdd = list1 @ list2
let list5 = 100::list1

//array
let array1 = [|1; 2; 3; 4|]
array1[0] <- 10

let zeroToTenArray : int array = Array.zeroCreate 10
let array2 = array1.[0..2]
array1.[0..]

//二维矩阵
let matrix = Array2D.zeroCreate<int> 3 3

//1下标到最后的行  和取所有列
matrix.[1.., *]
matrix.[1..2, *]

//取所有行  列取下标1到2
matrix.[*, 1..2]

//序列
seq{0..10..100}

let seqFormArray = [|1..10|] :> seq<int>

//Dictionary  线程不安全
let data = [(1, 10); (2, 5); (3, 6); (4, 7); (5, 8)]

let myDict = Dictionary<int, string>()
myDict.Add(1, "11")
myDict.Add(2, "22")
let aaa = myDict.ContainsKey(1)
//get
let getValue1 = myDict.[2]
let getValue2 = myDict.Item(2)

//map  线程安全
let myMap = data |> Map
let myMap2 = myMap.Remove(1)
let getMapValue = myMap2.[3]
let getMapValue2 = myMap2.Item(3)

//HashSet  线程不安全
let myHashSet = HashSet<int>([1;2;1;2;3])

let addHashSet = myHashSet.Add(5)

//Set 线程安全
let mySet = Set<int>([1;2;3;3])
let addSet = mySet.Add(5)

//方法
let f(x: int) = 
    x + 1

//定义参数类型与返回值类型
let cylinderVolume (radius: float) (hight: float) : float = 
    let pi = 3.141592654
    hight * pi * radius * radius

//使用方法
let vol = cylinderVolume 10.0 12.9

let fun5(x: int) =
    (x, "int", 78.7)

//F# 方法以递归方式执行
let sumFunction a b c = a + b + c + 1
let sumF1 = sumFunction 1
let sumF2 = sumF1 2
let sumF3 = sumF2 3

let sumFun1 (a: int) (b: int) (c: int) : int = 
    a + b + c + 1
let sumFun2 = sumFun1 1
let sumFun3 = sumFun2 2
let sumFun4 = sumFun3 3

//Function Values
let apply1 (transform: int -> int) y = transform y
let result = apply1 sumF2 4

let apply2 (f: int -> int -> int) x y = f x y


let mul (x: int) (y: int) : int = 
    x * y

let mul2 x y  = 
    x * y

let result2 = apply2 mul2 5 5

//Lamba表达式
let result3 = apply2 (fun x y -> x * y * 10) 10 10
let result4 = apply1(fun x -> x * 10) 10


//Function Composition and Pipelining  函数拼接
let function1 (x: int) : int =
    x + 1
let function2 (x: int) : int =
    x + 1
let pipe1 = function1 >> function2
let resultPipel = pipe1 1
let pipe2 = function2(function1(1))

let pipe3 = 100 |> function1 |>function2

//记录的成员函数
type TestRecord = 
    {
        A: int
        B: int
    }
    member this.TestFunction(x: int) : int = 
        x + this.A + this.B
    static member TestStaticFun(x: int) : int = 
        x * 1000
let fun12(x: int): int = 
    x * 1
let fun13(x: int): int = 
    x * 10
let r = 10
if r % 2 = 0
then
    fun12(r)
else
    fun13(r)

//Option
let div x y =
    if y = 0
    then None
    else 
        let r = x / y
        Some r

let div1 = div 10 0
let optionr1 =
    if div1.IsSome
    then div1.Value
    else 1

//F#高阶函数
//定义抽象函数体的执行顺序
let function3 (g: int -> int) (x: int) : int =
    x |> g
//参数1为lamda表达式
function3( fun x -> x + 11) 5


//map映射List
let listData = [1..100]
//List.map   List.mapi
let map1 = List.map (fun x -> x + 1), listData
//List.iter  List.iteri
List.iter (fun x -> printf "%A!" x) listData
List.iteri (fun index x -> printf "%A->%A!" index x) listData

let ran = System.Random()
let getRandomNumber (min: int)  (max: int) : int =
    ran.Next(min, max + 1)

//F# filter
[0..99] |>
List.map (fun x -> getRandomNumber 50 100) |>
List.filter(fun x -> x>=60)

//F# choose  相当于 map与filter的结合
let valueRan = [0..99]
List.choose (fun x -> 
    let num, score = x, getRandomNumber 50 100
    if score < 60
    then Some(num, score)
    else None) valueRan

//F# group
let result5 = 
    [1..10] |>
    List.map(fun num -> num, getRandomNumber 1 10) |>
    List.groupBy(fun (num,sorce) -> sorce) |>
    List.map(fun (sorce, group) -> 
        let team = List.map(fun (num, sorce) -> num) group
        sorce, team
    )

List.sortBy(fun (score, team) -> score) result5

[1..100] |>
List.map(fun x -> getRandomNumber 1 10) |>
List.distinct

let datafor = [1..100] 
datafor |>
List.map(fun i -> 
    if i % 2 = 0
    then "even"
    else "odd"
    )

//for循环
for i in datafor do
   printfn "%d\t" i

for i = 0 to 99 do
    printf "%d\t %s\t"  datafor.[i]  "hello world"

let mutable counter = 0
while counter < datafor.Length do
    printf "%d/t" datafor.[counter]
    counter <- counter + 1

//match
let matchingFun (x: int) =
    match x % 3 with
    | 0 -> -1
    | 1 -> 0
    | 2 -> 1
    | _ -> x

for i in datafor do
    printf $"%d{matchingFun datafor.[i-1]}\t"

//match with
let matchWith(x: int) =
    match x with
    | 1 | 2 | 3 -> printf "Found 1,2,3\t"
    | v -> printf $"%d{v}\t"

for i in datafor do
    matchWith datafor.[i-1]

let printOption(data) : bool =
    match data with
    | Some var1 -> true
    | None -> false

let function5 var1 var2 =
    match (var1, var2) with
    | (var1, var2) when var1 > var2 -> printf "%d is greater than %d" var1 var2
    | (var1, var2) when var1 < var2 -> printf "%d is less than %d" var1 var2
    | (var1, var2) -> printf "%d equals %d" var1 var2

function5 1 2
function5 2 1

let list6 = [1;2;3;4;5]
let rec printList l =
    match l with
    | head :: tail -> printf "%d\t" head; printList tail
    | [] -> ()
printList list6

let listLength list =
    match list with
    | [] -> 0
    | [_] -> 1
    | [_;_] -> 2
    | [_;_;_] -> 3
    | _ -> List.length list

printfn "%d" (listLength [1])
printfn "%d" (listLength [1; 1])
printfn "%d" (listLength [1; 1; 1])

type A() = class end
type B() = inherit A()
type C() = inherit A()

let m (a: A) =
    match a with
    | :? B -> printfn "It's a B"
    | :? C -> printfn "It's a C"
    | _ -> ()

let (|Even|Odd|) input1 = 
    if (input1 % 2) = 0 
    then Even 
    else Odd
   
let TestNumber input =
    match input with
    | Even -> printfn "%d is even" input
    | Odd -> printfn "%d is odd" input

TestNumber 7
TestNumber 11
TestNumber 12

//Partial Active Patterns
let (|Integer1|_|) (str:string) =
    let mutable intvalue = 0
    if System.Int32.TryParse(str, &intvalue) then Some(intvalue)
    else None

let (|Float1|_|) (str:string) =
    let mutable floatvalue = 0.0
    if System.Double.TryParse(str, &floatvalue) then Some(floatvalue)
    else None

let parseNumeric str =
    match str with
    | Integer1 i -> printfn "%d : Integer" i
    | Float1 f -> printfn "%f : Floating point" f
    | _ -> printfn "%s : Not matched." str

parseNumeric "1.1"
parseNumeric "2"

//F#并发
[|1..100|] |> Array.Parallel.map(fun i -> i * i)


let urlList = [("Microsoft", "http://www.microsoft.com"); ("MSDN", "http://msdn.microsoft.com"); ("Bing","http://www.bing.com")]

let fetchAsync(name, url: string) =
    async {
        try
            let uri = new System.Uri(url)
            let webClient  = new WebClient()
            let! html = webClient.AsyncDownloadString(uri)
            printfn "Read %d characters for %s" html.Length name
        with 
            | ex -> printfn "%s" (ex.Message);
    }

let allRun() =
    urlList |>
    List.toArray |>
    Array.Parallel.map fetchAsync |> Async.Parallel |> Async.RunSynchronously |> ignore

allRun()


