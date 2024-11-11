import pathlib
import re

year_pattern = re.compile(r"^\d{4}$")
day_pattern = re.compile(r"^\d{2}$")

language_extensions = {
    ".fsx": "F#",
    ".py": "Python",
    ".go": "Go",
}

languages: dict[str, dict[str, str]] = {}

for path in sorted(pathlib.Path().iterdir(), key=lambda p: p.name):
    if not path.is_dir() or not year_pattern.match(path.name):
        continue

    languages[path.name] = {}

    for subpath in sorted(path.iterdir(), key=lambda p: p.name):
        if not subpath.is_dir() or not day_pattern.match(subpath.name):
            continue

        for file in subpath.iterdir():
            if file.suffix in language_extensions:
                languages[path.name][subpath.name] = language_extensions[file.suffix]
                break

with open("README.md", "w") as f:
    f.write("# Advent of Code\n\n")

    f.write("My solutions to [Advent of Code](https://adventofcode.com/).\n\n")

    f.write("| Year | Days | Languages | Solutions |\n")
    f.write("| ---- | ---- | --------- | --------- |\n")

    for year, days in languages.items():
        f.write(
            f"| [{year}](https://adventofcode.com/{year}) | {len(days)} | {', '.join(sorted(set(days.values())))} | [Solutions](./{year}) |\n"
        )

for year, days in languages.items():
    with open(f"{year}/README.md", "w") as f:
        f.write(f"# Advent of Code {year}\n\n")

        f.write("| Day | Language | Solution |\n")
        f.write("| --- | -------- | -------- |\n")

        for day, language in days.items():
            f.write(
                f"| [{day}](https://adventofcode.com/{year}/day/{day}) | {language} | [Solution](./{day}) |\n"
            )
