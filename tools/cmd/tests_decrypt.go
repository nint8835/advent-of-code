package cmd

import (
	"log"

	"github.com/spf13/cobra"

	"github.com/nint8835/advent-of-code/tools/pkg/tests"
)

var testsDecryptCmd = &cobra.Command{
	Use:   "decrypt",
	Short: "Decrypt saved input / output values for all tests",
	Run: func(cmd *cobra.Command, args []string) {
		err := tests.DecryptAllFiles()
		if err != nil {
			log.Fatalf("failed to decrypt files: %v", err)
		}
	},
}

func init() {
	testsCmd.AddCommand(testsDecryptCmd)
}
