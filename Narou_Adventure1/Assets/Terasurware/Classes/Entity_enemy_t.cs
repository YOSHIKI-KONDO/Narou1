using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_enemy_t : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int Lv;
		public double EdgeOfTown;
		public double SmallHill;
		public double Plain;
		public double LostForest;
		public double OakForest;
		public double moor;
		public double hoardingHouse;
		public double sewer;
		public double bog;
		public double demonic_cellar;
	}
}

