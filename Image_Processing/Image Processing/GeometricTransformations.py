import cv2
import numpy as np
from matplotlib import pyplot as plt

imgPath = 'D:/img/lena_full.jpg'
imgPath2 = 'D:/img/583Btn7K31R61.jpg'
imgSaveGray = 'D:/img/lena_full_resized.jpg'
imgSaveGray = 'D:/img/test_resized.jpg'

# Load two images
img = cv2.imread(imgPath)
img2 = cv2.imread(imgPath2)

img = cv2.resize(img, (int(img.shape[1] / 2), int(img.shape[0] / 2)))
img2 = cv2.resize(img2, (int(img2.shape[1] / 2), int(img2.shape[0] / 2)))

# Scaling
res = cv2.resize(img, None, fx=0.5, fy=0.5, interpolation=cv2.INTER_CUBIC)
# cv2.imshow('res', res)
# cv2.waitKey()

# OR

height, width = img.shape[:2]
res = cv2.resize(img, (int(0.5 * width), int(0.5 * height)), interpolation=cv2.INTER_CUBIC)
# cv2.imshow('res2', res)
# cv2.waitKey()
# cv2.destroyAllWindows()

# Translation
img2 = cv2.imread(imgPath2, 0)
img2 = cv2.resize(img2, (int(img2.shape[1] / 2), int(img2.shape[0] / 2)))
rows, cols = img2.shape

M = np.float32([[1, 0, 100], [0, 1, 50]])  # Define transformation matrix
dst = cv2.warpAffine(img2, M, (cols, rows))

# cv2.imshow('img', dst)
# cv2.waitKey(0)
# cv2.destroyAllWindows()

# Rotation
img2 = cv2.imread(imgPath2, 0)
img2 = cv2.resize(img2, (int(img2.shape[1] / 2), int(img2.shape[0] / 2)))
rows, cols = img2.shape

M = cv2.getRotationMatrix2D((cols / 4, rows / 4), 30, 0.5)
dst = cv2.warpAffine(img2, M, (cols, rows))

# cv2.imshow('img', dst)
# cv2.waitKey(0)
# cv2.destroyAllWindows()

# Affine Transformation
img2 = cv2.imread(imgPath2)
img2 = cv2.resize(img2, (int(img2.shape[1] / 2), int(img2.shape[0] / 2)))

pts1 = np.float32([[50, 50], [200, 50], [50, 200]])
pts2 = np.float32([[10, 100], [200, 50], [100, 250]])

M = cv2.getAffineTransform(pts1, pts2)

dst = cv2.warpAffine(img, M, (cols, rows))

# plt.subplot(121), plt.imshow(img), plt.title('Input')
# plt.subplot(122), plt.imshow(dst), plt.title('Output')
# plt.show()

# Perspective Transformation
img2 = cv2.imread(imgPath2)
img2 = cv2.resize(img2, (int(img2.shape[1] / 2), int(img2.shape[0] / 2)))

pts1 = np.float32([[56, 65], [368, 52], [28, 387], [389, 390]])
pts2 = np.float32([[0, 0], [300, 0], [0, 300], [300, 300]])

M = cv2.getPerspectiveTransform(pts1, pts2)

dst = cv2.warpPerspective(img, M, (300, 300))

plt.subplot(121), plt.imshow(img), plt.title('Input')
plt.subplot(122), plt.imshow(dst), plt.title('Output')
plt.show()
