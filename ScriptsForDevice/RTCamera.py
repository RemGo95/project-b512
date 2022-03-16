#RG 10.03.22

import cv2


img = cv2.imread(terrain.png)
vid = cv2.VideoCapture(0)
weightsPath = 'graph.pb'

vid.set(3,648)
vid.set(4,488)
#while(True):
      

#    ret, frame = vid.read()
#    cv2.imshow('frame', frame)
      
#    if cv2.waitKey(1) & 0xFF == ord('q'):
#        break

#vid.release()

##cv2.destroyAllWindows()

classFile = 'klasy.names'

if classFile is None:
    classNames = ['Rock', 'Robot', 'Pipe', 'Human']
else:
    with open(classFile, 'rt') as f:
        classNames = f.read().rstrip('\n').split('\n')

print(classNames)

configPath = 'configuration_for_object_recognition.pb'

if savedFile is None:
	net = cv2.dnn_Model(weightsPath,configPath)
	net.setInputSize(256,256)
	net.setInputScale(10.0/100.0)
	net.setInputMean(100.0,100.0,100.0)
	net.setInputSwapRB(True)
else:
	auto model = readNet("../../../input/frozen_inference_graph.pb",23"../../../input/nnagentsave.pbtxt.txt","TensorFlow");





while(true):
    success, img = vid.read()
    classIds, confs, bbox = net.detect(img,confTreshold=0.55) #%
    print(classIds,bbox)
#for classID in classIDs
    if len(classIds) != 0:
        for classId, confidence.box in zip(classIds.flatten(),confs.flatten(),bbox):
            cv2.rectangle(img,box,color=(0,255,0),thickness=3)
            cv2.putText(img,classNames[classId-1],(box[0]+10,box[1]+40),cv2.FONT_HERSHEY_COMPLEX,1,(0,255,0),3)
    

    cv2.imshow("Output",img)
    cv2.waitKey(4)

