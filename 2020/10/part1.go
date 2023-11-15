package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strconv"
)

func _In(number int, numbers []int) bool {
	for _, val := range numbers {
		if val == number {
			return true
		}
	}
	return false
}

func _Max(numbers []int) int {
	var max = math.MinInt64

	for _, val := range numbers {
		if val > max {
			max = val
		}
	}
	return max
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	adapters := make([]int, 0)

	for scanner.Scan() {
		text := scanner.Text()
		adapterVal, _ := strconv.Atoi(text)
		adapters = append(adapters, adapterVal)
	}

	adapters = append(adapters, _Max(adapters)+3)

	currentVal := 0
	targetVal := _Max(adapters)
	differences := map[int]int{1: 0, 2: 0, 3: 0}

	for currentVal < targetVal {
		for diff := 1; diff <= 3; diff++ {
			if _In(currentVal+diff, adapters) {
				differences[diff]++
				currentVal += diff
				break
			}
		}
	}
	fmt.Println(differences[1] * differences[3])
}
