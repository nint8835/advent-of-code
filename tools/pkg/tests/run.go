package tests

import (
	"errors"
	"fmt"
	"io"
	"log"
	"os"
	"path/filepath"

	"github.com/nint8835/advent-of-code/tools/pkg/languages"
	"github.com/nint8835/advent-of-code/tools/pkg/solutions"
	"github.com/nint8835/advent-of-code/tools/pkg/utils"
)

func Run(year, day string) error {
	dayPath := filepath.Join(year, day)

	outputFile, err := os.Open(filepath.Join(dayPath, "output.txt"))
	if err != nil {
		if errors.Is(err, os.ErrNotExist) {
			log.Default().Printf("No output file found for %s/%s, skipping test", year, day)
			return nil
		}
		return fmt.Errorf("error opening output file: %w", err)
	}
	defer utils.DeferClose(outputFile)

	expectedOutput, err := io.ReadAll(outputFile)
	if err != nil {
		return fmt.Errorf("error reading output file: %w", err)
	}

	dayFiles, err := solutions.ListDayFiles(year, day)
	if err != nil {
		return fmt.Errorf("error listing day files: %w", err)
	}

	var solutionFile string

	for _, file := range dayFiles {
		if _, err = languages.IdentifyLanguage(file); err == nil {
			solutionFile = filepath.Join(dayPath, file)
			break
		}
	}

	if solutionFile == "" {
		return ErrNoSolutionFile
	}

	output, err := languages.ExecuteFile(solutionFile)
	if err != nil {
		return fmt.Errorf("error executing solution file: %w", err)
	}

	if string(output) != string(expectedOutput) {
		return ErrFailedTest
	}

	return nil
}

func RunYear(year string) ([]string, error) {
	days, err := solutions.ListYearDays(year)
	if err != nil {
		return nil, fmt.Errorf("error listing days: %w", err)
	}

	var failedTests []string
	var allErrors []error

	for _, day := range days {
		if err := Run(year, day); err != nil {
			if errors.Is(err, ErrFailedTest) {
				failedTests = append(failedTests, day)
			} else {
				allErrors = append(allErrors, fmt.Errorf("error running day %s: %w", day, err))
			}
		}
	}

	return failedTests, errors.Join(allErrors...)
}

func RunAll() ([]string, error) {
	years, err := solutions.ListYears()
	if err != nil {
		return nil, fmt.Errorf("error listing years: %w", err)
	}

	var failedTests []string
	var allErrors []error

	for _, year := range years {
		failed, err := RunYear(year)
		if err != nil {
			allErrors = append(allErrors, fmt.Errorf("error running year %s: %w", year, err))
		}

		for _, day := range failed {
			failedTests = append(failedTests, fmt.Sprintf("%s/%s", year, day))
		}
	}

	return failedTests, errors.Join(allErrors...)
}
