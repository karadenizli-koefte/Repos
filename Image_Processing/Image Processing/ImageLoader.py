import cv2


def load_img(path, scale):
    # Load image
    img = cv2.imread(path)
    img = cv2.resize(img, (int(img.shape[1] / scale), int(img.shape[0] / scale)))

    return img
