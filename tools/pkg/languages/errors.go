package languages

import (
	"errors"
)

// ErrUnknownLanguage is returned when a language cannot be identified
var ErrUnknownLanguage = errors.New("unknown language")
