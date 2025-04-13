package languages

import (
	"fmt"
	"os"
	"os/exec"
	"path"
)

type ExecOptions struct {
	BaseCmd    []string
	ExecInRoot bool
}

var execOpts = map[Language]ExecOptions{
	LanguagePython:            {BaseCmd: []string{"python"}},
	LanguageGo:                {BaseCmd: []string{"go", "run"}},
	LanguageFSharp:            {BaseCmd: []string{"dotnet", "run", "--project", "runner"}, ExecInRoot: true},
	LanguageFSharpInteractive: {BaseCmd: []string{"dotnet", "fsi"}},
}

func ExecuteFile(file string) ([]byte, error) {
	language, err := IdentifyLanguage(file)
	if err != nil {
		return nil, err
	}

	opts := execOpts[language]

	cwd, err := os.Getwd()
	if err != nil {
		return nil, fmt.Errorf("failed to get current working directory: %w", err)
	}

	newWd := path.Dir(file)

	if opts.ExecInRoot {
		newWd = cwd
	} else {
		file = path.Base(file)
	}

	if err := os.Chdir(newWd); err != nil {
		return nil, fmt.Errorf("failed to change working directory to %s: %w", newWd, err)
	}

	execArgs := append(opts.BaseCmd, file)

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
