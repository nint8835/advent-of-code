package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func _GetPermutations(bagType string, possibilityMap map[string][]string, resultsMap map[string]bool) {
	possibleParents, _ := possibilityMap[bagType]
	for _, parent := range possibleParents {
		resultsMap[parent] = true
		_GetPermutations(parent, possibilityMap, resultsMap)
	}
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	possibleContainers := make(map[string][]string)

	for scanner.Scan() {
		text := scanner.Text()
		textSegments := strings.Split(text, " contain ")
		bagType := strings.TrimSuffix(textSegments[0], " bags")
		contents := strings.Split(strings.TrimSuffix(textSegments[1], "."), ", ")

		for _, rawSubtype := range contents {
			splitSubtype := strings.Split(rawSubtype, " ")
			subtype := strings.Join(splitSubtype[1:len(splitSubtype)-1], " ")
			if subtype == "no other" {
				continue
			}

			possibleContainers[subtype] = append(possibleContainers[subtype], bagType)
		}
	}
	resultsMap := make(map[string]bool)
	_GetPermutations("shiny gold", possibleContainers, resultsMap)
	fmt.Println(len(resultsMap))
}
