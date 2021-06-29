import cv2

img1 = cv2.imread('D:/img/583Btn7K31R61.jpg')

e1 = cv2.getTickCount()
for i in range(5, 49, 2):
    img1 = cv2.medianBlur(img1, i)
e2 = cv2.getTickCount()
t = (e2 - e1) / cv2.getTickFrequency()
print("Time = " + str(t) + "s")

img1 = cv2.resize(img1, (int(img1.shape[1]/3), int(img1.shape[0]/3)))

cv2.imshow('res', img1)
cv2.waitKey(0)
cv2.destroyAllWindows()

# NOTE
# You can do the same with time module. Instead of cv2.getTickCount, use time.time() function.
# Then take the difference of two times.


# Optimization
img1 = cv2.imread('D:/img/583Btn7K31R61.jpg')

# check if optimization is enabled
print('cv2.useOptimized() = ' + str(cv2.useOptimized()))

res = cv2.medianBlur(img1, 49)
print('cv2.medianBlur(img1, 49) = ' + str(res))

# Disable it
cv2.setUseOptimized(False)

print('cv2.useOptimized() = ' + str(cv2.useOptimized()))

res = cv2.medianBlur(img1, 49)
print('cv2.medianBlur(img1, 49) = ' + str(res))


# Measuring Performance in IPython
# Step 1: Open command prompt and type ipython
# Step 2: Measure operations with %timeit
# Examples:
# In [10]: x = 5
#
# In [11]: %timeit y=x**2
# 10000000 loops, best of 3: 73 ns per loop
#
# In [12]: %timeit y=x*x
# 10000000 loops, best of 3: 58.3 ns per loop
#
# In [15]: z = np.uint8([5])
#
# In [17]: %timeit y=z*z
# 1000000 loops, best of 3: 1.25 us per loop
#
# In [19]: %timeit y=np.square(z)
# 1000000 loops, best of 3: 1.16 us per loop

# Step 3: Exit ipython with quit()


# NOTE
# Python scalar operations are faster than Numpy scalar operations.
# So for operations including one or two elements, Python scalar is better than Numpy arrays.
# Numpy takes advantage when size of array is a little bit bigger.


#  Performance Optimization Techniques
# 1. Avoid using loops in Python as far as possible, especially double/triple loops etc.
#    They are inherently slow.
# 2. Vectorize the algorithm/code to the maximum possible extent because Numpy and OpenCV
#    are optimized for vector operations.
# 3. Exploit the cache coherence.
# 4. Never make copies of array unless it is needed. Try to use views instead.
#    Array copying is a costly operation.
