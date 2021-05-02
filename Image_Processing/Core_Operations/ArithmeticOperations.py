import numpy as np
import cv2

imgPath = 'D:/img/lena_full.jpg'
imgPath2 = 'D:/img/583Btn7K31R61.jpg'
imgSaveGray = 'D:/img/lena_full_resized.jpg'
imgSaveGray = 'D:/img/test_resized.jpg'

# Image Addition

# NOTE
# There is a difference between OpenCV addition and Numpy addition.
# OpenCV addition is a saturated operation while Numpy addition is a modulo operation.

x = np.uint8([250])
y = np.uint8([10])

print(cv2.add(x, y))  # 250+10 = 260 => 255

print(x + y)  # 250+10 = 260 % 256 = 4

# Image Blending

img1 = cv2.imread(imgPath)
img2 = cv2.imread(imgPath2)

img1 = cv2.resize(img1, (768, 1024))
img2 = cv2.resize(img2, (768, 1024))

print(img1.shape)
print(img2.shape)
dst = cv2.addWeighted(img1, 0.3, img2, 0.7, 0)

cv2.imshow('dst', dst)
cv2.waitKey(0)
cv2.destroyAllWindows()
