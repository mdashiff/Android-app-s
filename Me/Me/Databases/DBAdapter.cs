using System;
using Android.Content;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Database;

namespace Me
{
	class DBAdapter
	{
		private Context c;
		private SQLiteDatabase db;
		private DBHelper helper;

		public DBAdapter(Context c)
		{
			this.c = c;
			helper = new DBHelper(c);
		}
		public DBAdapter openDB()
		{
			try
			{
				db = helper.WritableDatabase;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

			}
			return this;
		}
		public void CloseDB()
		{
			try
			{
				helper.Close();

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

			}
		}

		public bool Add(String name, String namePerson, String amount)
		{
			try
			{
				ContentValues cv = new ContentValues();
				cv.Put(Constants.NAME, name);
				cv.Put(Constants.NAME_PERSON, namePerson);
				cv.Put(Constants.NUMBER, amount);
				db.Insert(Constants.TB_NAME, Constants.ROW_ID, cv);
				return true;
			}
			catch (Exception e)
			{

				Console.WriteLine(e.Message);
			}
			return false;
		}
		//For Retrieving First Page

		public ICursor Retrieve()
		{
			ICursor c1 = null;
			string sql = "SELECT * FROM " + Constants.TB_NAME + " WHERE " + Constants.NAME;
			c1 = db.RawQuery(sql, null);
			return c1;
		}

		public int Delete(String name)
		{
			try
			{
				SQLiteDatabase db1 = helper.WritableDatabase;
				String[] whereArgs = { name };
				int count = db1.Delete(Constants.TB_NAME, Constants.NAME + "=?", whereArgs);
				return count;
			}
			catch (Exception e)
			{
				String s2 = e.Message;
				MainActivity1 m1 = new MainActivity1();
				Toast.MakeText(m1, "Error thrown" + " " + s2, ToastLength.Long).Show();
			}
			return 0;
		}
		public ICursor GetNames()
		{
			String[] columns = { Constants.ROW_ID, Constants.NAME, Constants.NAME_PERSON, Constants.NUMBER };

			return db.Query(Constants.TB_NAME, columns, null, null, null, null, null);
		}

		public ICursor RetrieveList(String chumma)
		{
			ICursor c1 = null;
			string sql = "SELECT * FROM " + Constants.TB_NAME + " WHERE " +Constants.NAME + "=" + "'"+chumma +"'";
			c1 = db.RawQuery(sql, null);
			return c1;
		}

	}
}