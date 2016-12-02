using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Me
{
	public class ViewHolder : Java.Lang.Object
	{
		public TextView txtHouse{get; set;}
		public TextView txtName { get; set; }
		public TextView txtAmount { get; set; }

	}
	public class CustomAdapter : BaseAdapter
	{
		private Activity activity;
		private List<Data> data;
		public CustomAdapter(Activity activity, List<Data> data)
		{
			this.activity = activity;
			this.data = data;
		}
		public override int Count
		{
			get
			{
				return data.Count;
			}
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return null;
		}

		public override long GetItemId(int position)
		{
			return data[position].Id;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view, parent, false);
			var txtHouse = view.FindViewById<TextView>(Resource.Id.textView1);
			var txtName = view.FindViewById<TextView>(Resource.Id.textView2);
			var txtAmount = view.FindViewById<TextView>(Resource.Id.textView3);

			txtName.Text = data[position].NamePerson;
			txtHouse.Text = data[position].NameHouse;
			txtAmount.Text = data[position].NameAmount;

			return view;

		}
	}
}
