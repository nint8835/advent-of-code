package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
)

func _FindWeakness(number int, numbers []int) int {
	for index, firstNumber := range numbers {
		if firstNumber >= number {
			continue
		}
		total := firstNumber
		lowestNumber := firstNumber
		highestNumber := firstNumber

		for _, nextNumber := range numbers[index+1:] {
			if nextNumber < lowestNumber {
				lowestNumber = nextNumber
			}
			if nextNumber > highestNumber {
				highestNumber = nextNumber
			}
			total += nextNumber
			if total == number {
				return lowestNumber + highestNumber
			} else if total > number {
				break
			}
		}
	}
	return -1
}

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
			fmt.Println(_FindWeakness(number, numbers))
			return
		}
		numbers = append(numbers, number)
	}
}
