print(
    (
        f := (
            lambda t, i: t
            if t[i] == 99
            else f(
                [
                    (
                        o := (
                            lambda y: lambda t, a, b, c: [
                                y(t, a, b) if x == c else i for x, i in enumerate(t)
                            ]
                        )
                    )(lambda t, a, b: t[a] + t[b]),
                    o(lambda t, a, b: t[a] * t[b]),
                ][t[i] - 1](t, *t[i + 1 : i + 4]),
                i + 4,
            )
        )
    )([(t := list(map(int, open("i").read().split(","))))[0], 12, 2, *t[3:]], 0)[0]
)
