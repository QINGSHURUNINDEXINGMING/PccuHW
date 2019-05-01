package com.example.myapplication;

import android.app.DatePickerDialog;
import android.app.DatePickerDialog.OnDateSetListener;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;

import java.util.Calendar;

public class MainActivity extends AppCompatActivity {

    private Button chooseDateButton;
    private int year, month, day;

    private void processViews()
    {
        chooseDateButton=(Button)findViewById(R.id.chooseDateButton);
    }

    public void ChooseDate(View view)          //選擇日期
    {
        OnDateSetListener listener = new OnDateSetListener()
        {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int day)          // 設定日期監聽類別
            {
                MainActivity.this.year=year;
                MainActivity.this.month=month;
                MainActivity.this.day=day;

                chooseDateButton.setText(year+"/"+(month+1)+"/"+day);
            }
        };
        DatePickerDialog d =new DatePickerDialog(this, listener, year, month, day);          // 顯示日期對話框
        d.show();

    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        processViews();

        //取得目前日期與時間
        Calendar cal = Calendar.getInstance();

        year= cal.get(Calendar.YEAR);
        month=cal.get(Calendar.MONTH);
        day=cal.get(Calendar.DAY_OF_MONTH);

    }
}
