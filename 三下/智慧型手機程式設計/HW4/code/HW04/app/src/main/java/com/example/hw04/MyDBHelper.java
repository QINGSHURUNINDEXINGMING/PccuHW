package com.example.hw04;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

public class MyDBHelper extends SQLiteOpenHelper { // 繼承SQLiteOpenHelper建立自己的資料庫幫助類別
    private static final String DATABASE_NAME = "MyTallyBook.db"; // 建立"資料庫名稱"常數
    private static final int DATABASE_VERSION = 3; // 建立"資料庫版本"常數
    public MyDBHelper(Context context) { //資料庫幫助類別建構子
        super(context, DATABASE_NAME, null, DATABASE_VERSION); // 執行父類別(SQLiteOpenHelper)之建構子
    }
    @Override // 複寫onCreate方法，在建立資料庫時，就會執行這個方法
    public void onCreate(SQLiteDatabase db) {

    }

    @Override // 複寫onUpgrade方法，在資料庫有新版本時(DATABASE_VERSION有增加時)，就執行此方法
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS tallybook");    //若"記帳簿資料表"tallybook存在，則刪除它
        db.execSQL("DROP TABLE IF EXISTS expenseTypes"); //若"消費種類資料表"expenseTypes存在，則刪除它
        onCreate(db); //呼叫onCreate()方法，重新建立"記帳簿資料表"tallybook及消費種類資料表"expenseTypes
    }
}