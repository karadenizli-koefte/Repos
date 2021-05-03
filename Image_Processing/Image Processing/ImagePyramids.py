import cv2
import numpy as np, sys

# A = cv2.imread('apple.jpg')
# B = cv2.imread('orange.jpg')

imgPath = 'D:/img/lena_full.jpg'
imgPath2 = 'D:/img/583Btn7K31R61.jpg'

# Load two images
img = cv2.imread(imgPath)
img2 = cv2.imread(imgPath2)

A = cv2.resize(img, (1024, 1024))
B = cv2.resize(img2, (1024, 1024))

# generate Gaussian pyramid for A
G = A.copy()
gpA = [G]
for i in range(6):
    G = cv2.pyrDown(G)
    gpA.append(G)

# generate Gaussian pyramid for B
G = B.copy()
gpB = [G]
for i in range(6):
    G = cv2.pyrDown(G)
    gpB.append(G)

# generate Laplacian Pyramid for A
lpA = [gpA[5]]
for i in range(5, 0, -1):
    GE = cv2.pyrUp(gpA[i])
    L = cv2.subtract(gpA[i - 1], GE)
    lpA.append(L)

# generate Laplacian Pyramid for B
lpB = [gpB[5]]
for i in range(5, 0, -1):
    GE = cv2.pyrUp(gpB[i])
    L = cv2.subtract(gpB[i - 1], GE)
    lpB.append(L)

# Now add left and right halves of images in each level
LS = []
for la, lb in zip(lpA, lpB):
    rows, cols, dpt = la.shape
    x1 = cols//2
    a = la[:, 0:x1]
    b = lb[:, x1:]
    ls = np.hstack((a, b))
    LS.append(ls)

# now reconstruct
ls_ = LS[0]
for i in range(1, 6):
    ls_ = cv2.pyrUp(ls_)
    ls_ = cv2.add(ls_, LS[i])

# image with direct connecting each half
real = np.hstack((A[:, :cols // 2], B[:, cols // 2:]))

cv2.imwrite('Pyramid_blending2.jpg', ls_)
cv2.imwrite('Direct_blending.jpg', real)
