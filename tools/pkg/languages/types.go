package languages

import (
	"path/filepath"
)

type Language string

const (
	LanguageFSharp            Language = "F#"
	LanguageFSharpInteractive Language = "F# (Script)"
	LanguageGo                Language = "Go"
	LanguagePython            Language = "Python"
)

var extensionLanguages = map[string]Language{
	".fs":  LanguageFSharp,
	".fsx": LanguageFSharpInteractive,
	".go":  LanguageGo,
	".py":  LanguagePython,
}

func IdentifyLanguage(filePath string) (Language, error) {
	ext := filepath.Ext(filePath)
	lang, ok := extensionLanguages[ext]
	if !ok {
		return "", ErrUnknownLanguage
	}

	return lang, nil
}
