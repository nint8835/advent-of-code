package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func runSlope(grid [][]string, xMod, yMod int) int {
	x, y, trees := 0, 0, 0

	for y < len(grid) {
		if grid[y][x] == "#" {
			trees++
		}
		x = (x + xMod) % len(grid[0])
		y += yMod
	}
	return trees
}

func main() {
	grid := make([][]string, 0)
	scanner := bufio.NewScanner(os.Stdin)

	for scanner.Scan() {
		text := scanner.Text()
		grid = append(grid, strings.Split(text, ""))
	}

	fmt.Println(
		runSlope(grid, 1, 1) *
			runSlope(grid, 3, 1) *
			runSlope(grid, 5, 1) *
			runSlope(grid, 7, 1) *
			runSlope(grid, 1, 2),
	)
}
