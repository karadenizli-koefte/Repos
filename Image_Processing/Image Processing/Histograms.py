import cv2
import numpy as np
from matplotlib import pyplot as plt, figure

imgPath = 'D:/img/583Btn7K31R61.jpg'

# Load two images
img = cv2.imread(imgPath)
img = cv2.resize(img, (int(img.shape[1] / 2), int(img.shape[0] / 2)))

# Histogram with OpenCV
hist = cv2.calcHist([img], [0], None, [256], [0, 256])
print("Hist = " + str(hist))

# Histogram with Numpy
hist2, bins = np.histogram(img.ravel(), 256, [0, 256])
print("Hist2 = " + str(hist2))

# Plotting histograms with matplotlib

plt.subplots(1, 2)
fig = plt.gcf()
fig.set_size_inches(15, 5)
plt.subplot(1, 2, 1)
plt.hist(img.ravel(), 256, [0, 256])

plt.subplot(1, 2, 2)
color = ('b', 'g', 'r')
for i, col in enumerate(color):
    histr = cv2.calcHist([img], [i], None, [256], [0, 256])
    plt.plot(histr, color=col)
    plt.xlim([0, 256])
plt.show()

# Application of mask
# create a mask
mask = np.zeros(img.shape[:2], np.uint8)
mask[250:650, 250:550] = 255
masked_img = cv2.bitwise_and(img, img, mask=mask)

# Calculate histogram with mask and without mask
# Check third argument for mask
hist_full = cv2.calcHist([img], [0], None, [256], [0, 256])
hist_mask = cv2.calcHist([img], [0], mask, [256], [0, 256])

plt.subplot(221), plt.imshow(img, 'gray')
plt.subplot(222), plt.imshow(mask, 'gray')
plt.subplot(223), plt.imshow(masked_img, 'gray')
plt.subplot(224), plt.plot(hist_full), plt.plot(hist_mask)
plt.xlim([0, 256])

plt.show()
