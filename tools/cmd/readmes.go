package cmd

import (
	"log"

	"github.com/spf13/cobra"

	"github.com/nint8835/advent-of-code/tools/pkg/readmes"
)

var readmesCmd = &cobra.Command{
	Use:   "readmes",
	Short: "Generate READMEs for the repository",
	Run: func(cmd *cobra.Command, args []string) {
		err := readmes.GenerateAllReadmes()
		if err != nil {
			log.Fatalf("failed to generate READMEs: %v", err)
		}
	},
}

func init() {
	rootCmd.AddCommand(readmesCmd)
}
