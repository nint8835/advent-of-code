package main

import (
	"bufio"
	"fmt"
	"os"
	"regexp"
	"strings"
)

var _RequiredFields map[string]*regexp.Regexp = map[string]*regexp.Regexp{
	"byr": regexp.MustCompile(`^(19[2-9][0-9]|200[0-3])$`),
	"iyr": regexp.MustCompile(`^(201[0-9]|2020)$`),
	"eyr": regexp.MustCompile(`^(202[0-9]|2030)$`),
	"hgt": regexp.MustCompile(`^(1[5-8][0-9]cm|19[0-3]cm|59in|7[0-6]in|6[0-9]in)$`),
	"hcl": regexp.MustCompile(`^#[0-9a-f]{6}$`),
	"ecl": regexp.MustCompile(`^amb|blu|brn|gry|grn|hzl|oth$`),
	"pid": regexp.MustCompile(`^[0-9]{9}$`),
}

type _Passport struct {
	fields map[string]string
}

func (passport *_Passport) ParseLine(line string) {
	lineFields := strings.Split(line, " ")
	for _, field := range lineFields {
		fieldParts := strings.Split(field, ":")
		passport.fields[fieldParts[0]] = fieldParts[1]
	}
}

func (passport *_Passport) Valid() bool {
	for fieldName, fieldValRegex := range _RequiredFields {
		fieldVal, found := passport.fields[fieldName]
		if !found {
			return false
		}
		if !fieldValRegex.MatchString(fieldVal) {
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
			passport = &_Passport{make(map[string]string)}
		}
		passport.ParseLine(text)
	}
	if passport.Valid() {
		valid++
	}
	fmt.Println(valid)
}
