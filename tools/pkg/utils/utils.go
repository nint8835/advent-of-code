package utils

import (
	"fmt"
	"io"
)

func DeferClose(closer io.Closer) {
	err := closer.Close()
	if err != nil {
		fmt.Printf("Failed to close resource: %v\n", err)
	}
}
