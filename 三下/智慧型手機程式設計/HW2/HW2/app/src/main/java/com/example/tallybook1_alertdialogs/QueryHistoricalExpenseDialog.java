//************************************************************************************
//*  本類別實作查詢歷史消費紀錄之各個方法
//*  by Min-Hsiung Hung 2014-04-19
//************************************************************************************
package com.example.tallybook1_alertdialogs;

import java.util.Calendar;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.Context;
import android.content.DialogInterface;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.Toast;

public class QueryHistoricalExpenseDialog extends Dialog implements View.OnClickListener
{

    // 與建立資料庫有關之變數宣告
    private static String DATABASE_TABLE1 = "tallybook";
    private SQLiteDatabase db;
    private MyDBHelper dbHelper;

    // 與圖形介面元件相關之變數宣告
    Button btnChoseStartDate, btnChoseEndDate;
    Button btnQuery, btnDelete;
    Button btnClearMainOutput, btnCloseDialog;
    RadioButton radioButtonByDate, radioButtonByType;
    TextView txtStartDate, txtEndDate, txtMainOutput;
    Context myDialogContext;

    // 其他變數
    String StartDateString="", EndDateString="";

    public QueryHistoricalExpenseDialog(Context context)
    {
        super(context);

        setContentView(R.layout.queryhistoricalexpensedialog);

        // 取得使用者介面上之所有元件
        btnChoseStartDate = (Button) findViewById(R.id.btnChoseStartDate);
        btnChoseEndDate = (Button) findViewById(R.id.btnChoseEndDate);
        btnQuery = (Button) findViewById(R.id.btnQuery);
        btnDelete = (Button) findViewById(R.id.btnDelete);
        btnClearMainOutput = (Button) findViewById(R.id.btnClearMainOutput);
        btnCloseDialog = (Button) findViewById(R.id.btnCloseDialog);
        radioButtonByDate = (RadioButton) findViewById(R.id.radioButtonByDate);
        radioButtonByType = (RadioButton) findViewById(R.id.radioButtonByType);
        txtStartDate = (TextView) findViewById(R.id.txtStartDate);
        txtEndDate = (TextView) findViewById(R.id.txtEndDate);
        txtMainOutput = (TextView) findViewById(R.id.txtMainOutput);
        myDialogContext = getContext();

        // 建立一個SQL資料庫 (MyTallyBook)
        dbHelper = new MyDBHelper(myDialogContext);
        db = dbHelper.getWritableDatabase(); // 透過輔助類別物件建立一個可以讀寫的資料庫

        // 清除主顯示區域
        txtMainOutput.setText("");

        btnChoseStartDate.setOnClickListener(this);
        btnChoseEndDate.setOnClickListener(this);
        btnQuery.setOnClickListener(this);
        btnDelete.setOnClickListener(this);
        btnClearMainOutput.setOnClickListener(this);
        btnCloseDialog.setOnClickListener(this);
    }
    // Click事件處理方法
    public void onClick(View v) // 對話方塊上6個操作按鈕之點擊事件都是由本方法來處理，傳入的物件為發出點擊事件之按鈕
    {
        if(v==btnChoseStartDate)
        {
            txtMainOutput.setText(""); //清除主顯示區
            Calendar dt = Calendar.getInstance(); //取得一個日曆物件
            DatePickerDialog dDialog = new DatePickerDialog(myDialogContext, new DatePickerDialog.OnDateSetListener() {
                // 覆寫選擇日期後的事件處理方法onDateSet()，所傳入的是所選擇的年、月、日，
                // 注意:月份的索引值從0開始，因此正確的月份為(monthOfYear+1)
                @Override
                public void onDateSet(DatePicker view, int year, int monthOfYear,	int dayOfMonth)
                {
                    StartDateString = year + "-" + (monthOfYear+1) + "-" + dayOfMonth; //以所選日期建立一個格式化的起始日期字串
                    txtStartDate.setText(year+ "年" + (monthOfYear+1) +"月"+ dayOfMonth +"日"); //將所選日期顯示在標籤上
                }}, dt.get(Calendar.YEAR), dt.get(Calendar.MONTH),dt.get(Calendar.DAY_OF_MONTH)); //以目前日期為預設日期
            dDialog.show(); //顯示日期選擇器對話方塊
        }
        //
        if(v==btnChoseEndDate)  //若發出點擊事件者為"選擇結束日期按鈕"，則進行以下處理
        {
            txtMainOutput.setText(""); //清除主顯示區
            Calendar dt = Calendar.getInstance(); //取得一個日曆物件
            DatePickerDialog dDialog = new DatePickerDialog(myDialogContext, new DatePickerDialog.OnDateSetListener() {
                // 覆寫選擇日期後的事件處理方法onDateSet()，所傳入的是所選擇的年、月、日
                // 注意:月份的索引值從0開始，因此正確的月份為(monthOfYear+1)
                @Override
                public void onDateSet(DatePicker view, int year, int monthOfYear,	int dayOfMonth)
                {
                    EndDateString = year + "-" + (monthOfYear+1) + "-" + dayOfMonth; //以所選日期建立一個格式化的結束日期字串
                    txtEndDate.setText(year+ "年" + (monthOfYear+1) +"月"+ dayOfMonth +"日"); //將所選日期顯示在標籤上
                }}, dt.get(Calendar.YEAR), dt.get(Calendar.MONTH),dt.get(Calendar.DAY_OF_MONTH)); //以目前日期為預設日期
            dDialog.show(); //顯示日期選擇器對話方塊
        }
        //
        if(v==btnQuery)
        {
            txtMainOutput.setText(""); //清除主顯示區
            // 以下確保使用者有選擇起始日期及結束日期
            int empty=0;
            if(StartDateString.length()==0) // 若沒有選擇起始日期，則利用吐司訊息提醒使用者
            {
                Toast.makeText(myDialogContext, "沒有選擇起始日期!", Toast.LENGTH_SHORT).show();
                empty++;
            }
            if(EndDateString.length()==0)   // 若沒有選擇結束日期，則利用吐司訊息提醒使用者
            {
                Toast.makeText(myDialogContext, "沒有選擇結束日期!", Toast.LENGTH_SHORT).show();
                empty++;
            }
            if (empty==0) // 若使用者有選擇起始日期及結束日期，則進行以下處理
            {
                try //利用try{}包住可能會產生例外的處理程式碼(如存取資料庫及檔案)，讓程式即便有例外，也不會當掉，以增加app之強韌性
                {
                    if(radioButtonByDate.isChecked()) // 若選擇依照日期先後顯示
                    {

                        String commandString = "SELECT * FROM " + DATABASE_TABLE1 + " WHERE payDate between '" +
                                StartDateString + "' and '" + EndDateString +"' ORDER BY payDate" ;
                        Cursor cursor = db.rawQuery(commandString, null); //執行資料表查詢命令
                        if(cursor != null)
                        {
                            int n = cursor.getCount(); //取得資料筆數
                            String str = "在" + StartDateString + "到" + EndDateString + "共有"+ n + "筆運動紀錄:\n";
                            int totalAmount = 0;
                            cursor.moveToFirst();  // 將指標移到第1筆紀錄
                            for (int i = 0; i < n; i++) // 利用迴圈逐一讀取每一筆紀錄
                            {
                                totalAmount += Integer.parseInt(cursor.getString(1));
                                cursor.moveToNext();  // 移動到下一筆
                            }
                            str += "共計 " + totalAmount +" 步\n";
                            // 顯示消費紀錄之每一個欄位之抬頭
                            String[] colNames = {"編號", "步數", "運動型態", "說明", "運動日期"};
                            for (int i = 0; i < colNames.length; i++)
                                str += String.format("%5s\t", colNames[i]); // 將每一個欄位的抬頭串接到顯示字串(str)中
                            str += "\n";

                            cursor.moveToFirst();  // 將指標移到第1筆紀錄
                            // 顯示欄位值
                            for (int i = 0; i < n; i++) //利用迴圈讀取每一筆紀錄之各個欄位
                            {
                                str += String.format("%6s\t", (i+1)); // 串接記錄編號(索引值+1)
                                str += String.format("%8s\t", cursor.getString(1));
                                str += String.format("%6s\t", cursor.getString(2));
                                str += String.format("%8s\t", cursor.getString(3));
                                str += String.format("%14s\t", cursor.getString(4));
                                str += "\n";
                                cursor.moveToNext();  // 移動到下一筆
                            }
                            txtMainOutput.setText(str);
                        }
                        else
                        {
                            txtMainOutput.setText("在" + StartDateString + "到" + EndDateString + "並沒有運動紀錄!\n");
                        }
                    }
                    if(radioButtonByType.isChecked())
                    {

                        Cursor cursor = db.rawQuery("SELECT DISTINCT expenseType FROM " + DATABASE_TABLE1 +
                                " WHERE payDate between '" + StartDateString + "' and '" +
                                EndDateString +"'", null); //執行資料表查詢命令
                        if(cursor != null)
                        {
                            String str1 = "在" + StartDateString + "到" + EndDateString + "之步數統計如下:\n";
                            String str="";
                            int n = cursor.getCount();
                            int totalAmount=0, num1; // 用於紀錄
                            String [] types = new String[n];
                            cursor.moveToFirst(); // 移到第1筆紀錄
                            for(int i=0; i<n; i++)
                            {
                                types[i]=cursor.getString(0);


                                String commandString = "SELECT SUM(price) FROM " + DATABASE_TABLE1 +
                                        " WHERE (payDate between '" + StartDateString + "' AND '" +
                                        EndDateString +"') AND (expenseType = '" + types[i]+ "')";
                                Cursor cursor1 = db.rawQuery(commandString, null);
                                cursor1.moveToFirst();
                                if(cursor1 != null)
                                    num1 = cursor1.getInt(0);
                                else
                                    num1=0;
                                totalAmount += num1;
                                str += String.format("%5s: " + "%8s \n", types[i],  num1);
                                cursor.moveToNext();
                            }
                            str1 += "運動步數總計" + totalAmount + "\n";
                            str1 += str;
                            txtMainOutput.setText(str1);	//將顯示字串str1的內容顯示在主顯示區文字盒上
                        }
                        else // 若有傳回消費紀錄，則顯示沒有消費紀錄
                        {
                            txtMainOutput.setText("在" + StartDateString + "到" + EndDateString + "並沒有運動紀錄!\n");
                        }
                    }
                }
                catch(Exception ex) // try{}中的程式碼執行時發生例外，則利用catch去捕捉，並利用吐司訊息提示給使用者知道
                {
                    Toast.makeText(myDialogContext, "程式出現例外: " + ex.getMessage(), Toast.LENGTH_SHORT).show();
                }
            }
        }
        //
        if(v ==btnDelete) //若發出點擊事件者為"刪除運動紀錄按鈕"，則進行以下處理
        {
            txtMainOutput.setText(""); //清除主顯示區
            // 以下確保使用者有選擇起始日期及結束日期
            int empty=0;
            if(StartDateString.length()==0)  // 若沒有選擇起始日期，則利用吐司訊息提醒使用者
            {
                Toast.makeText(myDialogContext, "沒有選擇起始日期!", Toast.LENGTH_SHORT).show();
                empty++;
            }
            if(EndDateString.length()==0)  // 若沒有選擇結束日期，則利用吐司訊息提醒使用者
            {
                Toast.makeText(myDialogContext, "沒有選擇結束日期!", Toast.LENGTH_SHORT).show();
                empty++;
            }
            if (empty==0) // 若使用者有選擇起始日期及結束日期，則進行以下處理
            {
                try //利用try{}包住可能會產生例外的處理程式碼(如存取資料庫及檔案)，讓程式即便有例外，也不會當掉，以增加app之強韌性
                {

                    AlertDialog.Builder builder = new AlertDialog.Builder(myDialogContext); //建立警示對話方塊物件
                    builder.setTitle("確認對話方塊") //設定警示對話方塊之標題
                            .setMessage("確定刪除選取日期範圍內之運動紀錄?") // //設定警示對話方塊之顯示訊息
                            .setPositiveButton("確定", new OnClickListener() { // 建立"確定按鈕"
                                public void onClick(DialogInterface dialoginterface, int i) // 點選"確認按鈕"之事件處理方法
                                {
                                    // 查詢刪除前之消費紀錄筆數
                                    Cursor cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE1, null);
                                    int n1 = cursor.getCount(); //取得刪除前之消費紀錄筆數

                                    // 刪除選定日期範圍內之消費紀錄

                                    String commandString = "DELETE FROM " + DATABASE_TABLE1 +
                                            " WHERE payDate between '" + StartDateString +
                                            "' and '" + EndDateString +"'";
                                    db.execSQL(commandString);

                                    // 查詢刪除後之運動紀錄筆數
                                    cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE1, null);
                                    int n2 = cursor.getCount(); //取得刪除後之運動紀錄筆數

                                    // 在主顯示區顯示已成功刪除的消費紀錄筆數
                                    txtMainOutput.setText("已成功刪除" + (n1-n2) + "筆運動紀錄!");
                                }
                            })
                            .setNegativeButton("取消", null)
                            .show(); // 顯示所建立之警示對話方塊
                }
                catch(Exception ex) // try{}中的程式碼執行時發生例外，則利用catch去捕捉，並利用吐司訊息提示給使用者知道
                {
                    Toast.makeText(myDialogContext, "程式出現例外: " + ex.getMessage(), Toast.LENGTH_SHORT).show();
                }
            }
        }
        //
        if(v ==btnClearMainOutput) //若發出點擊事件者為"清除主顯示區按鈕"，則進行以下處理
        {
            txtMainOutput.setText(""); // 清除主顯示區
        }
        //
        if(v==btnCloseDialog)  //若發出點擊事件者為"關閉對話方塊按鈕"，則進行以下處理
        {
            dismiss(); //關閉查詢對話方塊
        }
    }
}