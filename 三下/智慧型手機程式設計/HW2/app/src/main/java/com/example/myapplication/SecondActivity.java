package com.example.myapplication;

import android.app.DatePickerDialog;
import android.app.DatePickerDialog.OnDateSetListener;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;

import java.util.Calendar;

public class SecondActivity extends AppCompatActivity
{
    private Button startDate, endDate;

    private int year, month, day;

    private Bundle bundle;



    private void processViews()
    {
        startDate = (Button)findViewById(R.id.startDate);
        endDate = (Button)findViewById(R.id.endDate);
    }

    public void chooseStartDate(View view)          //選擇開始日期按鈕
    {
        OnDateSetListener listener = new OnDateSetListener()
        {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int day)          // 設定日期監聽類別
            {
                SecondActivity.this.year=year;
                SecondActivity.this.month=month;
                SecondActivity.this.day=day;

                startDate.setText(year+"/"+(month+1)+"/"+day);
            }
        };
        DatePickerDialog d =new DatePickerDialog(this, listener, year, month, day);          // 顯示日期對話框
        d.show();
    }
    public void chooseEndDate(View view)          //選擇結束日期按鈕
    {
        OnDateSetListener listener = new OnDateSetListener()
        {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int day)          // 設定日期監聽類別
            {
                SecondActivity.this.year=year;
                SecondActivity.this.month=month;
                SecondActivity.this.day=day;

                endDate.setText(year+"/"+(month+1)+"/"+day);
            }
        };
        DatePickerDialog d =new DatePickerDialog(this, listener, year, month, day);          // 顯示日期對話框
        d.show();
    }
    public void goBack(View view)
    {
        finish();
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.second);

        processViews();

        // 取得Intent物件
        Intent intent = getIntent();

        // 讀取Intent物件中包裝所有資料的Bundle物件
        bundle = intent.getExtras();

        //取得目前日期與時間
        Calendar cal = Calendar.getInstance();

        year= cal.get(Calendar.YEAR);
        month=cal.get(Calendar.MONTH);
        day=cal.get(Calendar.DAY_OF_MONTH);

    }

}
