package readmes

import (
	"fmt"
	"os"
	"strings"

	"github.com/nint8835/advent-of-code/tools/pkg/solutions"
	"github.com/nint8835/advent-of-code/tools/pkg/utils"
)

func GenerateRootReadme() error {
	readme, err := os.OpenFile("README.md", os.O_CREATE|os.O_TRUNC|os.O_WRONLY, 0644)
	if err != nil {
		return fmt.Errorf("failed to open README.md: %w", err)
	}
	defer utils.DeferClose(readme)

	_, err = readme.WriteString(`# Advent of Code

My solutions to [Advent of Code](https://adventofcode.com/).

| Year | Days | Languages | Solutions |
| ---- | ---- | --------- | --------- |
`)
	if err != nil {
		return fmt.Errorf("failed to write readme header: %w", err)
	}

	years, err := solutions.ListYears()

	for _, year := range years {
		days, err := solutions.ListYearDays(year)
		if err != nil {
			return fmt.Errorf("failed to list year days: %w", err)
		}

		langs, err := determineYearLanguages(year)
		if err != nil {
			return fmt.Errorf("failed to determine year languages: %w", err)
		}

		langStrings := make([]string, len(langs))
		for i, lang := range langs {
			langStrings[i] = string(lang)
		}

		_, err = readme.WriteString(
			fmt.Sprintf(
				"| [%s](https://adventofcode.com/%s) | %d | %s | [Solutions](./%s) |\n",
				year, year, len(days), strings.Join(langStrings, ", "), year,
			),
		)
		if err != nil {
			return fmt.Errorf("failed to write year row: %w", err)
		}
	}

	return nil
}
