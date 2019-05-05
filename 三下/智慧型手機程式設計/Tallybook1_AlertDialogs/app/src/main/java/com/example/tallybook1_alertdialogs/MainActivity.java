//************************************************************************************
//*  本類別實作簡易記帳簿主活動內之各個方法
//*  by Min-Hsiung Hung 2014-04-19
//************************************************************************************
package com.example.tallybook1_alertdialogs;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.TimePickerDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.RadioButton;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.TimePicker;
import android.widget.Toast;
import android.app.Dialog;
import java.util.Calendar;
import java.util.Date;
import java.sql.*;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;

import com.example.tallybook1_alertdialogs.ManageExpenseTypeDialog;
import com.example.tallybook1_alertdialogs.QueryHistoricalExpenseDialog;

public class MainActivity extends Activity
{
    // 與建立資料庫有關之變數宣告
    private static String DATABASE_TABLE1 = "tallybook";     //宣告"運動資料表"常數
    private static String DATABASE_TABLE2 = "expenseTypes";  //宣告"運動型態資料表"常數
    private SQLiteDatabase db;  //宣告資料庫變數
    private MyDBHelper dbHelper; //宣告資料庫幫助器變數



    // 與圖形介面元件相關之變數宣告
    private EditText etxtPrice, etxtComment;
    private Button btnManageExpenseType, btnChoseType;
    private Button btnDate, btnSave, btnQuery, btnDelete;
    private Button btnChoseStartDate, btnChoseEndDate;
    private Button btnCloseApp;
    private Button btnClearMainOutput;
    private TextView txtMainOutput,txtDate;
    private TextView txtStartDate, txtEndDate;

    // 其他變數宣告
    String [] initialExpenseTypes = {"慢走","慢跑","快走","快跑","其他"};
    String [] expenseTypes;


    int Price;
    String ExpenseType="";
    String Comment="";
    String PayDateString="";

    int Year, Month, Day;

    @Override
    public void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main); //顯示操作界面
        // 取得使用者介面上之所有元件
        etxtPrice=(EditText) findViewById(R.id.ENumber);
        etxtComment=(EditText) findViewById(R.id.etxtComment);
        btnManageExpenseType = (Button) findViewById(R.id.btnManageExpenseType);
        btnChoseType = (Button) findViewById(R.id.btnChoseType);
        btnDate = (Button) findViewById(R.id.btnDate);
        btnSave = (Button) findViewById(R.id.btnSave);
        btnQuery = (Button) findViewById(R.id.btnQuery);
        btnDelete = (Button) findViewById(R.id.btnDelete);
        btnCloseApp = (Button) findViewById(R.id.btnCloseApp);
        btnClearMainOutput = (Button) findViewById(R.id.btnClearMainOutput);
        btnChoseStartDate = (Button) findViewById(R.id.btnChoseStartDate);
        btnChoseEndDate = (Button) findViewById(R.id.btnChoseEndDate);
        txtMainOutput=(TextView) findViewById(R.id.txtMainOutput);
        txtDate=(TextView) findViewById(R.id.txtDate);
        txtStartDate=(TextView) findViewById(R.id.txtStartDate);
        txtEndDate=(TextView) findViewById(R.id.txtEndDate);

        // 建立一個SQL資料庫 (MyTallyBook)
        dbHelper = new MyDBHelper(this); //建立資料庫輔助類別物件
        db = dbHelper.getWritableDatabase(); // 透過輔助類別物件建立一個可以讀寫的資料庫

        // 若是資料庫中沒有任何消費種類，則在資料庫中建立預設的消費種類
        Cursor cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2, null);
        if(cursor!=null)
        {
            int n = cursor.getCount();
            if(n==0)
            {
                for (int i=0; i<initialExpenseTypes.length; i++)
                    db.execSQL("INSERT INTO " + DATABASE_TABLE2 + " (expenseType) VALUES ('" +
                            initialExpenseTypes[i] +  "')");
            }
        }
    }
    //
    @Override
    protected void onStop()
    {
        super.onStop();
        db.close(); // 關閉資料庫
    }



    // 點選選擇運動型態按鈕之事件處理程序
    public void btnChoseType_Click(View view)
    {
        txtMainOutput.setText("");

        Cursor cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2, null);
        if(cursor != null)
        {
            int n = cursor.getCount();
            expenseTypes = new String[n];
            cursor.moveToFirst();
            for (int i = 0; i < n; i++)
            {
                expenseTypes[i]= cursor.getString(1);
                cursor.moveToNext();
            }
        }
        else
        {
            expenseTypes = new String[1];
            expenseTypes[0]="";
        }
        // 建立運動型態單選對話方塊
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("選擇運動型態");
        builder.setItems(expenseTypes, listenerChoseType);
        AlertDialog dialogChoseType = builder.create();
        dialogChoseType.show();
    }
    // 建立"運動型態單選對話方塊"之點擊事件處理物件
    DialogInterface.OnClickListener listenerChoseType = new DialogInterface.OnClickListener()
    {
        public void onClick(DialogInterface dialog, int which)
        {
            btnChoseType.setText(expenseTypes[which]) ;
            ExpenseType=expenseTypes[which];
        }
    };
    // 管理運動型態按鈕之點擊事件處理程式
    public void btnManageExpenseType_Click(View view)
    {

        txtMainOutput.setText("");

        ManageExpenseTypeDialog manageExpenseTypeDialog = new ManageExpenseTypeDialog(this);
        manageExpenseTypeDialog.setTitle("管理運動型態對話方塊");
        manageExpenseTypeDialog.show();
    }


    // 選擇運動日期按鈕之點擊事件處理程式
    public void btnDate_Click(View view)
    {
        txtMainOutput.setText("");
        Calendar dt = Calendar.getInstance();
        DatePickerDialog dDialog = new DatePickerDialog(this, new DatePickerDialog.OnDateSetListener() {
            // 覆寫選擇日期後的事件處理方法onDateSet()，所傳入的是所選擇的年、月、日
            // 注意:月份的索引值從0開始，因此正確的月份為(monthOfYear+1)
            @Override
            public void onDateSet(DatePicker view, int year, int monthOfYear,	int dayOfMonth) //傳入使用者所選的時間資訊(年、月、日)
            {
                PayDateString = year + "-" + (monthOfYear+1) + "-" + dayOfMonth;
                txtDate.setText(year+"年"+ (monthOfYear+1) + "月" + dayOfMonth+ "日");
            }}, dt.get(Calendar.YEAR), dt.get(Calendar.MONTH),dt.get(Calendar.DAY_OF_MONTH));
        dDialog.show();
    }

    // 儲存運動紀錄按鈕之點擊事件處理程式
    public void btnSave_Click(View view)
    {
        txtMainOutput.setText("");
        String priceString =etxtPrice.getText().toString();
        Comment=etxtComment.getText().toString();

        int empty=0;
        if(priceString.length()==0)
        {
            Toast.makeText(this, "沒有填寫運動步數!", Toast.LENGTH_SHORT).show();
            empty++;
        }
        if(ExpenseType.length()==0)
        {
            Toast.makeText(this, "選擇運動種類!", Toast.LENGTH_SHORT).show();
            empty++;
        }
        if (PayDateString.length()==0)
        {
            Toast.makeText(this, "沒有選擇運動日期!", Toast.LENGTH_SHORT).show();
            empty++;
        }
        if (empty==0)
        {
            try
            {
                Price=Integer.parseInt(priceString);

                String commandString="INSERT INTO " + DATABASE_TABLE1 + " (price, expenseType, comment, payDate) VALUES " +
                        "(" + Price + ", '" + ExpenseType + "', '" + Comment + "', '" + PayDateString + "')";
                db.execSQL(commandString);
                Toast.makeText(this, "成功儲存新的運動紀錄!", Toast.LENGTH_SHORT).show();

                etxtPrice.setText("");
                etxtComment.setText("");
                btnChoseType.setText("請選擇運動種類");
                txtDate.setText("");
                PayDateString="";
                ExpenseType="";
            }
            catch(Exception ex)
            {
                Toast.makeText(this, "程式出現例外: " + ex.getMessage(), Toast.LENGTH_SHORT).show();
            }
        }
    }

    // 查詢歷史運動紀錄按鈕之點擊事件處理程式
    public void btnQuery_Click(View view)
    {

        txtMainOutput.setText("");

        QueryHistoricalExpenseDialog queryHistoricalExpenseDialog = new QueryHistoricalExpenseDialog(this);
        queryHistoricalExpenseDialog.setTitle("查詢運動紀錄對話方塊"); //設定標題
        queryHistoricalExpenseDialog.show(); // 顯示對話方塊
    }

    // 清除顯示區按鈕之點擊事件處理程式
    public void btnClearMainOutput_Click(View view)
    {
        txtMainOutput.setText(""); //清除主顯示區
    }

    // "關閉運動步數紀錄按鈕"之點擊事件處理程式
    public void btnCloseApp_Click(View view)
    {
        finish(); //關閉運動步數紀錄
    }

}