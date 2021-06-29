import cv2
import numpy as np
from matplotlib import pyplot as plt

imgPath = 'D:/img/lena_full.jpg'

# Load two images
img = cv2.imread(imgPath)
img = cv2.resize(img, (int(img.shape[1] / 2), int(img.shape[0] / 2)))

edges = cv2.Canny(img, 100, 200)

plt.subplot(121), plt.imshow(img, cmap='gray')
plt.title('Original Image'), plt.xticks([]), plt.yticks([])
plt.subplot(122), plt.imshow(edges, cmap='gray')
plt.title('Edge Image'), plt.xticks([]), plt.yticks([])

plt.show()
