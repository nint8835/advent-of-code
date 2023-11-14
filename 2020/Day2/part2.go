package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strconv"
)

func main() {
	parserRegexp := regexp.MustCompile(`^(\d+)-(\d+) (\w): (\w+)$`)
	scanner := bufio.NewScanner(os.Stdin)

	count := 0

	for scanner.Scan() {
		text := scanner.Text()
		matches := parserRegexp.FindStringSubmatch(text)

		pos1Str, pos2Str, char, password := matches[1], matches[2], matches[3], matches[4]

		pos1, _ := strconv.Atoi(pos1Str)
		pos2, _ := strconv.Atoi(pos2Str)

		pos1Matches := password[pos1-1:pos1] == char
		pos2Matches := password[pos2-1:pos2] == char

		if (pos1Matches || pos2Matches) && (pos1Matches != pos2Matches) {
			count++
		}
	}

	fmt.Println(count)
}
