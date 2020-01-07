
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2020_01_Puzzle.Device
{
    class CSVReader
    {
        private List<string[]> stringData;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CSVReader()
        {
            stringData = new List<string[]>();
        }

        public void Read(string filename, string path = "./")
        {
            Clear();
            //例外処理
            try
            {
                //csvファイルを開く
                using (var sr = new System.IO.StreamReader(@"Content/" + path + filename))
                {
                    //ストリームの末尾まで繰り返す
                    while (!sr.EndOfStream)
                    {
                        //1行読み込む
                        var line = sr.ReadLine();
                        //カンマごとに分けて配列に格納する
                        var values = line.Split(','); //文字のカンマ
                        //リストに読み込んだ1行を追加
                        stringData.Add(values);
#if DEBUG
                        //出力する
                        foreach (var v in values)
                        {
                            System.Console.Write("{0}", v);
                        }
                        System.Console.WriteLine();
#endif
                    }
                }
            }
            catch (System.Exception e)
            {
                //ファイルオープンが失敗したとき
                System.Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// リスト内をクリア
        /// </summary>
        public void Clear()
        {
            stringData.Clear();
        }

        public List<string[]> GetData()
        {
            return stringData;
        }
        public string[][] GetArrayData()
        {
            int count = stringData.Count;
            string[][] array = new string[count][];

            for (int i = 0; i < count; i++)
            {
                array[i] = stringData[i];
            }
            return array;
        }

        public int[][] GetIntData()
        {
            int count = stringData.Count;//Count List要素数
            int[][] array = new int[count][];

            for (int i = 0; i < count; i++)
            {
                int count2 = stringData[i].Length;//Length 配列の要素数
                array[i] = new int[count2];
                for (int j = 0; j < count2; j++)
                {
                    array[i][j] = int.Parse(stringData[i][j]);
                }
            }
            return array;
        }

        public string[,] GetStringMatrix()
        {
            var data = GetArrayData();   //元の2次元配列の取得
            int y = data.Count();　　　　//行の数
            int x = data[0].Count();　　 //列の数
            string[,] array = new string[y, x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    array[i, j] = data[i][j];
                }
            }
            return array;
        }

        public int[,] GetIntMatrix()
        {
            var data = GetIntData();   //元の2次元配列の取得
            int y = data.Count();　　　　//行の数
            int x = data[0].Count();　　 //列の数
            int[,] array = new int[y, x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    array[i, j] = data[i][j];
                }
            }
            return array;
        }
    }
}