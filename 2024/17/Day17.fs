// aoc-tools:stars 1
module AdventOfCode.Solutions.Y2024.D17

open System.IO
open AdventOfCode.Solutions.Utils

// TODO: Address warning
#nowarn "FS0025"

type ProgramState =
    { A: int
      B: int
      C: int

      PC: int

      Out: int[] }

type Operand =
    | Combo of int
    | Literal of int

type Opcode =
    | Adv of Operand
    | Bxl of Operand
    | Bst of Operand
    | Jnz of Operand
    | Bxc of Operand
    | Out of Operand
    | Bdv of Operand
    | Cdv of Operand

let parseInstruction ([| opcode; operand |]: int array) : Opcode =
    match opcode with
    | 0 -> Adv(Combo operand)
    | 1 -> Bxl(Literal operand)
    | 2 -> Bst(Combo operand)
    | 3 -> Jnz(Literal operand)
    | 4 -> Bxc(Literal operand)
    | 5 -> Out(Combo operand)
    | 6 -> Bdv(Combo operand)
    | 7 -> Cdv(Combo operand)
    | _ -> failwith "Invalid opcode"

let getOperandValue (state: ProgramState) (operand: Operand) : int =
    match operand with
    | Literal value -> value
    | Combo value ->
        match value with
        | 0
        | 1
        | 2
        | 3 -> value
        | 4 -> state.A
        | 5 -> state.B
        | 6 -> state.C
        | _ -> failwith "Invalid operand"

let executeInstruction
    (state: ProgramState)
    (instruction: Opcode)
    : ProgramState =
    match instruction with
    | Adv operand ->
        { state with
            A = (state.A / (pown 2 (getOperandValue state operand)))
            PC = state.PC + 1 }
    | Bxl operand ->
        { state with
            B = (state.B ^^^ (getOperandValue state operand))
            PC = state.PC + 1 }
    | Bst operand ->
        { state with
            B = ((getOperandValue state operand) % 8)
            PC = state.PC + 1 }
    | Jnz operand ->
        { state with
            PC =
                (if state.A = 0 then
                     state.PC + 1
                 else
                     ((getOperandValue state operand) % 2)) }
    | Bxc _ ->
        { state with
            B = state.B ^^^ state.C
            PC = state.PC + 1 }
    | Out operand ->
        { state with
            PC = state.PC + 1
            Out =
                Array.append state.Out [| (getOperandValue state operand) % 8 |] }
    | Bdv operand ->
        { state with
            B = (state.A / (pown 2 (getOperandValue state operand)))
            PC = state.PC + 1 }
    | Cdv operand ->
        { state with
            C = (state.A / (pown 2 (getOperandValue state operand)))
            PC = state.PC + 1 }

let rec executeProgram (state: ProgramState) (instructions: Opcode[]) =
    if state.PC >= instructions.Length then
        state
    else
        let newState = executeInstruction state instructions.[state.PC]
        executeProgram newState instructions

let registerText, programText =
    File.ReadAllText "input.txt" |> String.split "\n\n" |> Array.toTuple2

let initialState =
    registerText
    |> String.split "\n"
    |> Array.map (String.split ": " >> (fun a -> a[1]) >> int)
    |> (fun [| a; b; c |] ->
        { A = a
          B = b
          C = c
          PC = 0
          Out = [||] })

let program =
    programText
    |> String.split ": "
    |> (fun a -> a[1])
    |> String.split ","
    |> Array.map int
    |> Array.chunkBySize 2
    |> Array.map parseInstruction

let solve () : unit =
    executeProgram initialState program
    |> _.Out
    |> Array.map string
    |> String.concat ","
    |> printfn "%s"
