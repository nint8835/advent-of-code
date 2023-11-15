package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

type _BagEntry struct {
	Type  string
	Count int
}

func _GetCount(bagType string, contentsMap map[string][]_BagEntry) int {
	bagContents, _ := contentsMap[bagType]
	contentsCount := 0

	for _, content := range bagContents {
		contentsCount += content.Count + content.Count*_GetCount(content.Type, contentsMap)
	}
	return contentsCount
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	bagContents := make(map[string][]_BagEntry)

	for scanner.Scan() {
		text := scanner.Text()
		textSegments := strings.Split(text, " contain ")
		bagType := strings.TrimSuffix(textSegments[0], " bags")
		contents := strings.Split(strings.TrimSuffix(textSegments[1], "."), ", ")

		for _, rawSubtype := range contents {
			if rawSubtype == "no other bags." {
				continue
			}
			splitSubtype := strings.Split(rawSubtype, " ")
			contentsCountStr, subtype := splitSubtype[0], strings.Join(splitSubtype[1:len(splitSubtype)-1], " ")
			contentsCount, _ := strconv.Atoi(contentsCountStr)

			bagContents[bagType] = append(bagContents[bagType], _BagEntry{subtype, contentsCount})
		}
	}
	fmt.Println(_GetCount("shiny gold", bagContents))
}
