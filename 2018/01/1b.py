import sys
total = 0
seen = []
lines = list(open("input_1b.txt").readlines())
while True:
    for line in lines:
        total += int(line)
        print(total)
        if total in seen:
            sys.exit(0)
        else:
            seen.append(total)
print(total)