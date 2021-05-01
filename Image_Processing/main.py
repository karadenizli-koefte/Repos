import cv2
import numpy as np
from matplotlib import pyplot as plt

# Get OpenCV version
version = cv2.__version__
print(version)

# Define image paths
imgPath = 'D:/img/lena_full.jpg'
imgSaveGray = 'D:/img/lena_full_gray.jpg'

# Load an color image in grayscale
img = cv2.imread(imgPath, 0)
print(img.shape)

# Show gray scale image in window
cv2.namedWindow('image', cv2.WINDOW_AUTOSIZE)
cv2.imshow('image', img)
cv2.waitKey(0)
cv2.destroyAllWindows()

# Write gray scale image to disc.
cv2.imwrite(imgSaveGray, img)

# Plot image with matplotlib
img = cv2.imread(imgPath, 0)
plt.imshow(img, cmap='gray', interpolation='bicubic')
plt.xticks([]), plt.yticks([])  # to hide tick values on X and Y axis
plt.show()
