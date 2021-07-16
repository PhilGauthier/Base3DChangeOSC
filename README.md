Base3DChangeOSC

Simple demo projet to show 3d base change of the coordinate system between Unity (left handed) and another right handed 3d engine which is more common in robotic (I used Touch Designer for demo). The change is defined with a 4x4 matrix.

![2021-07-16 17-51-33](https://user-images.githubusercontent.com/16133942/125975089-183f9fc6-e99d-4bed-aa99-787330a03774.gif)

It uses :
- OSC protocol ([ExtOSC package](https://github.com/Iam1337/extOSC)) for communication between Unity and TouchDesigner.
- [Unity.Mathematics](https://github.com/Unity-Technologies/Unity.Mathematics) for basic matrix computations
