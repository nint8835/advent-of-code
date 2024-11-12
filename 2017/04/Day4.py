from itertools import permutations


def is_valid_pt1(password: str) -> bool:
    words = password.split(" ")
    seen = []
    for word in words:
        if word in seen:
            return False
        else:
            seen.append(word)
    return True


def find_valid_pt1(data: str) -> int:
    return len(list(filter(is_valid_pt1, data.split("\n"))))


def is_valid_pt2(password: str) -> bool:
    words = password.split(" ")
    seen = []
    for word in words:
        perm = permutations(word)
        for i in perm:
            i = "".join(i)
            if i in seen:
                return False
        seen.append(word)
    return True


def find_valid_pt2(data: str) -> int:
    return len(list(filter(is_valid_pt2, data.split("\n"))))


input_data = open("input.txt").read().strip()

print(find_valid_pt1(input_data))
print(find_valid_pt2(input_data))
