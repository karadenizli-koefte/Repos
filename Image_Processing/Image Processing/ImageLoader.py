import cv2


def load_img(path, scale):
    # Load image
    img = cv2.imread(path)
    img = cv2.resize(img, (int(img.shape[1] / scale), int(img.shape[0] / scale)))

    return img


def load_img_gray(path, scale):
    # Load image
    img = cv2.imread(path, 0)
    img = cv2.resize(img, (int(img.shape[1] / scale), int(img.shape[0] / scale)))

    return img
