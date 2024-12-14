package languages

import (
	"fmt"
	"os"
	"os/exec"
	"path"
)

var executeCmd = map[Language][]string{
	LanguagePython: {"python"},
	LanguageGo:     {"go", "run"},
	LanguageFSharp: {"dotnet", "fsi"},
}

func ExecuteFile(file string) ([]byte, error) {
	language, err := IdentifyLanguage(file)
	if err != nil {
		return nil, err
	}

	cwd, err := os.Getwd()
	if err != nil {
		return nil, fmt.Errorf("failed to get current working directory: %w", err)
	}

	newWd := path.Dir(file)
	if err := os.Chdir(newWd); err != nil {
		return nil, fmt.Errorf("failed to change working directory to %s: %w", newWd, err)
	}

	execArgs := append(executeCmd[language], path.Base(file))

	cmd := exec.Command(execArgs[0], execArgs[1:]...)
	output, err := cmd.Output()
	if err != nil {
		if exitErr, ok := err.(*exec.ExitError); ok {
			fmt.Printf("Stdout: %s\n", string(output))
			fmt.Printf("Stderr: %s\n", string(exitErr.Stderr))
		}

		return nil, fmt.Errorf("failed to execute file: %w", err)
	}

	if err := os.Chdir(cwd); err != nil {
		return nil, fmt.Errorf("failed to change working directory back to %s: %w", cwd, err)
	}

	return output, nil
}
