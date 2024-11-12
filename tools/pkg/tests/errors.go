package tests

import (
	"errors"
)

// ErrSecretKeyUnset is returned when the AOC_SECRET_KEY environment variable is not set
var ErrSecretKeyUnset = errors.New("AOC_SECRET_KEY is not set")

// ErrNoSolutionFile is returned when no solution file is found in the day's directory
var ErrNoSolutionFile = errors.New("no solution file found")

// ErrFailedTest is returned when the output does not match the expected output
var ErrFailedTest = errors.New("output does not match expected output")
