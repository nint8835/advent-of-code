package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func _ValidateNumber(number int, numbers []int) bool {
	if len(numbers) < 25 {
		return true
	}

	for _, recordedNum := range numbers[len(numbers)-25:] {
		requiredOtherNum := number - recordedNum
		if requiredOtherNum == recordedNum {
			continue
		}
		for _, otherRecordedNum := range numbers[len(numbers)-25:] {
			if otherRecordedNum == requiredOtherNum {
				return true
			}
		}
	}

	return false
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	numbers := make([]int, 0)

	for scanner.Scan() {
		text := scanner.Text()
		number, _ := strconv.Atoi(text)

		if !_ValidateNumber(number, numbers) {
			fmt.Println(number)
			return
		}
		numbers = append(numbers, number)
	}
}
