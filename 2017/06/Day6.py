from typing import List


def redistribute_memory_pt1(memory: List[int]):
    blocks = max(memory)
    index = memory.index(blocks)
    memory[index] = 0
    while blocks != 0:
        index += 1
        if index >= len(memory):
            index -= len(memory)
        memory[index] += 1
        blocks -= 1
    return memory


def do_reallocate_pt1(memory: List[int]):
    seen_states = []
    while memory not in seen_states:
        seen_states.append(memory)
        memory = redistribute_memory_pt1(memory[:])
    return len(seen_states)


def redistribute_memory_pt2(memory: List[int]):
    blocks = max(memory)
    index = memory.index(blocks)
    memory[index] = 0
    while blocks != 0:
        index += 1
        if index >= len(memory):
            index -= len(memory)
        memory[index] += 1
        blocks -= 1
    return memory


def do_reallocate_pt2(memory: List[int]):
    seen_states = []
    while memory not in seen_states:
        seen_states.append(memory)
        memory = redistribute_memory_pt2(memory[:])
    return len(seen_states) - seen_states.index(memory)


input_data = list(map(int, open("input.txt").read().split("\t")))

print(do_reallocate_pt1(input_data[:]))
print(do_reallocate_pt2(input_data[:]))
