def execute_pt1(offsets: list) -> int:
    pos = 0
    moves = 0
    while pos >= 0 and pos < len(offsets):
        new = offsets[pos]
        offsets[pos] += 1
        pos += new
        moves += 1
    return moves


def execute_pt2(offsets: list) -> int:
    pos = 0
    moves = 0
    while pos >= 0 and pos < len(offsets):
        new = offsets[pos]
        if new >= 3:
            offsets[pos] -= 1
        else:
            offsets[pos] += 1
        pos += new
        moves += 1
    return moves


input_data = list(map(int, open("input.txt").read().split("\n")))

print(execute_pt1(input_data[:]))
print(execute_pt2(input_data[:]))
