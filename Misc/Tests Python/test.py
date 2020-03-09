import time
import sys

def test(x):


    if 5000 < x:
	    return x	
    else:
        return test(x+1)

sys.setrecursionlimit(10000000)
start_time = time.time()
test(0)
print("--- %s seconds ---" % (time.time() - start_time))