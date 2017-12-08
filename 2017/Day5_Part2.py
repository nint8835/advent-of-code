def execute(offsets: list) -> int:
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


with open("input.txt") as f:
    print(execute(list(map(int, f.read().split("\n")))))