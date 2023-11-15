package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strconv"
	"strings"
)

func main() {
	scanner := bufio.NewScanner(os.Stdin)

	scanner.Scan()
	startTimeStr := scanner.Text()
	scanner.Scan()
	lineIdsStr := scanner.Text()

	startTime, _ := strconv.Atoi(startTimeStr)

	lineIds := make([]int, 0)

	for _, lineIDStr := range strings.Split(lineIdsStr, ",") {
		lineID, err := strconv.Atoi(lineIDStr)
		if err != nil {
			continue
		}
		lineIds = append(lineIds, lineID)
	}

	minimumDelay, quickestLineID := math.MaxInt64, 0

	for _, lineID := range lineIds {
		delay := lineID - (startTime % lineID)
		if delay < minimumDelay {
			minimumDelay = delay
			quickestLineID = lineID
		}
	}

	fmt.Println(minimumDelay * quickestLineID)
}
