print(sum([__import__("math").floor(int(i) / 3.0) - 2 for i in open("i")]))
