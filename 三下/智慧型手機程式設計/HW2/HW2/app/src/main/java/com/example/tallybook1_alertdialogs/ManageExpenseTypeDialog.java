//************************************************************************************
//*  本類別實作管理消費種類之各個方法
//*  by Min-Hsiung Hung 2014-04-19
//************************************************************************************
package com.example.tallybook1_alertdialogs;

import android.app.Dialog;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class ManageExpenseTypeDialog extends Dialog implements View.OnClickListener
{

    // 與建立資料庫有關之變數宣告
    private static String DATABASE_TABLE2 = "expenseTypes";
    private SQLiteDatabase db;
    private MyDBHelper dbHelper;

    // 與圖形介面元件相關之變數宣告
    Button btnCreateNewExpenseType, btnDeleteExpenseType;
    Button btnDisplayExpenseTypes, btnCloseDialog;
    EditText etxtNewExpenseType, etxtOldExpenseType;
    TextView txtShowExpenseTypes;
    Context myDialogContext;

    public ManageExpenseTypeDialog(Context context) // 自訂對話方塊之建構子
    {
        super(context);

        setContentView(R.layout.manageexpensetypedialog);

        // 取得使用者介面上之所有元件
        btnCreateNewExpenseType = (Button) findViewById(R.id.btnCreateNewExpenseType);
        btnDeleteExpenseType = (Button) findViewById(R.id.btnDeleteExpenseType);
        btnDisplayExpenseTypes = (Button) findViewById(R.id.btnDispalyExpenseTypes);
        btnCloseDialog = (Button) findViewById(R.id.btnCloseDialog);
        etxtNewExpenseType = (EditText) findViewById(R.id.etxtNewExpenseType);
        etxtOldExpenseType = (EditText) findViewById(R.id.etxtOldExpenseType);
        txtShowExpenseTypes = (TextView) findViewById(R.id.txtShowExpenseTypes);
        myDialogContext = getContext(); // 取得對話方塊之背景物件

        // 建立一個SQL資料庫 (MyTallyBook)
        dbHelper = new MyDBHelper(myDialogContext); //建立資料庫輔助類別物件
        db = dbHelper.getWritableDatabase(); // 透過輔助類別物件建立一個可以讀寫的資料庫


        Cursor cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2, null);
        if (cursor != null)
        {
            int n = cursor.getCount(); //取得消費種類紀錄筆數
            String str="目前已建立" + n + "個運動型態:\n";
            cursor.moveToFirst();  // 將指標移到第1筆紀錄
            for (int i = 0; i < n; i++) //利用迴圈讀取每一筆紀錄
            {
                str += (i+1) +": " + cursor.getString(1) + "\n"; //讀取第1個欄位值(消費種類expenseType)，然後串接到顯示字串(str)中
                cursor.moveToNext();  // 將指標移到下一筆紀錄
            }
            txtShowExpenseTypes.setText(str);
        }
        else
        {
            txtShowExpenseTypes.setText("目前並沒有建立任何運動型態!\n");
        }

        btnCreateNewExpenseType.setOnClickListener(this);
        btnDeleteExpenseType.setOnClickListener(this);
        btnDisplayExpenseTypes.setOnClickListener(this);
        btnCloseDialog.setOnClickListener(this);
    }
    // Click事件處理方法
    public void onClick(View v)
    {
        if (v == btnCreateNewExpenseType)
        {

            String newExpenseTypeString =etxtNewExpenseType.getText().toString().trim();

            int empty=0;
            if(newExpenseTypeString.length()==0)
            {
                Toast.makeText(myDialogContext, "沒有填寫新運動型態!", Toast.LENGTH_SHORT).show();
                empty++;
            }
            if (empty==0)
            {
                try
                {

                    Cursor cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2 + " WHERE expenseType = '" +
                            newExpenseTypeString + "'", null);
                    int count = cursor.getCount();
                    if(count==0)
                    {
                        db.execSQL("INSERT INTO " + DATABASE_TABLE2 + " (expenseType) VALUES ('" +
                                newExpenseTypeString +"')");

                        Toast.makeText(myDialogContext, "新增運動型態成功!", Toast.LENGTH_SHORT).show();
                        etxtNewExpenseType.setText("");

                        cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2, null);
                        int n = cursor.getCount();
                        String str="目前已建立之運動型態:\n";
                        cursor.moveToFirst();
                        for (int i = 0; i < n; i++)
                        {
                            str += (i+1) +": " + cursor.getString(1) + "\n";
                            cursor.moveToNext();
                        }
                        txtShowExpenseTypes.setText(str);
                    }
                    else
                    {
                        Toast.makeText(myDialogContext, "該運動型態已經存在!", Toast.LENGTH_SHORT).show();
                    }
                }
                catch(Exception ex)
                {
                    Toast.makeText(myDialogContext, "程式出現例外: " + ex.getMessage(), Toast.LENGTH_SHORT).show();
                }
            }
        }
        //
        if(v ==btnDeleteExpenseType)
        {
            String oldExpenseTypeIDString =etxtOldExpenseType.getText().toString().trim();
            int empty=0;
            if(oldExpenseTypeIDString.length()==0)
            {
                Toast.makeText(myDialogContext, "沒有填寫運動型態編號!", Toast.LENGTH_SHORT).show();
                empty++;
            }
            if (empty==0)
            {
                try
                {
                    int oldExpenseTypeID = Integer.parseInt(oldExpenseTypeIDString);

                    Cursor cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2 , null);
                    int count = cursor.getCount(); //取的紀錄筆數
                    if((oldExpenseTypeID > count) || (oldExpenseTypeID < 1))
                    {
                        Toast.makeText(myDialogContext, "運動型態編號超過範圍!", Toast.LENGTH_SHORT).show();
                    }
                    else
                    {

                        cursor.moveToPosition(oldExpenseTypeID-1);
                        String str2=cursor.getString(1);

                        db.execSQL("DELETE FROM " + DATABASE_TABLE2 + " WHERE expenseType='" + str2 + "'");

                        Toast.makeText(myDialogContext, "刪除運動型態成功!", Toast.LENGTH_SHORT).show();
                        etxtOldExpenseType.setText("");

                        cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2, null);
                        if(cursor!=null)
                        {
                            int n =cursor.getCount();
                            String str="目前已建立之運動型態:\n";
                            cursor.moveToFirst();
                            for (int i = 0; i < n; i++)
                            {
                                str += (i+1) +": " + cursor.getString(1) + "\n";
                                cursor.moveToNext();
                            }
                            txtShowExpenseTypes.setText(str);
                        }
                        else
                        {
                            txtShowExpenseTypes.setText("目前並沒有任何運動型態!\n");
                        }

                    }
                }
                catch(Exception ex)
                {
                    Toast.makeText(myDialogContext, "程式出現例外: " + ex.getMessage(), Toast.LENGTH_SHORT).show();
                }
            }
        }
        //
        if(v ==btnDisplayExpenseTypes)
        {

            Cursor cursor = db.rawQuery("SELECT * FROM " + DATABASE_TABLE2, null);
            if (cursor != null)
            {
                int n = cursor.getCount(); //取得資料筆數
                String str="目前已建立之運動型態:\n";
                cursor.moveToFirst();
                for (int i = 0; i < n; i++)
                {
                    str += (i+1) +": " + cursor.getString(1) + "\n";
                    cursor.moveToNext();
                }
                txtShowExpenseTypes.setText(str);
            }
            else
            {
                txtShowExpenseTypes.setText("目前並沒有建立任何運動型態!\n");
            }

        }
        //
        if(v==btnCloseDialog) //若發出點擊事件者為"關閉對話方塊按鈕"，則進行以下處理
        {
            dismiss(); //關閉查詢對話方塊
        }
    }
}
