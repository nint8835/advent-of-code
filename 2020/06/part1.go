package main

import (
	"bufio"
	"fmt"
	"os"
)

func _CountAnswers(answers map[rune]bool) int {
	totalAnswers := 0
	for _, char := range "abcdefghijklmnopqrstuvwxyz" {
		_, found := answers[char]
		if found {
			totalAnswers++
		}
	}
	return totalAnswers
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	totalAnswers := 0
	answers := make(map[rune]bool)

	for scanner.Scan() {
		text := scanner.Text()
		if text == "" {
			totalAnswers += _CountAnswers(answers)
			answers = make(map[rune]bool)
		} else {
			for _, char := range text {
				answers[char] = true
			}
		}
	}
	totalAnswers += _CountAnswers(answers)

	fmt.Println(totalAnswers)
}
