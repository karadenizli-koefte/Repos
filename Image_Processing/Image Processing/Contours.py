import numpy as np
import cv2

imgPath = 'D:/img/lena_full.jpg'

# Load two images
img = cv2.imread(imgPath)
img = cv2.resize(img, (int(img.shape[1] / 4), int(img.shape[0] / 4)))

imgray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
ret, thresh = cv2.threshold(imgray, 127, 255, 0)
contours, hierarchy = cv2.findContours(thresh, cv2.RETR_TREE, cv2.CHAIN_APPROX_NONE)
# contours, hierarchy = cv2.findContours(thresh, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)

# Draw all contours
# img = cv2.drawContours(img, contours, -1, (0, 255, 0), 3)

# To draw an individual contour, say 4th contour
# img = cv2.drawContours(img, contours, 3, (0, 255, 0), 3)

# But most of the time, below method will be useful
cnt = contours[4]
img = cv2.drawContours(img, [cnt], 0, (0, 255, 0), 3)

cv2.namedWindow('res')
cv2.imshow('res', img)
cv2.waitKey()
cv2.destroyAllWindows()
