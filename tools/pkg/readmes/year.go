package readmes

import (
	"fmt"
	"os"
	"path/filepath"

	"github.com/nint8835/advent-of-code/tools/pkg/solutions"
	"github.com/nint8835/advent-of-code/tools/pkg/utils"
)

func GenerateYearReadme(year string) error {
	readme, err := os.OpenFile(filepath.Join(year, "README.md"), os.O_CREATE|os.O_TRUNC|os.O_WRONLY, 0644)
	if err != nil {
		return fmt.Errorf("failed to open README.md: %w", err)
	}
	defer utils.DeferClose(readme)

	_, err = readme.WriteString(fmt.Sprintf(`# Advent of Code %s

| Day | Language | Solution |
| --- | -------- | -------- |
`, year))
	if err != nil {
		return fmt.Errorf("failed to write readme header: %w", err)
	}

	days, err := solutions.ListYearDays(year)
	if err != nil {
		return fmt.Errorf("failed to list year days: %w", err)
	}

	for _, day := range days {
		langs, err := determineDayLanguages(year, day)
		if err != nil {
			return fmt.Errorf("failed to determine day languages: %w", err)
		}

		if len(langs) != 1 {
			return fmt.Errorf("expected exactly one language for year %s day %s, got %d", year, day, len(langs))
		}

		langStrings := make([]string, len(langs))
		for i, lang := range langs {
			langStrings[i] = string(lang)
		}

		_, err = readme.WriteString(
			fmt.Sprintf(
				"| [%s](https://adventofcode.com/%s/day/%s) | %s | [Solution](./%s) |\n",
				day, year, day, langStrings[0], day,
			),
		)
		if err != nil {
			return fmt.Errorf("failed to write day row: %w", err)
		}
	}

	return nil
}
