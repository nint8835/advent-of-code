print(
    (
        a := (
            w := lambda m, x, y, p: w(
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
                                (x, i): True
                                for i in range(y, y + m + 1)
                                if (x, i) != (0, 0)
                            },
                            "D": lambda m: {
                                (x, i): True
                                for i in range(y - m, y + 1)
                                if (x, i) != (0, 0)
                            },
                            "R": lambda m: {
                                (h, y): True
                                for h in range(x, x + m + 1)
                                if (h, y) != (0, 0)
                            },
                            "L": lambda m: {
                                (h, y): True
                                for h in range(x - m, x + 1)
                                if (h, y) != (0, 0)
                            },
                        }[m[0][0]](int(m[0][1:]))
                    ),
                },
            )
            if len(m) != 0
            else p
        )((f := open("i").read().split("\n"))[0].split(","), 0, 0, {}),
        sorted([sum(c) for c in w(f[1].split(","), 0, 0, {}) if c in a])[0],
    )[1]
)
