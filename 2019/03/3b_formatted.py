print(
    (
        a := (
            w := lambda m, x, y, p, s: w(
                m[1:],
                {
                    "U": lambda m: x,
                    "D": lambda m: x,
                    "R": lambda m: x + m,
                    "L": lambda m: x - m,
                }[m[0][0]](int(m[0][1:])),
                {
                    "U": lambda m: y + m,
                    "D": lambda m: y - m,
                    "R": lambda m: y,
                    "L": lambda m: y,
                }[m[0][0]](int(m[0][1:])),
                {
                    **p,
                    **(
                        {
                            "U": lambda m: {
                                (x, y + i): s + i
                                for i in range(1, m + 1)
                                if (x, y + m) != (0, 0)
                            },
                            "D": lambda m: {
                                (x, y - i): s + i
                                for i in range(1, m + 1)
                                if (x, y - m) != (0, 0)
                            },
                            "R": lambda m: {
                                (x + i, y): s + i
                                for i in range(1, m + 1)
                                if (x + i, y) != (0, 0)
                            },
                            "L": lambda m: {
                                (x - i, y): s + i
                                for i in range(1, m + 1)
                                if (x - i, y) != (0, 0)
                            },
                        }[m[0][0]](int(m[0][1:]))
                    ),
                },
                s + int(m[0][1:]),
            )
            if len(m) != 0
            else p
        )((f := open("i").read().split("\n"))[0].split(","), 0, 0, {}, 0),
        sorted(
            [s + a[c] for c, s in w(f[1].split(","), 0, 0, {}, 0).items() if c in a]
        )[0],
    )[1]
)
