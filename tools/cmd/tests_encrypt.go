package cmd

import (
	"log"

	"github.com/spf13/cobra"

	"github.com/nint8835/advent-of-code/tools/pkg/tests"
)

var testsEncryptCmd = &cobra.Command{
	Use:   "encrypt [year] [day]",
	Short: "Encrypt saved input / output values for a given test",
	Run: func(cmd *cobra.Command, args []string) {
		err := tests.EncryptDayFiles(args[0], args[1])
		if err != nil {
			log.Fatalf("failed to encrypt files: %v", err)
		}
	},
}

func init() {
	testsCmd.AddCommand(testsEncryptCmd)
}
