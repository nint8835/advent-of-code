package solutions

import (
	"errors"
	"fmt"
	"io"
	"os"
	"path/filepath"
	"regexp"
	"slices"
	"strings"

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

// GetDayStars returns the number of stars earned for a given day
func GetDayStars(year, day string) (int, error) {
	files, err := ListDayFiles(year, day)
	if err != nil {
		return 0, fmt.Errorf("failed to list day files: %w", err)
	}

	for _, file := range files {
		f, err := os.Open(filepath.Join(year, day, file))
		if err != nil {
			return 0, fmt.Errorf("failed to open file: %w", err)
		}
		defer f.Close()

		fileBytes, err := io.ReadAll(f)
		if err != nil {
			return 0, fmt.Errorf("failed to read file: %w", err)
		}

		if strings.Contains(string(fileBytes), "aoc-tools:stars 1") {
			return 1, nil
		} else {
			return 2, nil
		}
	}

	return 0, errors.New("no files found")
}
