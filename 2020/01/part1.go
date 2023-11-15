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

	for index, val := range expenses {
		others := expenses[index:]
		for _, val2 := range others {
			if val+val2 == 2020 {
				fmt.Println(val * val2)
				return
			}
		}
	}
}
