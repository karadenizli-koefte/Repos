# Kaggle for datasets
import tensorflow as tf
layers = tf.keras.layers

# Data preparation
(x_train, y_train), (x_test, y_test) = tf.keras.datasets.mnist.load_data()
print(x_train)
print(y_train)
#x = Data -> KI -> Result = y

Schwarz = 0
Weiß = 255
RGB = (255, 0, 0) # Rot

x_train = x_train / 255.
x_test = x_test / 255.

# Model
model = tf.keras.models.Sequential([
    layers.Flatten(input_shape=(28, 28)),
    layers.Dense(256, activation='relu'),
    layers.Dense(128, activation='relu'),
    layers.Dense(64, activation='relu'),
    layers.Dense(10, activation='softmax')
])

# Convolutional Neural Network
model = tf.keras.models.Sequential([#28x28
    layers.Reshape((28, 28, 1)),
    layers.Convolution2D(32, 3, activation='relu'),
    layers.Convolution2D(32, 3, activation='relu'),
    layers.Flatten(),
    layers.Dense(128, activation='relu'),
    layers.Dense(64, activation='relu'),
    layers.Dense(10, activation='softmax')
])

"Dense / Fully Connected Layers"
#[0,1,0]

# Training parameters
model.compile(
    optimizer='rmsprop',
    loss='sparse_categorical_crossentropy',
    metrics=['accuracy']
)

# Train
model.fit(x_train, y_train, epochs=10)

# Predict
model.evaluate(x_test, y_test)

import matplotlib.pyplot as plt
import numpy as np

example_image = x_test[:1] #1 [ Reihe [ Pixel ]]
prediction = model(example_image)
plt.imshow(np.reshape(x_test[0], [28, 28]), cmap='gray')
plt.show()
print("Prediction: ", np.argmax(prediction.numpy()))
