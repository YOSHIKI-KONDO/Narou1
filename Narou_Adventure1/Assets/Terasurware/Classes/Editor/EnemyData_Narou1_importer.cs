using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class EnemyData_Narou1_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Scripts/VolatilityGraph/EnemyData_Narou1.xls";
	private static readonly string exportPath = "Assets/Scripts/VolatilityGraph/EnemyData_Narou1.asset";
	private static readonly string[] sheetNames = { "enemy_t", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_enemy_t data = (Entity_enemy_t)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_enemy_t));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_enemy_t> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_enemy_t.Sheet s = new Entity_enemy_t.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_enemy_t.Param p = new Entity_enemy_t.Param ();
						
					cell = row.GetCell(0); p.Lv = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.EdgeOfTown = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.SmallHill = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.Plain = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.LostForest = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.OakForest = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.moor = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.hoardingHouse = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.sewer = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.bog = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.demonic_cellar = (cell == null ? 0.0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
