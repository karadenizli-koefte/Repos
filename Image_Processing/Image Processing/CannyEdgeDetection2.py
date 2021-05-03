import cv2
import numpy as np
from matplotlib import pyplot as plt

imgPath = 'D:/img/lena_full.jpg'

# Load two images
img = cv2.imread(imgPath)
img = cv2.resize(img, (int(img.shape[1] / 2), int(img.shape[0] / 2)))


def nothing(x):
    pass


# Create a black image, a window
cv2.namedWindow('image')
cv2.namedWindow("image", cv2.WINDOW_AUTOSIZE);

# create trackbars for color change
cv2.createTrackbar('T1', 'image', 0, 255, nothing)
cv2.createTrackbar('T2', 'image', 0, 255, nothing)

while 1:
    # get current positions of four trackbars
    threshold1 = cv2.getTrackbarPos('T1', 'image')
    threshold2 = cv2.getTrackbarPos('T2', 'image')

    edges = cv2.Canny(img, threshold1, threshold2)
    cv2.imshow('image', edges)

    k = cv2.waitKey(1) & 0xFF

    if k == 27:
        break

cv2.destroyAllWindows()

