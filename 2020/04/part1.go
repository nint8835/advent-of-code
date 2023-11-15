package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

var _RequiredFields []string = []string{
	"byr",
	"iyr",
	"eyr",
	"hgt",
	"hcl",
	"ecl",
	"pid",
}

type _Passport struct {
	fields map[string]bool
}

func (passport *_Passport) ParseLine(line string) {
	lineFields := strings.Split(line, " ")
	for _, field := range lineFields {
		fieldParts := strings.Split(field, ":")
		passport.fields[fieldParts[0]] = true
	}
}

func (passport *_Passport) Valid() bool {
	for _, requiredField := range _RequiredFields {
		_, found := passport.fields[requiredField]
		if !found {
			return false
		}
	}
	return true
}

func main() {
	scanner := bufio.NewScanner(os.Stdin)
	var passport *_Passport
	valid := 0
	for scanner.Scan() {
		text := scanner.Text()

		if text == "" {
			if passport.Valid() {
				valid++
			}
			passport = nil
			continue
		}

		if passport == nil {
			passport = &_Passport{make(map[string]bool)}
		}
		passport.ParseLine(text)
	}
	if passport.Valid() {
		valid++
	}
	fmt.Println(valid)
}
