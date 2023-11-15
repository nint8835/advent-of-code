package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func main() {
	expenses := make([]int, 0)

	scanner := bufio.NewScanner(os.Stdin)
	for scanner.Scan() {
		text := scanner.Text()
		val, _ := strconv.Atoi(text)
		expenses = append(expenses, val)
	}

	for index1, val1 := range expenses {
		subArray1 := expenses[index1:]
		for index2, val2 := range subArray1 {
			subArray2 := subArray1[index2:]
			for _, val3 := range subArray2 {
				if val1+val2+val3 == 2020 {
					fmt.Println(val1 * val2 * val3)
					return
				}
			}
		}
	}
}
