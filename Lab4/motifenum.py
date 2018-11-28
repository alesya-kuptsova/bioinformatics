import sys


def K_mers(pattern):
    member_in_dnk = ["A", "C", "G", "T"]
    result = set()
    i = 0
    while i < len(pattern):
        for j in member_in_dnk:
            result.add(pattern[:i] + j + pattern[i+1:])
        i += 1

    return result

def Diff(str1, str2):
    diffs = 0
    for char1, char2 in zip(str1, str2):
        if (char1 != char2):
            diffs += 1
    return diffs

def inEachString(pattern, DNAPart, d, k):
    for i in range(len(DNAPart) - k + 1):
        window = DNAPart[i:i + k]
        if (Diff(pattern, window) <= d):
            return True
    return False

def MotifEnumeration(DNA, k, d):
    patterns = []
    i = 0
    for dnaParts in DNA:
        for i in range(len(DNA[0])-k+1):
            pattern = dnaParts[i:i + k]
            Motifs = K_mers(pattern)
            for motif in Motifs:
                flagMeeting = True
                for j in range(len(DNA)):
                    if not inEachString(motif, DNA[j], d, k):
                        flagMeeting = False
                        break
                if flagMeeting:
                    patterns.append(motif)

    patterns = set(patterns)
    patterns = list(sorted(patterns))
    return ' '.join(patterns)

# main program
if __name__ == "__main__":
    k, d = input().split()
    string_split = []
    while True:
        temp = sys.stdin.readline()
        if temp != '':
            string_split.append(str(temp).replace('\n', ''))
        else:
            break

    output = MotifEnumeration(string_split, int(k), int(d))
    print(output)
