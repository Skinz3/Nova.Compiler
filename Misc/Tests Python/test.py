import time

def test(x):
    """This is a recursive function
    to find the factorial of an integer"""

    if 900 < x:
	    return x	
    else:
        return test(x+1)


start_time = time.time()
test(0)
print("--- %s seconds ---" % (time.time() - start_time))