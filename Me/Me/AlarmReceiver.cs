using System;
using Android.Content;
using Android.App;
using Android.Support.V4.App;
using Android.Graphics;

namespace Me
{
	public class AlarmReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			var message = intent.GetStringExtra("message");
			var title = intent.GetStringExtra("title");

			var notIntent = new Intent(context, typeof(MainActivity1));
			var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.UpdateCurrent);
			var manager = NotificationManagerCompat.From(context);
			var style = new NotificationCompat.BigTextStyle();
			style.BigText(message);
			int resourceId;
			resourceId = Resource.Drawable.ic_stat_button_click;

			var wearableExtender = new NotificationCompat.WearableExtender()
				.SetBackground(BitmapFactory.DecodeResource(context.Resources, resourceId))
							;
			var builder = new NotificationCompat.Builder(context)
						.SetContentIntent(contentIntent)
						.SetSmallIcon(Resource.Drawable.ic_stat_button_click)
						.SetContentTitle(title)
						.SetContentText(message)
						.SetStyle(style)
						.SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
						.SetAutoCancel(true)
						.Extend(wearableExtender);


			var notification = builder.Build();
			manager.Notify(0, notification);
		}
	}
}