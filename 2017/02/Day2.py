def get_checksum_pt1(table: str) -> int:
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


def get_checksum_pt2(table: str) -> int:
    total = 0
    for line in table.split("\n"):
        line = line.replace("\t", " ")
        items = line.split(" ")
        if items == [""]:
            continue
        items = list(map(int, items))
        for i in range(len(items)):
            for x in range(i + 1, len(items)):
                if items[i] % items[x] == 0:
                    total += items[i] / items[x]
                elif items[x] % items[i] == 0:
                    total += items[x] / items[i]

    return int(total)


input_data = open("input.txt").read().strip()

print(get_checksum_pt1(input_data))
print(get_checksum_pt2(input_data))
