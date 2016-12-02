using System;

namespace Me
{
    class Constants
    {
        public static String ROW_ID = "id";
        public static String NAME = "name";
		public static String NAME_PERSON = "namePerson";
		public static String NUMBER = "number";	

        public static String DB_NAME = "ManageMe";
        public static String TB_NAME = "NameAndAmount";
		//for first page
		//public static String TB_NAME1 = "House";
		//public static String HOUSE = "houseCount";
        public static int DB_VERSION = 1;

        public static String CREATE_TB = "CREATE TABLE NameAndAmount(id INTEGER PRIMARY KEY AUTOINCREMENT,"
            + "name TEXT NOT NULL, namePerson TEXT NOT NULL, number TEXT NOT NULL);"; //added namePerson and number

		//public static String CREATE_TB1 = "CREATE TABLE House(id INTEGER PRIMARY KEY AUTOINCREMENT, houseCount TEXT NOT NULL);";
        public static String DROP_TB = "DROP TABLE IF EXISTS" + TB_NAME;

     }
}