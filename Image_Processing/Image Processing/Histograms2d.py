import cv2
import numpy as np
from matplotlib import pyplot as plt
import ImageLoader as il

img = il.load_img('D:/img/583Btn7K31R61.jpg', 2)

# OpenCV
hsv = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)
hist = cv2.calcHist([hsv], [0, 1], None, [180, 256], [0, 180, 0, 256])

h = hsv[:, :, 0]
s = hsv[:, :, 1]
v = hsv[:, :, 2]

# Numpy
hist, xbins, ybins = np.histogram2d(h.ravel(), s.ravel(), [180, 256], [[0, 180], [0, 256]])

hist = cv2.calcHist( [hsv], [0, 1], None, [180, 256], [0, 180, 0, 256])
plt.imshow(hist, interpolation='nearest')
plt.show()
