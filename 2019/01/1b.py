print(sum((f:=lambda m:b+f(b) if (b:=__import__("math").floor(m/3.0)-2)>0 else 0)(m) for m in map(int,open("i"))))
