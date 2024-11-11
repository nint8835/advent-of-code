package readmes

import (
	"errors"
	"fmt"
	"slices"

	"github.com/nint8835/advent-of-code/tools/pkg/languages"
	"github.com/nint8835/advent-of-code/tools/pkg/solutions"
)

func determineDayLanguages(year, day string) ([]languages.Language, error) {
	files, err := solutions.ListDayFiles(year, day)
	if err != nil {
		return nil, fmt.Errorf("failed to list day files: %w", err)
	}

	var langs []languages.Language

	for _, file := range files {
		lang, err := languages.IdentifyLanguage(file)
		if err != nil && !errors.Is(err, languages.ErrUnknownLanguage) {
			return nil, fmt.Errorf("failed to identify language for file %s: %w", file, err)
		}

		if !slices.Contains(langs, lang) {
			langs = append(langs, lang)
		}
	}

	slices.Sort(langs)

	return langs, nil
}

func determineYearLanguages(year string) ([]languages.Language, error) {
	days, err := solutions.ListYearDays(year)
	if err != nil {
		return nil, fmt.Errorf("failed to list year days: %w", err)
	}

	var langs []languages.Language

	for _, day := range days {
		dayLangs, err := determineDayLanguages(year, day)
		if err != nil {
			return nil, fmt.Errorf("failed to determine day languages: %w", err)
		}

		for _, lang := range dayLangs {
			if !slices.Contains(langs, lang) {
				langs = append(langs, lang)
			}
		}
	}

	slices.Sort(langs)

	return langs, nil
}
