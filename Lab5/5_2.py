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

def CrProf(motif):
    n = 4
    m = len(motif[0])
    prof = [[0] * m for i in range(n)]

    for i in range(len(motif)):
        for j in range(len(motif[i])):
            if motif[i][j] == 'A':
                prof[0][j] += 1
            if motif[i][j] == 'C':
                prof[1][j] += 1
            if motif[i][j] == 'G':
                prof[2][j] += 1
            if motif[i][j] == 'T':
                prof[3][j] += 1

    for i in range(n):
        for j in range(m):
            prof[i][j] = prof[i][j]/len(motif)

    return prof

def ProfMostProb(DNA, K, prof):
    bestPattern = DNA[0:0 + K]
    bestProbability = 0.0
    for i in range(len(DNA) - K + 1):
        kmer = DNA[i:i + K]
        nProbablity = CalcProb(prof, kmer)
        if nProbablity > bestProbability:
            bestPattern = kmer
            bestProbability = nProbablity
    return bestPattern

def Score(motif):
    score = 0
    for j in range(len(motif[0])):
        count = 0
        counts = [0, 0, 0, 0]
        for i in range(len(motif)):
            if motif[i][j] == 'A':
                counts[0] += 1
                count += 1
            if motif[i][j] == 'C':
                counts[1] += 1
                count += 1
            if motif[i][j] == 'G':
                counts[2] += 1
                count += 1
            if motif[i][j] == 'T':
                counts[3] += 1
                count += 1
        score += count - max(counts)
    return score

def GredMotifSearch(DNA, K, M):
    BestMotifs = []
    for i in range(M):
        BestMotifs.append(DNA[i][0:K])

    for i in range(len(DNA[0]) - K + 1):
        motif = []
        motif.append(DNA[0][i:i + K])

        for j in range(1, M):
            prof = CrProf(motif)
            motif.append(ProfMostProb(DNA[j], K, prof))

        if Score(motif) < Score(BestMotifs):
            BestMotifs = motif.copy()
    return BestMotifs

def PrintMotif(motif):
    for i in motif:
        print(i)


def main():

    K, M = input().split()
    DNA = sys.stdin.read().split('\n')
    RES = GredMotifSearch(DNA, int(K), int(M))
    PrintMotif(RES)

if __name__ == "__main__":
    main()
