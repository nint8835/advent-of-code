def get_checksum(table: str) -> int:
    total = 0
    for line in table.split("\n"):
        line = line.replace("\t", " ")
        items = line.split(" ")
        if items == [""]:
            continue
        items = list(map(int, items))
        largest = max(items)
        smallest = min(items)
        diff = largest - smallest
        total += diff
    return total


with open("input.txt") as f:
    print(get_checksum(f.read()))