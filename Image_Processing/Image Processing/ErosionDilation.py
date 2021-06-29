import cv2
import numpy as np
from matplotlib import pyplot as plt

imgPath = 'D:/img/morph.png'

# Load two images
img = cv2.imread(imgPath)
img = cv2.resize(img, None, fx=5, fy=5, interpolation=cv2.INTER_CUBIC)

kernel = np.ones((31, 31), np.uint8)
erosion = cv2.erode(img, kernel, iterations=1)
dilation = cv2.dilate(img, kernel, iterations=1)
opening = cv2.morphologyEx(img, cv2.MORPH_OPEN, kernel)
closing = cv2.morphologyEx(img, cv2.MORPH_CLOSE, kernel)
gradient = cv2.morphologyEx(img, cv2.MORPH_GRADIENT, kernel)
tophat = cv2.morphologyEx(img, cv2.MORPH_TOPHAT, kernel)
blackhat = cv2.morphologyEx(img, cv2.MORPH_BLACKHAT, kernel)

plt.subplot(2, 4, 1), plt.imshow(img), plt.title('Original')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 4, 2), plt.imshow(erosion), plt.title('Erosion')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 4, 3), plt.imshow(dilation), plt.title('Dilation')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 4, 4), plt.imshow(opening), plt.title('Opening')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 4, 5), plt.imshow(closing), plt.title('Closing')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 4, 6), plt.imshow(gradient), plt.title('Gradient')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 4, 7), plt.imshow(tophat), plt.title('Top Hat')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 4, 8), plt.imshow(blackhat), plt.title('Black Hat')
plt.xticks([]), plt.yticks([])
plt.show()

# Structuring Element
# Rectangular Kernel
se1 = cv2.getStructuringElement(cv2.MORPH_RECT, (5, 5))

# Elliptical Kernel
se2 = cv2.getStructuringElement(cv2.MORPH_ELLIPSE, (5, 5))

# Cross-shaped Kernel
se3 = cv2.getStructuringElement(cv2.MORPH_CROSS, (5, 5))

print('se1 = \n' + str(se1))
print('se2 = \n' + str(se2))
print('se3 = \n' + str(se3))
