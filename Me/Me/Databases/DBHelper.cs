using System;
using Android.Content;
using Android.Database.Sqlite;

namespace Me
{
	class DBHelper : SQLiteOpenHelper
	{
		public DBHelper(Context context) : base(context, Constants.DB_NAME, null, Constants.DB_VERSION)
		{
		}
		public override void OnCreate(SQLiteDatabase db)
		{
			try
			{
				db.ExecSQL(Constants.CREATE_TB);
			}
			catch (Exception e)
			{

				Console.WriteLine(e.Message);
			}
		}

		public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
		{
			db.ExecSQL(Constants.DROP_TB);
			OnCreate(db);
		}
	}
}