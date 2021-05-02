import cv2
import numpy as np

# Show OpenCV flags for color spaces
flags = [i for i in dir(cv2) if i.startswith('COLOR_')]
print(flags)

# NOTE
# For HSV, Hue range is [0,179], Saturation range is [0,255] and Value range is [0,255]. Different
# softwares use different scales. So if you are comparing OpenCV values with them, you need to normalize
# these ranges.

imgPath = 'D:/img/lena_full.jpg'
imgPath2 = 'D:/img/583Btn7K31R61.jpg'
imgSaveGray = 'D:/img/lena_full_resized.jpg'
imgSaveGray = 'D:/img/test_resized.jpg'

# Load two images
img1 = cv2.imread(imgPath)
img2 = cv2.imread(imgPath2)

img1 = cv2.resize(img1, (int(img1.shape[1] / 2), int(img1.shape[0] / 2)))
img2 = cv2.resize(img2, (int(img2.shape[1] / 2), int(img2.shape[0] / 2)))

# define range of blue color in HSV
lower_blue = np.array([int(80 / 100 * 255), 50, 50])
upper_blue = np.array([int(100 / 100 * 255), 255, 255])
lower_blue = np.array([0, 40, 80])
upper_blue = np.array([30, 140, 255])

green = np.uint8([[[255, 255, 0]]])
hsv_green = cv2.cvtColor(green, cv2.COLOR_BGR2HSV)
print(hsv_green)

# cv2.imshow('res', img2)
# cv2.waitKey()
hsv = cv2.cvtColor(img2, cv2.COLOR_BGR2HSV)
# cv2.imshow('res2', hsv)
# cv2.waitKey()
mask = cv2.inRange(hsv, lower_blue, upper_blue)
# cv2.imshow('res3', mask)
# cv2.waitKey()
res = cv2.bitwise_and(img2, img2, mask=mask)
cv2.imshow('res4', res)
cv2.waitKey()

cv2.destroyAllWindows()
