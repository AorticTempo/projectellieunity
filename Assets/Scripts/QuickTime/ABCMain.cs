using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using ABC;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class ABCMain
{
    private FileStream file;
    private Tune tune;
    private List<ABC.Item> list = new List<ABC.Item>();
    //private int tempo = 60;
    public int tempo = 60;
    public Queue<Tuple<string, float>> QTE;


    private void buildQTE(List<ABC.Item> items)
    {
        QTE = new Queue<Tuple<string, float>>();
        foreach (var item in items)
        {
            // build events here
            if (item is Note note)
            {
                float length = note.duration;
                string key = note.pitch.ToString();
                Tuple<string, float> parsednote = new Tuple<string, float>(key, length);
                QTE.Enqueue(parsednote);
            }
            if (item is Rest rest)
            {
                float length = rest.duration;
                Tuple<string, float> parsednote = new Tuple<string, float>("R", length);
                QTE.Enqueue(parsednote);
            }
        }
    }

    public Tune GetSong(string path)
    {
        if (!File.Exists(path))
        {
            Application.Quit();
            throw new UnityException(path + " Does not exist.");
        }
        else
        {
            file = File.OpenRead(path);
            try
            {
                Parser parser = new Parser();
                Tune tune = parser.Parse(file);
                this.tune = tune;
                // GET TUNE HEADER NOTE LENGTH
                if (tune.tempo != null)
                {
                    tempo = (int)tune.tempo;
                }
                else
                {
                    tempo = 120;
                }
                foreach (var voice in tune.voices)
                {
                    foreach (var item in voice.items)
                    {
                        list.Add(item);
                    }
                }
                buildQTE(list);
                return tune;
            }
            catch (FileLoadException e)
            {
                throw e;
            }
            finally
            {
                file.Close();
            }
        }

    }
}