
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Me
{
	[Activity(Label = "LIstNew")]
	public class LIstNew : Activity
	{
		private ListView lv;
		List<Data> data = new List<Data>();
		private CustomAdapter adapter;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.ListActivity);
			lv = FindViewById<ListView>(Resource.Id.listView1);

			string text = Intent.GetStringExtra("fetch");
			this.GetNameHouse(text);

		}
		public void GetNameHouse(string name1)
		{
			data.Clear();
			DBAdapter db = new DBAdapter(this);
			db.openDB();

			ICursor c = db.RetrieveList(name1);
			if (c != null)
			{
				while (c.MoveToNext())
				{
					int i = c.GetInt(c.GetColumnIndex("id"));
					string name = c.GetString(c.GetColumnIndex("name"));
					string nameOwner = c.GetString(c.GetColumnIndex("namePerson"));
					string amount = c.GetString(c.GetColumnIndex("number"));
					string new_amount = "Rs." + " " + amount;
					//string dummy = name + nameOwner + amount;
					//nameHouse.Add(dummy); //need to add something
					//Collections.Sort(nameHouse);

					Data d = new Data()
					{
						Id = i,
						NameHouse = name,
						NamePerson = nameOwner,
						NameAmount = new_amount
					};
					data.Add(d);
				}
				lv = FindViewById<ListView>(Resource.Id.listView1);
				adapter = new CustomAdapter(this, data);
				lv.Adapter = adapter;

			}
			db.CloseDB();
			/*if (data.Count > 0)
			{
				namelist.Adapter = adapter;
			}*/
		}
	}
}
