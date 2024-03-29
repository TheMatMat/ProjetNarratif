// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TeamSeven
{

	public class DialogueTable
	{
		[System.Serializable]
		public struct Row
		{
			public string ID;
			public string FR;
			public string EN;


			public Row(string _first)
			{
				ID = "custom";
				FR = _first;
				EN = "";
			}

			public Row(string _fr, string _en)
			{
				ID = "custom";
				FR = _fr;
				EN = _en;
			}
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
				row.FR = grid[i][1];
				row.EN = grid[i][2];

				rowList.Add(row);
			}
			isLoaded = true;
		}

		public int NumRows()
		{
			return rowList.Count;
		}

		public bool GetAt(int i, out Row value)
		{
			if (rowList.Count <= i)
			{
				value = new Row();
				return false;
			}
			value = rowList[i];
			return true;
		}

		public Row Find_ID(string find)
		{
			return rowList.Find(x => x.ID == find);
		}
		public List<Row> FindAll_ID(string find)
		{
			return rowList.FindAll(x => x.ID == find);
		}
		public Row Find_FR(string find)
		{
			return rowList.Find(x => x.FR == find);
		}
		public List<Row> FindAll_FR(string find)
		{
			return rowList.FindAll(x => x.FR == find);
		}
		public Row Find_EN(string find)
		{
			return rowList.Find(x => x.EN == find);
		}
		public List<Row> FindAll_EN(string find)
		{
			return rowList.FindAll(x => x.EN == find);
		}

	}
}