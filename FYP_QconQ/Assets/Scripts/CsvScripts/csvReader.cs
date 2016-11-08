using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class csvReader 
{
    public Row FetchArcadeQtns( List<int> IDs)
    {
        if (IDs.Count > 1)
        {
            int rand = Random.Range(1, IDs.Count);
            currentQtn = Find_ID(IDs[rand].ToString());
        }
        else
        {
            currentQtn = Find_ID(IDs[1].ToString());
        }
        

        return currentQtn;
    }

    public Row FetchStageQtns(int ID , string category)
    {
        currentQtn = FindAll_Category(category)[ID];

        return currentQtn;
    }

    public int GetCategorySize(string category)
    {
        return FindAll_Category(category).Count;
    }

    public bool pressAns(int ans)
    {
        for (int i = 0; i < correctAns.Length; i++)
        {
            if (currentQtn.Answer[ans] == correctAns[i])
            {
                return true;
            }
        }

        return false;
    }

    string[] correctAns = new string[3];

    Row currentQtn;


    // From here ...
    public class Row
    {
        public string ID;
        public string Category;
        public string Question;
        public string[] Answer = new string[10];
        public string[] CorrectAns = new string[3];
    }

    List<Row> rowList = new List<Row>();
    bool isLoaded = false;

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.ID = grid[i][0];
            row.Category = grid[i][1];
            row.Question = grid[i][2];
            row.Answer[0] = grid[i][3];
            row.Answer[1] = grid[i][4];
            row.Answer[2] = grid[i][5];
            row.Answer[3] = grid[i][6];
            row.Answer[4] = grid[i][7];
            row.Answer[5] = grid[i][8];
            row.Answer[6] = grid[i][9];
            row.Answer[7] = grid[i][10];
            row.Answer[8] = grid[i][11];
            row.Answer[9] = grid[i][12];
            row.CorrectAns[0] = grid[i][13];
            row.CorrectAns[1] = grid[i][14];
            row.CorrectAns[2] = grid[i][15];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_ID(string find)
    {
        return rowList.Find(x => x.ID == find);
    }
    public List<Row> FindAll_ID(string find)
    {
        return rowList.FindAll(x => x.ID == find);
    }
    public Row Find_Category(string find)
    {
        return rowList.Find(x => x.Category == find);
    }
    public List<Row> FindAll_Category(string find)
    {
        return rowList.FindAll(x => x.Category == find);
    }
    public Row Find_Question(string find)
    {
        return rowList.Find(x => x.Question == find);
    }
    public List<Row> FindAll_Question(string find)
    {
        return rowList.FindAll(x => x.Question == find);
    }
    public Row Find_Answer1(string find)
    {
        return rowList.Find(x => x.Answer[0] == find);
    }
    public List<Row> FindAll_Answer1(string find)
    {
        return rowList.FindAll(x => x.Answer[0] == find);
    }
    public Row Find_Answer2(string find)
    {
        return rowList.Find(x => x.Answer[1] == find);
    }
    public List<Row> FindAll_Answer2(string find)
    {
        return rowList.FindAll(x => x.Answer[1] == find);
    }
    public Row Find_Answer3(string find)
    {
        return rowList.Find(x => x.Answer[2] == find);
    }
    public List<Row> FindAll_Answer3(string find)
    {
        return rowList.FindAll(x => x.Answer[2] == find);
    }
    public Row Find_Answer4(string find)
    {
        return rowList.Find(x => x.Answer[3] == find);
    }
    public List<Row> FindAll_Answer4(string find)
    {
        return rowList.FindAll(x => x.Answer[3] == find);
    }
    public Row Find_Answer5(string find)
    {
        return rowList.Find(x => x.Answer[4] == find);
    }
    public List<Row> FindAll_Answer5(string find)
    {
        return rowList.FindAll(x => x.Answer[4] == find);
    }
    public Row Find_Answer6(string find)
    {
        return rowList.Find(x => x.Answer[5] == find);
    }
    public List<Row> FindAll_Answer6(string find)
    {
        return rowList.FindAll(x => x.Answer[5] == find);
    }
    public Row Find_Answer7(string find)
    {
        return rowList.Find(x => x.Answer[6] == find);
    }
    public List<Row> FindAll_Answer7(string find)
    {
        return rowList.FindAll(x => x.Answer[6] == find);
    }
    public Row Find_Answer8(string find)
    {
        return rowList.Find(x => x.Answer[7] == find);
    }
    public List<Row> FindAll_Answer8(string find)
    {
        return rowList.FindAll(x => x.Answer[7] == find);
    }
    public Row Find_Answer9(string find)
    {
        return rowList.Find(x => x.Answer[8] == find);
    }
    public List<Row> FindAll_Answer9(string find)
    {
        return rowList.FindAll(x => x.Answer[8] == find);
    }
    public Row Find_Answer10(string find)
    {
        return rowList.Find(x => x.Answer[9] == find);
    }
    public List<Row> FindAll_Answer10(string find)
    {
        return rowList.FindAll(x => x.Answer[9] == find);
    }
    public Row Find_CorrectAns1(string find)
    {
        return rowList.Find(x => x.CorrectAns[0] == find);
    }
    public List<Row> FindAll_CorrectAns1(string find)
    {
        return rowList.FindAll(x => x.CorrectAns[0] == find);
    }
    public Row Find_CorrectAns2(string find)
    {
        return rowList.Find(x => x.CorrectAns[1] == find);
    }
    public List<Row> FindAll_CorrectAns2(string find)
    {
        return rowList.FindAll(x => x.CorrectAns[1] == find);
    }
    public Row Find_CorrectAns3(string find)
    {
        return rowList.Find(x => x.CorrectAns[2] == find);
    }
    public List<Row> FindAll_CorrectAns3(string find)
    {
        return rowList.FindAll(x => x.CorrectAns[2] == find);
    }
    // Till Here
}


//public List<string> FetchQtn()
//{
//    currentQtn = FindAll_Category(GameControl.handle.Category.ToString())[Random.Range(0, FindAll_Category(GameControl.handle.Category.ToString()).Count)];
//    QtnID = currentQtn.ID;

//    List<string> Data = new List<string>();
//    Data.Add(currentQtn.Question);
//    for (int i = 0; i < 10 ; i++)
//        Data.Add(currentQtn.Answer[i]);

//    for (int i = 0; i < correctAns.Length; i++)
//        correctAns[i] = currentQtn.CorrectAns[i];

//    return Data;
//}