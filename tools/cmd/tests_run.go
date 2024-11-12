package cmd

import (
	"log"

	"github.com/spf13/cobra"

	"github.com/nint8835/advent-of-code/tools/pkg/tests"
)

var testsRunCmd = &cobra.Command{
	Use:   "run",
	Short: "Run all tests",
	Run: func(cmd *cobra.Command, args []string) {
		failed, err := tests.RunAll()
		if err != nil {
			log.Fatalf("failed to run tests: %v", err)
		}

		if len(failed) > 0 {
			log.Fatalf("failed tests: %v", failed)
		}
	},
}

func init() {
	testsCmd.AddCommand(testsRunCmd)
}
