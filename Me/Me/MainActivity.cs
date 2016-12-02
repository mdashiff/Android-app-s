using Android.App;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Java.Lang;
using Android.Database;
using Java.Util;
using Android.Support.V4.Widget;
using System;
using Android.Content;

namespace Me
{
	[Activity(Label = "Manage Me", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private Button mainButton;
		private ListView list;
		JavaList<Java.Lang.String> HouseList = new JavaList<Java.Lang.String>();
		private ArrayAdapter adapter;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			// Set our view from the "main" layout resource
			adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, HouseList);
			SetContentView(Resource.Layout.Main);
			mainButton = FindViewById<Button>(Resource.Id.main);
			list = FindViewById<ListView>(Resource.Id.main_listView);
			var swipeContainer = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout1);
			swipeContainer.SetColorSchemeResources(Android.Resource.Color.HoloBlueLight, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloOrangeLight, Android.Resource.Color.HoloRedLight);
			swipeContainer.Refresh += SwipeContainer_Refresh;
			this.GetSpaceCrafts();
			adapter.NotifyDataSetChanged();
			mainButton.Click += delegate
			{
				StartActivity(typeof(MainActivity1));
			};
			list.ItemClick += List_ItemClick;

		}

		void SwipeContainer_Refresh(object sender, EventArgs e)
		{
			(sender as SwipeRefreshLayout).Refreshing = false;
			adapter.NotifyDataSetChanged();
		}

		public void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			string s = (string)HouseList[e.Position];
			Intent new_activity = new Intent(this, typeof(LIstNew));
			new_activity.PutExtra("fetch",s);
			StartActivity(new_activity);
		}

		public void GetSpaceCrafts()
		{
			HouseList.Clear();
			DBAdapter db = new DBAdapter(this);
				db.openDB();
				ICursor c = db.Retrieve(); //SpaceCraft s = null;
			if (c != null)
			{
				while (c.MoveToNext())
				{
					string phone = c.GetString(c.GetColumnIndex("name")); 
					HouseList.Add(phone);
					Collections.Sort(HouseList);
				}
			}
			db.CloseDB();
			if (HouseList.Size() > 0)
			{
				list.Adapter = adapter;
			}

		}

		}
	}


