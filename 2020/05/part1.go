package main

import (
	"bufio"
	"fmt"
	"os"
)

type _BSPParser struct {
	Value int
}

func (parser *_BSPParser) Lower() {
	parser.Value <<= 1
}

func (parser *_BSPParser) Upper() {
	parser.Value <<= 1
	parser.Value++
}

type _BoardingPass struct {
	row    *_BSPParser
	column *_BSPParser
}

func (pass _BoardingPass) Row() int {
	return pass.row.Value
}

func (pass _BoardingPass) Column() int {
	return pass.column.Value
}

func (pass _BoardingPass) SeatID() int {
	return pass.Row()*8 + pass.Column()
}

func _NewBoardingPass(position string) *_BoardingPass {
	pass := &_BoardingPass{&_BSPParser{0}, &_BSPParser{0}}

	for _, char := range position {
		switch char {
		case 'F':
			pass.row.Lower()
		case 'B':
			pass.row.Upper()
		case 'L':
			pass.column.Lower()
		case 'R':
			pass.column.Upper()
		}
	}

	return pass
}

func main() {
	maxID := 0

	scanner := bufio.NewScanner(os.Stdin)

	for scanner.Scan() {
		text := scanner.Text()
		pass := _NewBoardingPass(text)
		passID := pass.SeatID()
		if passID > maxID {
			maxID = passID
		}
	}

	fmt.Println(maxID)
}
