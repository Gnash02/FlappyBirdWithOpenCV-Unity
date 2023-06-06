
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class FaceDetector : MonoBehaviour
{
    // Start is called before the first frame update

    public WebCamTexture _webCamTexture;
    CascadeClassifier cascade;
    OpenCvSharp.Rect PlayerFace;
    public float faceY;
    void Start()
    {
        //WebCamDevice[] devices=WebCamTexture.devices;
        _webCamTexture=new WebCamTexture();
        _webCamTexture.Play();
        Debug.Log(_webCamTexture.isPlaying);
        cascade = new CascadeClassifier(Application.dataPath+@"/OpenCV+Unity/Demo/Face_Detector/haarcascade_frontalface_default.xml");
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Renderer>().material.mainTexture=_webCamTexture;
        Mat frame=OpenCvSharp.Unity.TextureToMat(_webCamTexture);
        findNewFace(frame);
        display(frame);
    }
    void findNewFace(Mat frame){
        var Faces=cascade.DetectMultiScale(frame,1.1,2,HaarDetectionType.ScaleImage);
        if(Faces.Length>=1){
            Debug.Log(Faces[0].Location);
            PlayerFace=Faces[0];
            faceY=Faces[0].Y;
        }
    }
    void display(Mat frame){
        if(PlayerFace!=null){

            frame.Rectangle(PlayerFace,new Scalar(0,0,255),2);
        }
        Texture NewTexture =OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture=NewTexture;

    }
}
