import cv2
import numpy as np

imgPath = 'D:/img/Circles.png'

# Load two images
img = cv2.imread(imgPath, 0)

# Find contours
ret, thresh = cv2.threshold(img, 127, 255, 0)
contours, hierarchy = cv2.findContours(thresh, 1, 2)
cnt = contours[0]

# Aspect Ratio
# It is the ratio of width to height of bounding rect of the object.
x, y, w, h = cv2.boundingRect(cnt)
aspect_ratio = float(w) / h

print("Aspect Ratio = " + str(aspect_ratio))

# Extent
# Extent is the ratio of contour area to bounding rectangle area.
area = cv2.contourArea(cnt)
x, y, w, h = cv2.boundingRect(cnt)
rect_area = w * h
extent = float(area) / rect_area

print("Extent = " + str(extent))

# Solidity
# Solidity is the ratio of contour area to its convex hull area.
area = cv2.contourArea(cnt)
hull = cv2.convexHull(cnt)
hull_area = cv2.contourArea(hull)
solidity = float(area) / hull_area

print("Solidity = " + str(solidity))

# Equivalent Diameter
# Equivalent Diameter is the diameter of the circle whose area is same as the contour area.
area = cv2.contourArea(cnt)
equi_diameter = np.sqrt(4 * area / np.pi)

print("Equivalent Diameter = " + str(equi_diameter))

# Orientation
# Orientation is the angle at which object is directed. Following method also gives the Major Axis and Minor Axis
# lengths.
(x, y), (MA, ma), angle = cv2.fitEllipse(cnt)

print("Orientation:\n(x,y)=\t[" + str(x) + "|" + str(x) + "]\n" +
      "(MA,ma)=[" + str(MA) + "|" + str(ma) + "]\n" +
      "Angle=\t" + str(angle))

# Mask and Pixel Points
# In some cases, we may need all the points which comprises that object. It can be done as follows:
mask = np.zeros(img.shape, np.uint8)
cv2.drawContours(mask, [cnt], 0, 255, -1)
pixelpoints = np.transpose(np.nonzero(mask))
# pixelpoints = cv2.findNonZero(mask)

print('Pixel Points = ' + str(pixelpoints))

# Maximum Value, Minimum Value and their locations
min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(img, mask=mask)

print('Min Val = ' + str(min_val) + "\n" +
      'Max Val = ' + str(max_val) + "\n" +
      'Min Loc = ' + str(min_loc) + "\n" +
      'Max Loc = ' + str(max_loc))

# Mean Color or Mean Intensity
# Here, we can find the average color of an object. Or it can be average intensity of the object in grayscale mode.
# We again use the same mask to do it.

mean_val = cv2.mean(img, mask=mask)
print('Mean Val = ' + str(mean_val))

# Extreme Points
# Extreme Points means topmost, bottommost, rightmost and leftmost points of the object.
leftmost = tuple(cnt[cnt[:, :, 0].argmin()][0])
rightmost = tuple(cnt[cnt[:, :, 0].argmax()][0])
topmost = tuple(cnt[cnt[:, :, 1].argmin()][0])
bottommost = tuple(cnt[cnt[:, :, 1].argmax()][0])

print('leftmost = ' + str(leftmost))
print('rightmost = ' + str(rightmost))
print('topmost = ' + str(topmost))
print('bottommost = ' + str(bottommost))

# Check regionprops from MATLAB
