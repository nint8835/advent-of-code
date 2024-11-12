package cmd

import (
	"errors"
	"fmt"
	"log"
	"os"
	"os/exec"
	"strings"

	"github.com/joho/godotenv"
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

func prepareToRun() {
	cmd := exec.Command("git", "rev-parse", "--show-toplevel")
	out, err := cmd.Output()
	if err != nil {
		log.Fatalf("failed to determine repo root: %v", err)
	}

	repoRoot := strings.TrimSpace(string(out))

	if err := os.Chdir(repoRoot); err != nil {
		log.Fatalf("failed to change directory to repo root: %v", err)
	}

	err = godotenv.Load()
	if err != nil && !errors.Is(err, os.ErrNotExist) {
		log.Fatalf("failed to load .env file: %v", err)
	}
}

func init() {
	cobra.OnInitialize(prepareToRun)
}
