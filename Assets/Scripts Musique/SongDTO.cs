using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class SongDTO
{
    public String Name;
    public int BPM;
    public int IntervalsByBPM;
    public int[] MusicLine;
    public int[] RythmLine;

}

