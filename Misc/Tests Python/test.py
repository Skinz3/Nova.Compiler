import time
import sys

def main():
    vect = []

    for i in range(0,10000000) :
        vect.append(i)

    



		


sys.setrecursionlimit(10000000)
start_time = time.time()
main()
print("--- %s seconds ---" % (time.time() - start_time))