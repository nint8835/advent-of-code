def is_valid(password: str) -> bool:
    words = password.split(" ")
    seen = []
    for word in words:
        if word in seen:
            return False
        else:
            seen.append(word)
    return True


def find_valid(data: str) -> int:
    return len(list(filter(is_valid, data.split("\n"))))


with open("input.txt") as f:
    print(find_valid(f.read()))
