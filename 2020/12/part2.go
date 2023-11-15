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

	waypointX, waypointY := 10, 1
	x, y := 0, 0

	for scanner.Scan() {
		command := scanner.Text()
		operation := command[0]
		amount, _ := strconv.Atoi(command[1:])

		switch operation {
		case 'N':
			waypointY += amount
		case 'S':
			waypointY -= amount
		case 'E':
			waypointX += amount
		case 'W':
			waypointX -= amount
		case 'R':
			for rotateCount := 0; rotateCount < amount/90; rotateCount++ {
				waypointX, waypointY = waypointY, waypointX*-1
			}
		case 'L':
			for rotateCount := 0; rotateCount < amount/90; rotateCount++ {
				waypointX, waypointY = waypointY*-1, waypointX
			}
		case 'F':
			x += waypointX * amount
			y += waypointY * amount
		}
	}
	fmt.Println(math.Abs(float64(x)) + math.Abs(float64(y)))
}
