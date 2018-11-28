import sys


def FindPatternFromNumber(num, n):
    num2char = {0: 'A', 1: 'C', 2: 'G', 3: 'T'}
    pattern = ''
    tmp = num
    while(n != len(pattern)):
        pattern += num2char[tmp % len(num2char)]
        tmp = tmp // len(num2char)
    return pattern

def HammingsDist(word, word1):
    diffChartsCount = 0
    for i in range(len(word)):
        if word[i] != word1[i]:
            diffChartsCount += 1
    return diffChartsCount

def DistancePatternText(pattern, dna):
    n = len(pattern)
    dist = 0
    for stringDna in dna:
        minDistString = float('inf')
        for i in range(len(stringDna) - n + 1):
            d = HammingsDist(pattern, stringDna[i:i + n])
            if d < minDistString:
                minDistString = d

        dist += minDistString
    return dist

def MedianString(dna, n):
    median = ''
    dist = float('inf')
    for i in range(4 ** n):
        pattern = FindPatternFromNumber(i, n)
        d = DistancePatternText(pattern, dna)
        if dist > d:
            dist = d
            median = pattern
    return median

def main():
    n = input()
    dna = sys.stdin.read().split('\n')
    res = MedianString(list(dna), int(n))
    print(res)

if __name__ == "__main__":
    main()
