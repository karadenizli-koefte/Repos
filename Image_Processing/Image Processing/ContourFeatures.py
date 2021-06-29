import cv2
import numpy as np

imgPath = 'D:/img/Circles.png'

# Load two images
img = cv2.imread(imgPath, 0)

cv2.namedWindow('res')

# Get moments
ret, thresh = cv2.threshold(img, 127, 255, 0)
contours, hierarchy = cv2.findContours(thresh, 1, 2)

cnt = contours[0]
M = cv2.moments(cnt)
print(M)

# Get centroids of moment
# cx = int(M['m10']/M['m00'])
# cy = int(M['m01']/M['m00'])

# Contour area
area = cv2.contourArea(cnt)
print(area)

# Contour perimeter
perimeter = cv2.arcLength(cnt, True)
print(perimeter)

# Contour Approximation
epsilon = 0.1 * cv2.arcLength(cnt, True)
approx = cv2.approxPolyDP(cnt, epsilon, True)

# Convex Hull
hull = cv2.convexHull(cnt)

# Checking Convexity
k = cv2.isContourConvex(cnt)

img = cv2.imread(imgPath)

# Bounding Rectangle

# Straight Bounding Rectangle
x, y, w, h = cv2.boundingRect(cnt)
im = cv2.rectangle(img, (x, y), (x + w, y + h), (0, 255, 0), 2)

cv2.imshow('res', im)
cv2.waitKey()
img = cv2.imread(imgPath)

# Rotated Bounding Rectangle
rect = cv2.minAreaRect(cnt)
box = cv2.boxPoints(rect)
box = np.int0(box)
im = cv2.drawContours(img, [box], 0, (0, 0, 255), 2)

cv2.imshow('res', im)
cv2.waitKey()
img = cv2.imread(imgPath)

# Minimum Enclosing Circle
(x, y), radius = cv2.minEnclosingCircle(cnt)
center = (int(x), int(y))
radius = int(radius)
im = cv2.circle(img, center, radius, (0, 255, 0), 2)

cv2.imshow('res', im)
cv2.waitKey()
img = cv2.imread(imgPath)

# Fitting an Ellipse
ellipse = cv2.fitEllipse(cnt)
im = cv2.ellipse(img, ellipse, (0, 255, 0), 2)

cv2.imshow('res', im)
cv2.waitKey()
img = cv2.imread(imgPath)

# Fitting a Line
rows, cols = img.shape[:2]
[vx, vy, x, y] = cv2.fitLine(cnt, cv2.DIST_L2, 0, 0.01, 0.01)
lefty = int((-x * vy / vx) + y)
righty = int(((cols - x) * vy / vx) + y)
im = cv2.line(img, (cols - 1, righty), (0, lefty), (0, 255, 0), 2)

cv2.imshow('res', im)
cv2.waitKey()
cv2.destroyAllWindows()
