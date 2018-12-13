import sys

def CalcProb(prof, kmer):
    prob = 1
    for i in range(0, len(kmer)):
        if kmer[i] == 'A':
            prob = prob * prof[0][i]
        elif kmer[i] == 'C':
            prob = prob * prof[1][i]
        elif kmer[i] == 'G':
            prob = prob * prof[2][i]
        elif kmer[i] == 'T':
            prob = prob * prof[3][i]
    return prob

def FindBestProbPattern(text, K, prof):
    bestPattern = ""
    bestProb = 0.0
    for i in range(len(text) - K + 1):
        kmer = text[i:i + K]
        nProbablity = CalcProb(prof, kmer)
        if nProbablity > bestProb:
            bestPattern = kmer
            bestProb = nProbablity
    return bestPattern

def main():

    text = input()
    K = input()
    prof = []
    for i in range(0,4):
        prof.append([float(j) for j in input().split()])
    RES = FindBestProbPattern(text, int(K), prof)
    print(RES)

if __name__ == "__main__":
    main()
