import cv2
import numpy as np
from matplotlib import pyplot as plt

imgPath = 'D:/img/Lenna_Noisy.png'
img = cv2.imread(imgPath)
img = cv2.resize(img, (int(img.shape[1] / 2), int(img.shape[0] / 2)))

# 2D Convolution
kernel = np.ones((7, 7), np.float32) / 49
dst = cv2.filter2D(img, -1, kernel)

# plt.subplot(121), plt.imshow(img), plt.title('Original')
# plt.xticks([]), plt.yticks([])
# plt.subplot(122), plt.imshow(dst), plt.title('Averaging')
# plt.xticks([]), plt.yticks([])
# plt.show()

# Averaging / Gaussian Filtering / Median Filtering
blur = cv2.blur(img, (5, 5))  # Averaging
blur2 = cv2.GaussianBlur(img, (5, 5), 0)  # Gaussian Filtering
median = cv2.medianBlur(img, 5)
blur3 = cv2.bilateralFilter(img, 9, 75, 75)

plt.subplot(2, 3, 1), plt.imshow(img), plt.title('Original')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 3, 2), plt.imshow(blur2), plt.title('Blurred - Averaging')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 3, 3), plt.imshow(blur2), plt.title('Gaussian Filtering')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 3, 4), plt.imshow(median), plt.title('Median')
plt.xticks([]), plt.yticks([])
plt.subplot(2, 3, 5), plt.imshow(blur3), plt.title('Bilateral Filtering')
plt.xticks([]), plt.yticks([])
plt.show()
