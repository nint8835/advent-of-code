package cmd

import (
	"github.com/spf13/cobra"
)

var testsCmd = &cobra.Command{
	Use:   "tests",
	Short: "Run & manage tests",
}

func init() {
	rootCmd.AddCommand(testsCmd)
}
