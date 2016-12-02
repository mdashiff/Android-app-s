
using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Database;
using Android.Support.V4.App;
using Android.Media;

namespace Me
{
	[Activity(Label = "Main Page")]
	public class MainActivity1 : Activity
	{
		private EditText getHomename;
		private EditText name_Person;
		private EditText number_amount;
		private Button addlist;
		private ListView namelist;
		private static readonly int LocalNotification1 = 9999;
		List<Data> data = new List<Data>();
		//private ArrayAdapter adapter;
		private CustomAdapter adapter;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main2);
			// Create your application here
			//adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, nameHouse);
			getHomename = FindViewById<EditText>(Resource.Id.textNameHouse);
			name_Person = FindViewById<EditText>(Resource.Id.Name);
			number_amount = FindViewById<EditText>(Resource.Id.amount);
			namelist = FindViewById<ListView>(Resource.Id.namelist);
			addlist = FindViewById<Button>(Resource.Id.button1);
			this.GetNameHouse();
			adapter.NotifyDataSetChanged();
			namelist.ItemClick += Namelist_ItemClick;
			namelist.ItemLongClick += Namelist_ItemLongClick;
			addlist.Click += delegate
			{
				String s1 = getHomename.Text;
				String s2 = name_Person.Text;
				String s3 = number_amount.Text;
				if (!String.IsNullOrEmpty(s1) && !String.IsNullOrEmpty(s2) && !String.IsNullOrEmpty(s3))
				{

					Save(s1, s2, s3);
				}
				else
				{
					Toast.MakeText(this, "Fields can't be Empty.", ToastLength.Short).Show();
				}
				//adapter.NotifyDataSetChanged();

			};
		}

		private void Namelist_ItemClick(object sender, AdapterView.ItemClickEventArgs e) 
		{
			var s1 = data[e.Position].NamePerson;
			LocalPushNotification(s1);
			var amount_status = new AlertDialog.Builder(this);
			amount_status.SetMessage("Is amount Paid by" + " " + s1);
			amount_status.SetNeutralButton("Paid", delegate
			{
				Toast.MakeText(this, " Updated Successfully ", ToastLength.Long).Show();
			});
			amount_status.SetNegativeButton("Cancel", delegate { });
			amount_status.Show();
		}

		private void Namelist_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
		{
			String s1 = data[e.Position].NamePerson;
			LocalPushNotification(s1);
			var delete = new AlertDialog.Builder(this);
			delete.SetMessage("Do you want to delete" + " " + s1);
			delete.SetNeutralButton("Yes", delegate
			{
				DBAdapter db = new DBAdapter(this);
				db.openDB();
				int i = db.Delete(s1);
				if (i == 1)
				{
					Toast.MakeText(this, "Entry Deleted", ToastLength.Short).Show();
					adapter.NotifyDataSetChanged();
					this.GetNameHouse();
				}
				else
				{
					Toast.MakeText(this, "Error! Please Try again", ToastLength.Long).Show();
				}

			});
			delete.SetNegativeButton("No", delegate { });
			delete.Show();

		}

		public void Save(String name, String namePerson, String numberAmount)
		{
			DBAdapter db = new DBAdapter(this);
			db.openDB();
			bool saved = db.Add(name, namePerson, numberAmount); //modified Sonething here
			db.CloseDB();
			if (saved)//modified
			{
				getHomename.Text = "";
				name_Person.Text = "";
				number_amount.Text = "";
				Toast.MakeText(this, "Details Saved", ToastLength.Short).Show();
			}
			else
			{
				Toast.MakeText(this, "Unable to Save, Please Try Again", ToastLength.Short).Show();
			}
			this.GetNameHouse();

		}
		public void GetNameHouse()
		{
			data.Clear();
			DBAdapter db = new DBAdapter(this);
			db.openDB();

			ICursor c = db.GetNames();
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
				var k1 = FindViewById<ListView>(Resource.Id.namelist);
				adapter = new CustomAdapter(this, data);
				k1.Adapter = adapter;

			}
			db.CloseDB();
			/*if (data.Count > 0)
			{
				namelist.Adapter = adapter;
			}*/
		}

		private void LocalPushNotification(String name)
		{
			//String local_name = getName;
			String chumma = name;
			Bundle valueSend = new Bundle();
			valueSend.PutString("sendContent", "Something has to be done");

			Intent newIntent = new Intent(this, typeof(SecondActivity));
			newIntent.PutExtras(valueSend);

			Android.Support.V4.App.TaskStackBuilder stackBuilder = Android.Support.V4.App.TaskStackBuilder.Create(this);
			stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(SecondActivity)));
			stackBuilder.AddNextIntent(newIntent);

			PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);
			NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
				.SetAutoCancel(true)
				.SetContentIntent(resultPendingIntent)
				.SetContentTitle("Remainding You!!!")
				.SetSmallIcon(Resource.Drawable.ic_stat_button_click)
				.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
				.SetContentText("You have to Receive payment from"+" "+chumma);
			

			NotificationManager notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(LocalNotification1, builder.Build());
			//Implementing the Alarm Service from Here
			String message = "You have some task to look after";
			DateTime now = DateTime.Now.ToLocalTime();
			RemaindingService(now,chumma, message);

		}
		//Implementing the alarmService Method
		private void RemaindingService(DateTime date,String  name, String message)
		{
			Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
			alarmIntent.PutExtra("Message", name);
			alarmIntent.PutExtra("title", message);

			PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0 , alarmIntent, PendingIntentFlags.UpdateCurrent);
			AlarmManager alarmManager = (AlarmManager)this.GetSystemService(Context.AlarmService);
			alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);

		}


	}
}





