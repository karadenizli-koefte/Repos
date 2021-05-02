import cv2
import numpy as np

brush = 0
mouseX, mouseY = 0, 0
draw = False


def nothing(x):
    pass


# mouse callback function
def draw_circle(event, x, y, flags, param):
    global draw
    if event == cv2.EVENT_LBUTTONDOWN:
        draw = True
        cv2.circle(img, (x, y), brush, (b, g, r), -1)
    if event == cv2.EVENT_LBUTTONUP:
        draw = False
    if event == cv2.EVENT_MOUSEMOVE:
        if draw is True:
            cv2.circle(img, (x, y), brush, (b, g, r), -1)


# Create a black image, a window
img = np.zeros((600, 1024, 3), np.uint8)
cv2.namedWindow('image')

# Set mouse callback
cv2.setMouseCallback('image', draw_circle)

# create trackbars for color change
cv2.createTrackbar('R', 'image', 0, 255, nothing)
cv2.createTrackbar('G', 'image', 0, 255, nothing)
cv2.createTrackbar('B', 'image', 0, 255, nothing)

# create switch for ON/OFF functionality
switch = '0 : OFF \n1 : ON'
cv2.createTrackbar(switch, 'image', 0, 1, nothing)

cv2.createTrackbar('Brush', 'image', 0, 100, nothing)

while 1:
    cv2.imshow('image', img)
    k = cv2.waitKey(1) & 0xFF
    if k == 27:
        break

    # get current positions of four trackbars
    r = cv2.getTrackbarPos('R', 'image')
    g = cv2.getTrackbarPos('G', 'image')
    b = cv2.getTrackbarPos('B', 'image')
    s = cv2.getTrackbarPos(switch, 'image')
    brush = cv2.getTrackbarPos('Brush', 'image')

    # if s == 0:
    #     img[:] = 0
    # else:
    #     img[:] = [b, g, r]

cv2.destroyAllWindows()
