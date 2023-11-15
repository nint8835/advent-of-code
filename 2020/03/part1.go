package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func main() {
	grid := make([][]string, 0)
	scanner := bufio.NewScanner(os.Stdin)

	for scanner.Scan() {
		text := scanner.Text()
		grid = append(grid, strings.Split(text, ""))
	}

	trees := 0

	x, y := 0, 0

	for y < len(grid) {
		if grid[y][x] == "#" {
			trees++
		}
		x = (x + 3) % len(grid[0])
		y++
	}

	fmt.Println(trees)
}
