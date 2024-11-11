total = 0
with open("input_1a.txt") as f:
    for line in f:
        total += int(line)
print(total)
