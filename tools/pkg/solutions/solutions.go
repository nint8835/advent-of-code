package solutions

import (
	"fmt"
	"os"
	"path/filepath"
	"regexp"
	"slices"

	"github.com/nint8835/advent-of-code/tools/pkg/languages"
)

var yearPattern = regexp.MustCompile(`^\d{4}$`)
var dayPattern = regexp.MustCompile(`^\d{2}$`)

// ListYears returns a list of all years that have solutions
func ListYears() ([]string, error) {
	entries, err := os.ReadDir(".")
	if err != nil {
		return nil, fmt.Errorf("failed to list directory contents: %w", err)
	}

	var years []string

	for _, entry := range entries {
		if !entry.IsDir() {
			continue
		}

		if !yearPattern.MatchString(entry.Name()) {
			continue
		}

		years = append(years, entry.Name())
	}

	slices.Sort(years)

	return years, nil
}

// ListYearDays returns a list of all days that have solutions for a given year
func ListYearDays(year string) ([]string, error) {
	entries, err := os.ReadDir(year)
	if err != nil {
		return nil, fmt.Errorf("failed to list directory contents: %w", err)
	}

	var days []string

	for _, entry := range entries {
		if !entry.IsDir() {
			continue
		}

		if !dayPattern.MatchString(entry.Name()) {
			continue
		}

		days = append(days, entry.Name())
	}

	slices.Sort(days)

	return days, nil
}

// ListDayFiles returns a list of all files that contain solutions for a given day
func ListDayFiles(year, day string) ([]string, error) {
	entries, err := os.ReadDir(filepath.Join(year, day))
	if err != nil {
		return nil, fmt.Errorf("failed to list directory contents: %w", err)
	}

	var files []string

	for _, entry := range entries {
		if entry.IsDir() {
			continue
		}

		if _, err := languages.IdentifyLanguage(entry.Name()); err != nil {
			continue
		}

		files = append(files, entry.Name())
	}

	return files, nil
}
