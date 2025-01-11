using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.Splines;
using ABC;
using UnityEditor.SearchService;
using AudioSynthesis.Synthesis;
using System;
using Unity.VisualScripting;
using UnityEngine.Splines;
using System.Linq;
using UnityEditor;

/// <summary>
/// Author: AorticTempo
/// Description: this uses a way
/// </summary>
public class GuitarHeroKnockoff : MonoBehaviour
{
    public GameObject loE;
    public GameObject A;
    public GameObject D;
    public GameObject G;
    public GameObject B;
    public GameObject hiE;
    private Tune tune;
    public int tempo;
    private int fixedupdatecount;
    private Tuple<string, float> note;
    private float duration = 0f;
    private string[,] fretboard = { 
        {"E4","F4","G4","A4","B4","C5","D5","E5","F5","G5","A5","B5","C6","D6","E6"}, 
        {"B3","C4","D4","E4","F4","G4","A4","B4","C5","D5","E5","F5","G5","A5","B5"}, 
        {"G3","A3","B3","C4","D4","E4","F4","G4","A4","B4","C5","D5","E5","F5","G5"}, 
        {"D3","E3","F3","G3","A3","B3","C4","D4","E4","F4","G4","A4","B4","C5","D5"}, 
        {"A2","B2","C3","D3","E3","F3","G3","A3","B3","C4","D4","E4","F4","G4","A4"}, 
        {"E2","F2","G2","A2","B2","C3","D3","E3","F3","G3","A3","B3","C4","D4","E4"} }; //breadth first search
    private Tuple<int, int> lastFret;
    private ABCMain aBCMain;

    // Start is called before the first frame update
    void Start()
    {
        aBCMain = new ABCMain();
        tune = aBCMain.GetSong(EditorUtility.OpenFilePanel("Select ABC Notation File", "Assets/Music/", "abc"));
        tempo = aBCMain.tempo;
        fixedupdatecount = 0;
        note = aBCMain.QTE.Dequeue();
        duration = note.Item2;
        playNextNote(note.Item1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (aBCMain.QTE.Count != 0)
        {
            ++fixedupdatecount;
            if ((fixedupdatecount / 50) >= ((duration*4)/(tempo/60)))
            {
                note = aBCMain.QTE.Dequeue();
                if (note.Item1 == "R")
                {
                    duration = note.Item2;
                    fixedupdatecount = 0;
                }
                else
                {
                    duration = note.Item2;
                    playNextNote(note.Item1);
                    fixedupdatecount = 0;
                }
            }
        }
    }

    private void playNextNote(string note)
    {
        GameObject go;
        bool[,] vis = new bool[6, 15];
        if (lastFret == null)
        {
            lastFret = GfG.BFS(fretboard, vis, note);
        }
        else
        {
            lastFret = GfG.BFS(fretboard, vis, note, lastFret.Item1, lastFret.Item2);
        }
        switch (lastFret.Item1)
        {
            case 0:
                go = Instantiate(hiE, hiE.transform.parent);
                go.GetComponent<SplineAnimate>().Play();
                Destroy(go, (tempo / 60));
                break;
            case 1:
                go = Instantiate(B, B.transform.parent);
                go.GetComponent<SplineAnimate>().Play();
                Destroy(go, (tempo / 60));
                break;
            case 2:
                go = Instantiate(G, G.transform.parent);
                go.GetComponent<SplineAnimate>().Play();
                Destroy(go, (tempo / 60));
                break;
            case 3:
                go = Instantiate(D, D.transform.parent);
                go.GetComponent<SplineAnimate>().Play();
                Destroy(go, (tempo / 60));
                break;
            case 4:
                go = Instantiate(A, A.transform.parent);
                go.GetComponent<SplineAnimate>().Play();
                Destroy(go, (tempo / 60));
                break;
            case 5:
                go = Instantiate(loE, loE.transform.parent);
                go.GetComponent<SplineAnimate>().Play();
                Destroy(go, (tempo / 60));
                break;
            default:
                break;
        }
    }
}

