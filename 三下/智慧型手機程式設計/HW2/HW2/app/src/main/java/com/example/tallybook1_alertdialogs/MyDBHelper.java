//************************************************************************************
//*  本類別實作資料庫建立自訂幫助類別
//*  by Min-Hsiung Hung 2014-04-19
//************************************************************************************
package com.example.tallybook1_alertdialogs;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class MyDBHelper extends SQLiteOpenHelper { // 繼承SQLiteOpenHelper建立自己的資料庫幫助類別
    private static final String DATABASE_NAME = "MyTallyBook"; // 建立"資料庫名稱"常數
    private static final int DATABASE_VERSION = 3; // 建立"資料庫版本"常數
    public MyDBHelper(Context context) { //資料庫幫助類別建構子
        super(context, DATABASE_NAME, null, DATABASE_VERSION); // 執行父類別(SQLiteOpenHelper)之建構子
    }
    @Override
    public void onCreate(SQLiteDatabase db) {

        db.execSQL("CREATE TABLE tallybook (_id integer primary key autoincrement, "
                + "price int not null, expenseType nvarchar(15) not null, comment nvarchar(30) null, "
                + "payDate date not null)");


        db.execSQL("CREATE TABLE expenseTypes (_id integer primary key autoincrement, "
                + "expenseType nvarch(15) not null)");
    }
    @Override // 複寫onUpgrade方法，在資料庫有新版本時(DATABASE_VERSION有增加時)，就執行此方法
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS tallybook");
        db.execSQL("DROP TABLE IF EXISTS expenseTypes");
        onCreate(db);
    }
}