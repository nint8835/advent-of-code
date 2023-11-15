package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strconv"
)

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	dirs := [][]int{{1, 0}, {0, -1}, {-1, 0}, {0, 1}}
	x, y := 0, 0
	facingDir := 0

	for scanner.Scan() {
		command := scanner.Text()
		operation := command[0]
		amount, _ := strconv.Atoi(command[1:])

		switch operation {
		case 'N':
			y += amount
		case 'S':
			y -= amount
		case 'E':
			x += amount
		case 'W':
			x -= amount
		case 'R':
			facingDir = (4 + facingDir + (amount / 90)) % len(dirs)
		case 'L':
			facingDir = (4 + facingDir - (amount / 90)) % len(dirs)
		case 'F':
			x += dirs[facingDir][0] * amount
			y += dirs[facingDir][1] * amount
		}
	}
	fmt.Println(math.Abs(float64(x)) + math.Abs(float64(y)))
}
