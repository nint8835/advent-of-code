def execute(offsets: list) -> int:
    pos = 0
    moves = 0
    while pos >= 0 and pos < len(offsets):
        new = offsets[pos]
        offsets[pos] += 1
        pos += new
        moves += 1
    return moves


with open("input.txt") as f:
    print(execute(list(map(int, f.read().split("\n")))))