package tests

import (
	"errors"
)

// ErrSecretKeyUnset is returned when the AOC_SECRET_KEY environment variable is not set
var ErrSecretKeyUnset = errors.New("AOC_SECRET_KEY is not set")
