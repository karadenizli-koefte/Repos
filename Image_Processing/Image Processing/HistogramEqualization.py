import cv2
import numpy as np
from matplotlib import pyplot as plt
import ImageLoader as il

img = il.load_img('D:/img/583Btn7K31R61.jpg', 2)
hist, bins = np.histogram(img.flatten(), 256, [0, 256])

# Cumulative histogram
cdf = hist.cumsum()
cdf_normalized = cdf * hist.max() / cdf.max()

cdf_m = np.ma.masked_equal(cdf, 0)
cdf_m = (cdf_m - cdf_m.min()) * 255 / (cdf_m.max() - cdf_m.min())
cdf2 = np.ma.filled(cdf_m, 0).astype('uint8')
hist_max = hist.max()
cdf_max = cdf2.max()
diff = hist_max / cdf_max
cdf_normalized2 = cdf2 * diff
img2 = cdf2[img]

plt.subplots(1, 2)
plt.subplot(1, 2, 1)
plt.imshow(img)
plt.subplot(1, 2, 2)
plt.plot(cdf_normalized, color='b')
plt.hist(img.flatten(), 256, [0, 256], color='r')
plt.xlim([0, 256])
plt.legend(('cdf', 'histogram'), loc='upper left')
plt.show()
cv2.waitKey()

plt.subplots(1, 2)
plt.subplot(1, 2, 1)
plt.imshow(img2)
plt.subplot(1, 2, 2)
plt.plot(cdf_normalized2, color='b')
plt.hist(img2.flatten(), 256, [0, 256], color='r')
plt.xlim([0, 256])
plt.legend(('cdf', 'histogram'), loc='upper left')
plt.show()
