import matplotlib.pyplot as plt
import sklearn.datasets as data

digits = data.load_digits()
print(digits.data.shape)
print(digits.target)

# plt.gray()
# plt.matshow(digits.images[0])
# plt.show()

_, axes = plt.subplots(nrows=1, ncols=4, figsize=(10, 3))
for ax, image, label in zip(axes, digits.images, digits.target):
    ax.set_axis_off()
    ax.imshow(image, cmap=plt.cm.gray_r, interpolation='nearest')
    ax.set_title('Training: %i' % label)

plt.show()
