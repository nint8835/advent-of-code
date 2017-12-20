from typing import List


def redistribute_memory(memory: List[int]):
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


def do_reallocate(memory: List[int]):
    seen_states = []
    while memory not in seen_states:
        seen_states.append(memory)
        memory = redistribute_memory(memory[:])
    return len(seen_states) - seen_states.index(memory)
