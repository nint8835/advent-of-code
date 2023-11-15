package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strconv"
	"strings"
)

func main() {
	parserRegexp := regexp.MustCompile(`^(\d+)-(\d+) (\w): (\w+)$`)
	scanner := bufio.NewScanner(os.Stdin)

	count := 0

	for scanner.Scan() {
		text := scanner.Text()
		matches := parserRegexp.FindStringSubmatch(text)

		minStr, maxStr, char, password := matches[1], matches[2], matches[3], matches[4]

		min, _ := strconv.Atoi(minStr)
		max, _ := strconv.Atoi(maxStr)

		if charCount := strings.Count(password, char); charCount >= min && charCount <= max {
			count++
		}
	}

	fmt.Println(count)
}
