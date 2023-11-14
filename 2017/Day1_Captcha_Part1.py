def solve_captcha(captcha: str) -> int:
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


if __name__ == "__main__":
    print(solve_captcha(input("Input: ")))
