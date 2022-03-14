# project-brain

Universal machine learning environment for robots with graphic visualization

![screen1](https://user-images.githubusercontent.com/37455393/158229883-b5376833-8485-4d61-85e0-78dea7974998.png)


Demo version of app for android - .apk file
Just send file to your phone and install.

Permissions for application:

-Communication(TCP Socket connection for learning on PC and send data to andorid phone)

-Storage


![screen2](https://user-images.githubusercontent.com/37455393/157963032-687795ee-a439-467d-87f9-943da3b088e2.png)


Software and packages you will need to run neural network and simulation:

For host with Unity Engine:
- Python (==3.7.8),
- Tensorflow (==2.6.0),
- Visual Studio 2019,
- Keras (==2.6.0),
- Opencv-Python (minimum version ==4.1.0.25),
- tqdm (==4.31.1),
- Unity Engine (2020.3.4f1),
- Unity ML Agents (==1.0.8 - important, different version doesnt work in this case),

For virtual environment (python venv):
- first, you will need to upgrade PIP to the newest version,
- Pytorch (==1.7.0) - this is little bit tricki, because we can already have pytorch on our venv, be carefull dont install 2 versions of torch,
- ML Agents,
- Pillow,
- Numpy, 
- tqdm. 
(try to install the same versions)

Optional (if we want to use our graphic card):
- NVIDIA cuDNN.
