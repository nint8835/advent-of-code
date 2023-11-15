package main

import (
	"bufio"
	"fmt"
	"os"
	"reflect"
	"strings"
)

func _GetAdjacentVals(grid [][]string, rowIndex, columnIndex int) ([]string, map[string]int) {
	vals := make([]string, 0)
	valCounts := map[string]int{"L": 0, "#": 0, ".": 0}
	for adjacentRow := -1; adjacentRow < 2; adjacentRow++ {
		for adjacentColumn := -1; adjacentColumn < 2; adjacentColumn++ {
			newColumnIndex := columnIndex + adjacentColumn
			newRowIndex := rowIndex + adjacentRow
			if len(grid) <= newRowIndex || newRowIndex < 0 {
				continue
			}
			if len(grid[0]) <= newColumnIndex || newColumnIndex < 0 {
				continue
			}
			if adjacentColumn == 0 && adjacentRow == 0 {
				continue
			}
			vals = append(vals, grid[newRowIndex][newColumnIndex])
			valCounts[grid[newRowIndex][newColumnIndex]]++
		}
	}
	return vals, valCounts
}

func _Iterate(grid [][]string) [][]string {
	newGrid := make([][]string, 0)

	for rowIndex, row := range grid {
		newRow := make([]string, 0)
		for columnIndex, column := range row {
			_, adjacentCharCounts := _GetAdjacentVals(grid, rowIndex, columnIndex)
			var newChar string
			switch column {
			case "L":
				newChar = "#"
				if adjacentCharCounts["#"] != 0 {
					newChar = "L"
				}
			case "#":
				newChar = "#"
				if adjacentCharCounts["#"] >= 4 {
					newChar = "L"
				}
			case ".":
				newChar = "."
			}
			newRow = append(newRow, newChar)
		}
		newGrid = append(newGrid, newRow)
	}
	return newGrid
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	grid := make([][]string, 0)

	for scanner.Scan() {
		text := scanner.Text()
		grid = append(grid, strings.Split(text, ""))
	}

	previousGrid := grid
	newGrid := _Iterate(previousGrid)

	for !reflect.DeepEqual(previousGrid, newGrid) {
		previousGrid = newGrid
		newGrid = _Iterate(newGrid)
	}

	occupiedCount := 0

	for _, row := range newGrid {
		for _, column := range row {
			if column == "#" {
				occupiedCount++
			}
		}
	}
	fmt.Println(occupiedCount)
}
