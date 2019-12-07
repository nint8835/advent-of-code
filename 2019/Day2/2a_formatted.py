print(
    (lambda a: lambda t, i: a(a, t, i))(
        lambda a, t, i: t
        if t[i] == 99
        else a(
            a,
            [
                lambda t, a, b, c: [
                    t[a] + t[b] if x == c else i for x, i in enumerate(t)
                ],
                lambda t, a, b, c: [
                    t[a] * t[b] if x == c else i for x, i in enumerate(t)
                ],
            ][t[i] - 1](t, *t[i + 1 : i + 4]),
            i + 4,
        )
    )([(t := list(map(int, open("i").read().split(","))))[0], 12, 2, *t[3:]], 0)[0]
)
