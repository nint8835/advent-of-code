package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

var _LowerLetters = "abcdefghijklmnopqrstuvwxyz"

func _CountAnswers(answers map[rune]bool) int {
	totalAnswers := 0
	for _, char := range _LowerLetters {
		val, _ := answers[char]
		if val {
			totalAnswers++
		}
	}
	return totalAnswers
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	totalAnswers := 0
	answers := make(map[rune]bool)
	for _, refChar := range _LowerLetters {
		answers[refChar] = true
	}

	for scanner.Scan() {
		text := scanner.Text()
		if text == "" {
			totalAnswers += _CountAnswers(answers)
			for _, refChar := range _LowerLetters {
				answers[refChar] = true
			}
		} else {
			for _, refChar := range _LowerLetters {
				if !strings.Contains(text, string(refChar)) {
					answers[refChar] = false
				}
			}
		}
	}
	totalAnswers += _CountAnswers(answers)

	fmt.Println(totalAnswers)
}
