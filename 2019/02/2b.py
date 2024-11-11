print(
    (
        (
            r := [
                (lambda l: [i for s in l for i in s if i[0] == 19690720])(
                    [
                        [
                            (
                                (
                                    f := (
                                        lambda t, i: t
                                        if t[i] == 99
                                        else f(
                                            [
                                                (
                                                    o := (
                                                        lambda y: lambda t, a, b, c: [
                                                            y(t, a, b) if x == c else i
                                                            for x, i in enumerate(t)
                                                        ]
                                                    )
                                                )(lambda t, a, b: t[a] + t[b]),
                                                o(lambda t, a, b: t[a] * t[b]),
                                            ][t[i] - 1](t, *t[i + 1 : i + 4]),
                                            i + 4,
                                        )
                                    )
                                )(
                                    [
                                        (
                                            t := list(
                                                map(int, open("i").read().split(","))
                                            )
                                        )[0],
                                        n,
                                        v,
                                        *t[3:],
                                    ],
                                    0,
                                )[0],
                                n,
                                v,
                            )
                            for n in range(100)
                        ]
                        for v in range(100)
                    ]
                )
            ][0][0]
        )[1]
        * 100
    )
    + r[2]
)
