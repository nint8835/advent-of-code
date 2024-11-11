package cmd

import (
	"fmt"
	"os"
	"os/exec"
	"strings"

	"github.com/spf13/cobra"
)

var rootCmd = &cobra.Command{
	Use:   "tools",
	Short: "A variety of tools for managing my Advent of Code solutions",
}

func Execute() {
	if err := rootCmd.Execute(); err != nil {
		_, _ = fmt.Fprintln(os.Stderr, err)
		os.Exit(1)
	}
}

func ensureWorkingDirectory() {
	cmd := exec.Command("git", "rev-parse", "--show-toplevel")
	out, err := cmd.Output()
	if err != nil {
		_, _ = fmt.Fprintf(os.Stderr, "Failed to locate git repo root: %s\n", err)
		os.Exit(1)
	}

	repoRoot := strings.TrimSpace(string(out))

	if err := os.Chdir(repoRoot); err != nil {
		_, _ = fmt.Fprintf(os.Stderr, "Failed to change working directory to repo root: %s\n", err)
		os.Exit(1)
	}
}

func init() {
	cobra.OnInitialize(ensureWorkingDirectory)
}
