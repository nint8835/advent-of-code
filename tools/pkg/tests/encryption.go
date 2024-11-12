package tests

import (
	"errors"
	"fmt"
	"io"
	"os"
	"path/filepath"

	"filippo.io/age"
	"filippo.io/age/armor"

	"github.com/nint8835/advent-of-code/tools/pkg/solutions"
	"github.com/nint8835/advent-of-code/tools/pkg/utils"
)

func makeIdentity() (age.Identity, error) {
	secretKey := os.Getenv("AOC_SECRET_KEY")
	if secretKey == "" {
		return nil, ErrSecretKeyUnset
	}

	return age.NewScryptIdentity(secretKey)
}

func makeRecipient() (age.Recipient, error) {
	secretKey := os.Getenv("AOC_SECRET_KEY")
	if secretKey == "" {
		return nil, ErrSecretKeyUnset
	}

	return age.NewScryptRecipient(secretKey)
}

var inputFileName = "input.txt"
var outputFileName = "output.txt"

func encryptFile(path string) error {
	r, err := makeRecipient()
	if err != nil {
		return fmt.Errorf("failed to create recipient: %w", err)
	}

	encryptedFilePath := path + ".enc"

	inputFile, err := os.Open(path)
	if err != nil {
		return fmt.Errorf("failed to open input file: %w", err)
	}
	defer utils.DeferClose(inputFile)

	outputFile, err := os.Create(encryptedFilePath)
	if err != nil {
		return fmt.Errorf("failed to create output file: %w", err)
	}
	defer utils.DeferClose(outputFile)

	armorWriter := armor.NewWriter(outputFile)
	defer utils.DeferClose(armorWriter)

	encryptOut, err := age.Encrypt(armorWriter, r)
	if err != nil {
		return fmt.Errorf("failed to create encryption writer: %w", err)
	}
	defer utils.DeferClose(encryptOut)

	_, err = io.Copy(encryptOut, inputFile)
	if err != nil {
		return fmt.Errorf("failed to copy input to encryption writer: %w", err)
	}

	return nil
}

func EncryptDayFiles(year, day string) error {
	dayPath := filepath.Join(year, day)
	inputPath := filepath.Join(dayPath, inputFileName)
	outputPath := filepath.Join(dayPath, outputFileName)

	if err := encryptFile(inputPath); err != nil && !errors.Is(err, os.ErrNotExist) {
		return fmt.Errorf("failed to encrypt input file: %w", err)
	}

	if err := encryptFile(outputPath); err != nil && !errors.Is(err, os.ErrNotExist) {
		return fmt.Errorf("failed to encrypt output file: %w", err)
	}

	return nil
}

func EncryptYearFiles(year string) error {
	days, err := solutions.ListYearDays(year)
	if err != nil {
		return fmt.Errorf("failed to list year days: %w", err)
	}

	for _, day := range days {
		if err := EncryptDayFiles(year, day); err != nil {
			return fmt.Errorf("failed to encrypt day files: %w", err)
		}
	}

	return nil
}

func EncryptAllFiles() error {
	years, err := solutions.ListYears()
	if err != nil {
		return fmt.Errorf("failed to list years: %w", err)
	}

	for _, year := range years {
		if err := EncryptYearFiles(year); err != nil {
			return fmt.Errorf("failed to encrypt year files: %w", err)
		}
	}

	return nil
}
