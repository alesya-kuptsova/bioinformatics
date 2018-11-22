def CreateMapAcidMass():
    map = {
     'G': 57,
     'A': 71,
     'S': 87,
     'P': 97,
     'V': 99,
     'T': 101,
     'C': 103,
     'I': 113,
     'L': 113,
     'N': 114,
     'D': 115,
     'K': 128,
     'Q': 128,
     'E': 129,
     'M': 131,
     'H': 137,
     'F': 147,
     'R': 156,
     'Y': 163,
     'W': 186}
    return map



mapAminoAcidMass = CreateMapAcidMass()

def Linearspectr(pept):
    result = []
    i = 0
    k = 1
    while k < len(pept):
        while i < len(pept) - k + 1:
            j = 0
            temp = 0
            while j < k:
                temp += int(mapAminoAcidMass[pept[i + j]])
                j += 1
            result.append(temp)
            i += 1
        i = 0
        k += 1
    if len(pept) != 0:
        temp = 0
        for c in pept:
            temp += int(mapAminoAcidMass[c])
        result.append(temp)
        result.append(0)
    result.sort()
    return result

def LinearScore(pept, spectr):
    exp_spectr = []
    th_spectr = Linearspectr(pept)
    for i in spectr:
        exp_spectr.append(int(i))

    score = 0
    i = 0
    j = 0
    while (i < len(th_spectr) and j < len(exp_spectr)):
        if (th_spectr[i] == exp_spectr[j]):
            i += 1
            j += 1
            score += 1
        else:
            if (th_spectr[i] < exp_spectr[j]):
                i += 1
            else:
                j += 1
    return score

def GetpeptMass(pept):
    mass = 0
    for c in pept:
        mass += mapAminoAcidMass[c]
    return mass

def ParentMass(spectr):
    return spectr[-1]

def Expand(pepts):
    result = set()
    aminoacids = set({'A', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'K', 'M', 'N', 'P', 'R', 'S', 'T', 'V', 'W', 'Y'})

    if(len(pepts)==0):
        result = aminoacids
    else:
        for i in pepts:
            for j in aminoacids:
                result.add(i + j)
    return result

def Trim(n, Leaderboard, spectr):
    result = set()
    pept_scores = []
    for i in Leaderboard:
        pept_scores.append([i, LinearScore(i, spectr)])
    pept_scores = sorted(pept_scores, key = lambda element: element[1], reverse=True)
    min_length = min(len(pept_scores), int(n))
    check = True
    add = 0
    while (check):
        if (add < min_length):
            result.add(pept_scores[add][0])
            add += 1
        else:
            if(add < len(pept_scores) and pept_scores[min_length-1][1] == pept_scores[add][1]):
                result.add(pept_scores[add][0])
                add += 1
            else:
                check = False

    return result

def LeaderboardCyclopeptSequencing(n, spectr_str):
    spectr = []
    for i in spectr_str.split():
        spectr.append(int(i))

    check = True
    Leaderboard = set()
    LeadPeptid = ''
    while (len(Leaderboard) != 0 or check):
        check = False
        remove_pepts = []
        Leaderboard = Expand(Leaderboard)
        for p in Leaderboard:
            if (GetpeptMass(p) == ParentMass(spectr)):
                if (LinearScore(p, spectr) > LinearScore(LeadPeptid, spectr)):
                    LeadPeptid = p
            else:
                if (GetpeptMass(p) > ParentMass(spectr)):
                    remove_pepts.append(p)
        for p in remove_pepts:
            Leaderboard.remove(p)
        Leaderboard = Trim(n, Leaderboard, spectr)

    return LeadPeptid

def ConvertpeptsToMasses(peptid):
    result_str = ''
    for i in peptid:
        result_str += str(mapAminoAcidMass[i]) + '-'
    return result_str[:-1]

def main():
    N = input()
    spectr = input()
    result = LeaderboardCyclopeptSequencing(int(N), spectr)
    print(ConvertpeptsToMasses(result))

if __name__ == "__main__":
    main()
