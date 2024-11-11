package readmes

import (
	"fmt"

	"github.com/nint8835/advent-of-code/tools/pkg/solutions"
)

func GenerateAllReadmes() error {
	years, err := solutions.ListYears()
	if err != nil {
		return fmt.Errorf("failed to list years: %w", err)
	}

	err = GenerateRootReadme()
	if err != nil {
		return fmt.Errorf("failed to generate root README: %w", err)
	}

	for _, year := range years {
		err = GenerateYearReadme(year)
		if err != nil {
			return fmt.Errorf("failed to generate year %s README: %w", year, err)
		}
	}

	return nil
}
