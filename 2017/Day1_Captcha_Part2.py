def solve_captcha(captcha: str) -> int:
    offset = int(len(captcha) / 2)
    total = 0
    for i in range(len(captcha)):
        second_position = i + offset
        if second_position >= len(captcha):
            second_position = i - offset
        if captcha[i] == captcha[second_position]:
            total += int(captcha[i])
    return total
