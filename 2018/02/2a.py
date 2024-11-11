import string

two = 0
three = 0

with open("input_2a.txt") as f:
    for line in f:
        done_two = False
        done_three = False
        for char in string.ascii_lowercase:
            if line.count(char) == 2:
                if not done_two:
                    two += 1
                    done_two = True
            elif line.count(char) == 3:
                if not done_three:
                    three += 1
                    done_three = True

print(two * three)