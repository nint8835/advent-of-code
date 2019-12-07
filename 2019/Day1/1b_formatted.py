print(
    sum(
        (lambda a: lambda v: a(a, v))(
            lambda x, m: b + x(x, b)
            if (b := __import__("math").floor(m / 3.0) - 2) > 0
            else 0
        )(m)
        for m in map(int, open("i"))
    )
)
