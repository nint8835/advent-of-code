def solve_captcha_pt1(captcha: str) -> int:
    last = None
    total = 0
    for char in captcha:
        char = int(char)
        if char == last:
            total += char
        last = char
    if len(captcha) > 2:
        if captcha[-1] == captcha[0]:
            total += int(captcha[0])

    return total


def solve_captcha_pt2(captcha: str) -> int:
    offset = int(len(captcha) / 2)
    total = 0
    for i in range(len(captcha)):
        second_position = i + offset
        if second_position >= len(captcha):
            second_position = i - offset
        if captcha[i] == captcha[second_position]:
            total += int(captcha[i])
    return total


input_data = open("input.txt").read().strip()

print(solve_captcha_pt1(input_data))
print(solve_captcha_pt2(input_data))
