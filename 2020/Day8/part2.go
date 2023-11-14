package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

// Operation represents the identifier of a single operation.
type Operation = string

const (
	// OpAcc adds specified value to accumulator.
	OpAcc Operation = "acc"
	// OpJmp adds a specified value to the program counter.
	OpJmp Operation = "jmp"
	// OpNop runs nothing, continuing on to the next instruction.
	OpNop Operation = "nop"
)

// Instruction represents an individual program instruction.
type Instruction struct {
	Operation Operation
	Argument  int
	ExecCount int
}

// Console represents the processor for a handheld video game console.
type Console struct {
	Instructions []*Instruction
	Accumulator  int
	PC           int
}

// Reset resets the console to it's default state, ready to re-execute the stored program.
func (console *Console) Reset() {
	console.Accumulator = 0
	console.PC = 0
	for _, instruction := range console.Instructions {
		instruction.ExecCount = 0
	}
}

// Run evaluates the program stored on the console, exiting once it is about to loop.
func (console *Console) Run() bool {
	for console.Instructions[console.PC].ExecCount == 0 {
		instruction := console.Instructions[console.PC]
		switch instruction.Operation {
		case OpAcc:
			console.Accumulator += instruction.Argument
			console.PC++
		case OpJmp:
			console.PC += instruction.Argument
		case OpNop:
			console.PC++
		}
		instruction.ExecCount++
		if console.PC >= len(console.Instructions) {
			return true
		}
	}
	return false
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	instructions := make([]*Instruction, 0)

	for scanner.Scan() {
		line := scanner.Text()
		lineSegs := strings.Split(line, " ")
		argument, _ := strconv.Atoi(lineSegs[1])

		instructions = append(instructions, &Instruction{lineSegs[0], argument, 0})
	}

	console := &Console{Instructions: instructions}

	for _, instruction := range instructions {
		originalVal := instruction.Operation
		switch originalVal {
		case OpJmp:
			instruction.Operation = OpNop
		case OpNop:
			instruction.Operation = OpJmp
		}
		if halted := console.Run(); halted {
			fmt.Println(console.Accumulator)
			return
		}
		instruction.Operation = originalVal
		console.Reset()
	}
}
