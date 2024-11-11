import sys

lines = list(map(lambda x: x.replace("\n", ""), open("input_2a.txt").readlines()))
min = 10000
min_p = (0,0)
for i in range(len(lines)-1):
    for j in range(i + 1, len(lines)):
        diff = sum(1 for a, b in zip(lines[i], lines[j]) if a != b)
        if diff < min:
            min_p = (i, j)
            min = diff
print(min_p)
print(min)
print(lines[min_p[0]], lines[min_p[1]])