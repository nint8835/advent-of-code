def get_checksum(table: str) -> int:
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


with open("input.txt") as f:
    print(get_checksum(f.read()))
