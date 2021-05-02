import cv2
import numpy as np
from matplotlib import pyplot as plt

counter = 1


def get_mouse_click_position(event, x, y, flags, params):
    global counter
    if event == cv2.EVENT_LBUTTONDBLCLK:
        print("Mouse position " + str(counter) + " = " + str((x, y)))
        counter += 1


def show(image):
    # Show gray scale image in window
    cv2.namedWindow('Lena', cv2.WINDOW_AUTOSIZE)
    cv2.setMouseCallback('Lena', get_mouse_click_position)
    cv2.imshow('Lena', image)
    cv2.waitKey(0)
    cv2.destroyAllWindows()


# Define image paths
imgPath = 'D:/img/lena_full.jpg'
imgSaveGray = 'D:/img/lena_full_gray.jpg'

img = cv2.imread(imgPath)

# Accessing distinct color pixels
print("Accessing distinct color pixels")
px = img[100, 100]
print("px = " + str(px))

blue = img[100, 100, 0]
print("Blue px = " + str(blue))
green = img[100, 100, 1]
print("Green px = " + str(green))
red = img[100, 100, 2]
print("Red px = " + str(red))
print()

# Modify pixel value
print("Modify pixel value")
print("img[100, 100] before = " + str(img[100, 100]))
img[100, 100] = [255, 255, 255]
print("img[100, 100] after = " + str(img[100, 100]))
print()

# WARNING
# Numpy is a optimized library for fast array calculations.
# So simply accessing each and every pixel values and modifying
# it will be very slow and it is discouraged.

# NOTE
# Above mentioned method is normally used for selecting a region of array,
# say first 5 rows and last 3 columns like that. For individual pixel access,
# Numpy array methods, array.item() and array.itemset() is considered to be better.
# But it always returns a scalar. So if you want to access all B,G,R values,
# you need to call array.item() separately for all.

print("Accessing and modifying RED value")
# accessing RED value
print("img.item(10, 10, 2) = " + str(img.item(10, 10, 2)))

# modifying RED value
print("img.itemset((10, 10, 2), 100) = " + str(img.itemset((10, 10, 2), 100)))
print("img.item(10, 10, 2) = " + str(img.item(10, 10, 2)))
print()

# Accessing image shape
print("Image Properties")
print("img.shape = " + str(img.shape))
print("img.size = " + str(img.size))
print("img.dtype = " + str(img.dtype))
print()

# NOTE - img.shape
# If image is grayscale, tuple returned contains only number of rows and columns.
# So it is a good method to check if loaded image is grayscale or color image.

# NOTE - img.dtype
# img.dtype is very important while debugging because a large number of errors in
# OpenCV-Python code is caused by invalid datatype.

# ROI
# ball = img[786:925, 953:1044]
# img[100:100 + 139, 273:273 + 91] = ball

# Splitting and Merging Image Channels
b, g, r = cv2.split(img)

print("Split b = " + str(b))
print("Split g = " + str(g))
print("Split r = " + str(r))

# cv2.namedWindow('Lena Blue', cv2.WINDOW_AUTOSIZE)
# cv2.imshow("Lena Blue", img[:, :, 0])
# cv2.waitKey(0)
# cv2.namedWindow('Lena Green', cv2.WINDOW_AUTOSIZE)
# cv2.imshow("Lena Green", img[:, :, 1])
# cv2.waitKey(0)
# cv2.namedWindow('Lena Red', cv2.WINDOW_AUTOSIZE)
# cv2.imshow("Lena Red", img[:, :, 2])
# cv2.waitKey(0)
# cv2.destroyAllWindows()

img = cv2.merge((b, g, r))

# WARNING
# cv2.split() is a costly operation (in terms of time), so only use it if necessary.
# Numpy indexing is much more efficient and should be used if possible.

# Subplots and borders
BLUE = [255, 0, 0]

img1 = cv2.imread(imgPath)

size = 10
replicate = cv2.copyMakeBorder(img1, size, size, size, size, cv2.BORDER_REPLICATE)
reflect = cv2.copyMakeBorder(img1, size, size, size, size, cv2.BORDER_REFLECT)
reflect101 = cv2.copyMakeBorder(img1, size, size, size, size, cv2.BORDER_REFLECT_101)
wrap = cv2.copyMakeBorder(img1, size, size, size, size, cv2.BORDER_WRAP)
constant = cv2.copyMakeBorder(img1, size, size, size, size, cv2.BORDER_CONSTANT, value=BLUE)

plt.subplot(231), plt.imshow(img1, 'gray'), plt.title('ORIGINAL')
plt.subplot(232), plt.imshow(replicate, 'gray'), plt.title('REPLICATE')
plt.subplot(233), plt.imshow(reflect, 'gray'), plt.title('REFLECT')
plt.subplot(234), plt.imshow(reflect101, 'gray'), plt.title('REFLECT_101')
plt.subplot(235), plt.imshow(wrap, 'gray'), plt.title('WRAP')
plt.subplot(236), plt.imshow(constant, 'gray'), plt.title('CONSTANT')

plt.show()
