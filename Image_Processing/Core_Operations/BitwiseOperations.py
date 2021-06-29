import numpy as np
import cv2

imgPath = 'D:/img/lena_full.jpg'
imgPath2 = 'D:/img/583Btn7K31R61.jpg'
imgSaveGray = 'D:/img/lena_full_resized.jpg'
imgSaveGray = 'D:/img/test_resized.jpg'

# Load two images
img1 = cv2.imread(imgPath)
img2 = cv2.imread(imgPath2)

img1 = cv2.resize(img1, (int(img1.shape[1]/2), int(img1.shape[0]/2)))
img2 = cv2.resize(img2, (int(img1.shape[1]/6), int(img1.shape[0]/6)))

# I want to put logo on top-left corner, So I create a ROI
rows, cols, channels = img2.shape
roi = img1[0:rows, 0:cols]

# Now create a mask of logo and create its inverse mask also
img2gray = cv2.cvtColor(img2, cv2.COLOR_BGR2GRAY)
img2gray0 = img2[:, :, 0]
img2gray1 = img2[:, :, 1]
img2gray2 = img2[:, :, 2]

ret, mask = cv2.threshold(img2gray, 30, 255, cv2.THRESH_BINARY)
mask_inv = cv2.bitwise_not(mask)

# Now black-out the area of logo in ROI
img1_bg = cv2.bitwise_and(roi, roi, mask=mask_inv)

# Take only region of logo from logo image.
img2_fg = cv2.bitwise_and(img2, img2, mask=mask)

# Put logo in ROI and modify the main image
dst = cv2.add(img1_bg, img2_fg)
img1[0:rows, 0:cols] = dst

cv2.imshow('res', img1)
cv2.waitKey(0)
cv2.destroyAllWindows()
