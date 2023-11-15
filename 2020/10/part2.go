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

var _Answers = make(map[int]int)

func _Traverse(numbers []int, start int, target int) int {
	if start == target {
		return 1
	}

	savedAns, found := _Answers[start]
	if found {
		return savedAns
	}

	result := 0

	for diff := 1; diff <= 3; diff++ {
		if _In(start+diff, numbers) {
			result += _Traverse(numbers, start+diff, target)
		}
	}
	_Answers[start] = result
	return result
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

	fmt.Println(_Traverse(adapters, 0, _Max(adapters)))
}
